using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleCreator
{
    public partial class mySNHULoginWindow : Form
    {
        public class ClassData
        {
            public string CourseNameAndTitle;
            public string ProfName;
            public string ProfEmail;
            public string MeetingInformation;
            public string Creds;
        }

        public static mySNHULoginWindow instance;

        private List<ClassData> ClassDataFound = new List<ClassData>();

        private readonly string baseURL = "https://my.snhu.edu/";

        private int classDataIncrementor = 0;
        private int numberOfClassesFound = 0;

        private TermSelectWindow selectTermWindow;

        //private int browserCommandSequenceCount = 0;

        public mySNHULoginWindow()
        {
            StartPosition = FormStartPosition.CenterScreen;
            instance = this;
            InitializeComponent();
        }

        /// <summary>
        /// Gets called when the window is being closed.
        /// Allows to have only a single instance of this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginWindow_FormClosing(Object sender, FormClosingEventArgs e)
        {
            MainWindow.instance.ClosedLoginWindowCallback();

            if(selectTermWindow != null)
            {
                selectTermWindow.Close();
            }
        }

        /// <summary>
        /// Indicates to the user that the window is loading by changing the UI
        /// </summary>
        /// <param name="status"></param>
        private void CanNowUseUI(bool status)
        {
            ControlBox = status;
            button1.Enabled = status;

            if(!status)
            {
                button1.Text = "Loading...";
            }
            else
            {
                button1.Text = "Login";
            }
        }

        /// <summary>
        /// Login (and get data) button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox1.Text != "")
            {
                CanNowUseUI(false);

                if (webBrowser1.IsDisposed)
                {
                    webBrowser1 = new WebBrowser();
                }

                webBrowser1.Navigate(new Uri(baseURL));
                LoadInLoginDataAfterSecond();
            }
            else
            {
                MessageBox.Show("A login field is empty!");
                ResetWindowUI();
            }
        }

        /// <summary>
        /// Outputs all the class data into a single string
        /// </summary>
        /// <returns></returns>
        private string ConvertDataToString()
        {
            string result = "";

            for (int i = 0; i < ClassDataFound.Count; i++)
            {
                result += ClassDataFound[i].CourseNameAndTitle + '\r' + '\n';
                result += ClassDataFound[i].ProfName + '\r'+'\n';
                result += ClassDataFound[i].ProfEmail + '\r'+'\n';
                result += ClassDataFound[i].MeetingInformation + '\r' + '\n';
                result += ClassDataFound[i].Creds + '\r' + '\n' + '\r' + '\n' + '\r' + '\n';
            }

            return result;
        }

        /// <summary>
        /// Loads login data into their respective fields (If we could connect)
        /// </summary>
        /// <returns></returns>
        private async Task LoadInLoginDataAfterSecond()
        {
            //Wait a second for the page to load
            await Task.Delay(1000);

            //If we connected, load in our login data and submit
            if (webBrowser1.Document.GetElementById("MessageDIV") != null)
            {
                webBrowser1.Document.GetElementById("input_1").SetAttribute("value",textBox1.Text);
                webBrowser1.Document.GetElementById("input_2").SetAttribute("value", textBox2.Text);

                webBrowser1.Document.GetElementById("SubmitCreds").InvokeMember("click");

                await ClickMyClassScheduleButtonAfterSeconds();
            }
            else
            {
                MessageBox.Show("Could not connect to mySNHU!");
                ResetWindowUI();
            }
        }

        /// <summary>
        /// Finds the My Class Schedule button on the home page and clicks it
        /// </summary>
        /// <returns></returns>
        private async Task ClickMyClassScheduleButtonAfterSeconds()
        {
            //Wait 8 second for the home page to load
            await Task.Delay(8000);

            if (webBrowser1.Document.GetElementById("MessageDIV") == null)
            {
                //Get the element that contains the button we're looking for
                HtmlElement listedOptions = webBrowser1.Document.GetElementById("slwp_ctl00_SPWebPartManager1_g_8c9117d0_9229_4728_b58a_89abc3e71a69");

                //Get the link reference from the button and navigate to it
                string webRef = listedOptions.FirstChild.FirstChild.Children[1].Children[6].FirstChild.FirstChild.GetAttribute("href");
                webBrowser1.Navigate(new Uri(webRef));

                await SelectClassTerm();
            }
            else
            {
                MessageBox.Show("Login Failed!");
                ResetWindowUI();
            }
        }

        /// <summary>
        /// Brings up a window directing the user to select the term they want to use
        /// </summary>
        /// <returns></returns>
        private async Task SelectClassTerm()
        {
            //Wait 3 seconds for the My Schedule home page to load
            await Task.Delay(3000);

            //Get the drop down object
            HtmlElement termDropdown = webBrowser1.Document.GetElementById("VAR4");

            //Arrays where the index represents a drop down element's string (term) and it's value
            string[] terms = new string[termDropdown.Children.Count];
            string[] vals = new string[termDropdown.Children.Count];

            for (int i = 0; i < termDropdown.Children.Count; i++)
            {
                terms[i] = termDropdown.Children[i].InnerText;
                vals[i] = termDropdown.Children[i].GetAttribute("value");
            }

            selectTermWindow = new TermSelectWindow();
            selectTermWindow.Show();

            selectTermWindow.LoadComboBoxData(terms, vals);
        }

        /// <summary>
        /// Continues the scraping process by recieving what term the user wants
        /// </summary>
        /// <param name="term"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public async Task LoadSelectedTerm(string term, string val)
        {

            //Get the drop down object again...
            HtmlElement termDropdown = webBrowser1.Document.GetElementById("VAR4");

            //Change the drop down's default selected object to the most recent one we want
            termDropdown.FirstChild.InnerText = term;
            termDropdown.FirstChild.SetAttribute("value", val);

            //Click Submit Button
            webBrowser1.Document.GetElementById("WASubmit").InvokeMember("click");

            //Wait 1 second to load the schedule
            await Task.Delay(1000);

            await GetClassData();
        }

        /// <summary>
        /// Goes through every single class to get the data we need
        /// </summary>
        /// <returns></returns>
        private async Task GetClassData()
        {
            //Set incrementor to 1 to skip the header part of the schedule table
            classDataIncrementor = 1;

            //Set to the number of classes we have
            numberOfClassesFound = webBrowser1.Document.GetElementById("GROUP_Grp_LIST_VAR6").Children[2].FirstChild.Children.Count;

            if (numberOfClassesFound != 1)
            {

                //Increment through each class to get the data
                while (classDataIncrementor < numberOfClassesFound)
                {
                    //Get the tableElement that contains the class info
                    HtmlElement tableElement = webBrowser1.Document.GetElementById("GROUP_Grp_LIST_VAR6").Children[2].FirstChild.Children[classDataIncrementor];

                    //Get the class name, meetingInfo, and credit amount
                    ClassData newClassData = new ClassData();
                    newClassData.CourseNameAndTitle = tableElement.Children[1].FirstChild.FirstChild.InnerText;
                    newClassData.MeetingInformation = tableElement.Children[3].FirstChild.InnerText;
                    newClassData.Creds = tableElement.Children[4].FirstChild.InnerText;

                    //Click the class title link to find more information (Opens a tab that's internal)
                    webBrowser1.Document.GetElementById("LIST_VAR6_" + classDataIncrementor).InvokeMember("click");

                    //Wait 2 seconds for the tab to load
                    await Task.Delay(2000);

                    //Get the professor's name and email and add this data entry to a list
                    newClassData.ProfName = webBrowser1.Document.GetElementById("LIST_VAR7_1").InnerText;
                    newClassData.ProfEmail = webBrowser1.Document.GetElementById("LIST_VAR10_1").InnerText;

                    //Modify string to get rid of that extra space on the far right
                    newClassData.ProfEmail = newClassData.ProfEmail.Substring(0, newClassData.ProfEmail.Length - 1);

                    ClassDataFound.Add(newClassData);

                    //Get the tab we just opened and close it
                    webBrowser1.Document.GetElementById("openTab").FirstChild.FirstChild.FirstChild.Children[1].InvokeMember("click");

                    //Wait to get back to our original schedule tab
                    await Task.Delay(1000);

                    classDataIncrementor++;
                }

                //Load the data we found
                MainWindow.instance.LoadDataIntoTextBox(ConvertDataToString());

            }
            else
            {
                MessageBox.Show("No classes were found!");
            }

            //Close this window
            ResetWindowUI();
            MainWindow.instance.Focus();
            Close();
        }

        /// <summary>
        /// An easier way of 'resetting' the window's UI to make it look normal again
        /// </summary>
        public void ResetWindowUI()
        {
            //browserCommandSequenceCount = 0;

            //Stop the Web Browser
            webBrowser1.Stop();
            webBrowser1.Dispose();

            CanNowUseUI(true);
        }
    }
}
