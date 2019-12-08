using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ScheduleCreator
{
    public partial class AddClassWindow : Form
    {
        /// <summary>
        /// List of errors to check when accepting input from the user
        /// </summary>
        private List<string> Errors = new List<string>
        {
            "ClassDay","ClassName","ClassLocation","ProfessorFullName", "ProfessorEmail"
        };

        public AddClassWindow()
        {
            InitializeComponent();
            SetErrorUI();
            StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Sets the UI to what's contained in Errors list
        /// </summary>
        public void SetErrorUI()
        {
            for(int i = 0; i < Errors.Count; i++)
            {
                SetErrorUIElement(Errors[i], true);
            }
        }

        /// <summary>
        /// Sets the UI element to indicate if error occured or not. (True = red, False = black)
        /// </summary>
        /// <param name="element"></param>
        /// <param name="status"></param>
        public void SetErrorUIElement(string element, bool status)
        {
            switch (element)
            {
                case "ProfessorFullName":
                    label8.ForeColor = (status) ? Color.Red : Color.Black;
                    break;

                case "ClassDay":
                    label1.ForeColor = (status) ? Color.Red : Color.Black;
                    break;

                case "ClassName":
                    label2.ForeColor = (status) ? Color.Red : Color.Black;
                    break;

                case "ClassLocation":
                    label4.ForeColor = (status) ? Color.Red : Color.Black;
                    break;

                case "ProfessorEmail":
                    label9.ForeColor = (status) ? Color.Red : Color.Black;
                    break;

                case "ClassCredits":
                    label10.ForeColor = (status) ? Color.Red : Color.Black;
                    break;

                case "ClassDate":
                    label3.ForeColor = (status) ? Color.Red : Color.Black;
                    label5.ForeColor = (status) ? Color.Red : Color.Black;
                    break;

                case "ClassTime":
                    label6.ForeColor = (status) ? Color.Red : Color.Black;
                    label7.ForeColor = (status) ? Color.Red : Color.Black;
                    break;
            }
        }

        /// <summary>
        /// Gets called when the window is being closed.
        /// Allows to have only a single instance of this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClassWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainWindow.instance.ClosedAddClassWindowCallback();
        }

        /// <summary>
        /// Keep track of any errors that occur with user input
        /// </summary>
        /// <param name="errorOrigin"></param>
        /// <param name="errorOccured"></param>
        private void LogPotentialError(string errorOrigin, bool errorOccured)
        {
            if(errorOccured)
            {
                if (!Errors.Contains(errorOrigin))
                    Errors.Add(errorOrigin);
            }
            else
            {
                if (Errors.Contains(errorOrigin))
                    Errors.Remove(errorOrigin);
            }

            if (Errors.Count > 0)
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        /// <summary>
        /// Add Class button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            string data = "";

            //Class Name
            data += "(Custom) "+textBox1.Text+"\r\n";
            //Professors name (first last)
            data += textBox3.Text + "\r\n";
            //Professors email 
            data += textBox4.Text + "\r\n";
            //Start/End Date (9/5/2019 - 12/19/2019)
            data += dateTimePicker1.Text + '-' + dateTimePicker2.Text  + " Lecture ";
            //Day (Monday - Friday)
            data += comboBox1.Text;
            //Optional 2nd Day
            data += (comboBox2.SelectedIndex > 0) ? (", " + comboBox2.Text + " ") : " ";
            //Time Period (11:00 AM - 1:45 PM)
            data += dateTimePicker4.Text + " - " + dateTimePicker3.Text + ", ";
            //Location (Robert Frost Hall Room 12)
            data += textBox2.Text + "\r\n";
            //Credits
            data += textBox5.Text + "\r\n\r\n\r\n";

            //Append to class data
            MainWindow.instance.AddCustomClass(data);

            Close();
        }

        /// <summary>
        /// Close window button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Entered Professor's full name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if(textBox3.Text.Length == 0)
            {
                LogPotentialError("ProfessorFullName", true);
                label8.ForeColor = Color.Red;
            }
            else
            {
                label8.ForeColor = Color.Black;
                LogPotentialError("ProfessorFullName", false);
            }
        }

        /// <summary>
        /// Entered Professor's email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length == 0)
            {
                LogPotentialError("ProfessorEmail", true);
                label9.ForeColor = Color.Red;
            }
            else
            {
                label9.ForeColor = Color.Black;
                LogPotentialError("ProfessorEmail", false);
            }
        }

        /// <summary>
        /// Entered Credits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length == 0 || !System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "[0-9]"))
            {
                LogPotentialError("ClassCredits", true);
                label10.ForeColor = Color.Red;
            }
            else
            {
                label10.ForeColor = Color.Black;
                LogPotentialError("ClassCredits", false);
            }
        }

        /// <summary>
        /// Entered location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            //ONE COMMA ALLOWED!
            int numOfCommas = (textBox2.Text.Length - textBox2.Text.Replace(",", "").Length);

            if (textBox2.Text.Length == 0 || numOfCommas > 1)
            {
                LogPotentialError("ClassLocation", true);
                label4.ForeColor = Color.Red;
            }
            else
            {
                label4.ForeColor = Color.Black;
                LogPotentialError("ClassLocation", false);
            }
        }

        /// <summary>
        /// Entered name of Class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                LogPotentialError("ClassName", true);
                label2.ForeColor = Color.Red;
            }
            else
            {
                label2.ForeColor = Color.Black;
                LogPotentialError("ClassName", false);
            }
        }

        /// <summary>
        /// Changed the day of the class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                LogPotentialError("ClassDay", true);
                label1.ForeColor = Color.Red;
            }
            else
            {
                label1.ForeColor = Color.Black;
                LogPotentialError("ClassDay", false);
            }
        }

        /// <summary>
        /// Changed start date value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //Start date must be less than end date
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                LogPotentialError("ClassTime", true);
                label6.ForeColor = Color.Red;
                label7.ForeColor = Color.Red;
            }
            else
            {
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
                LogPotentialError("ClassTime", false);
            }
        }

        /// <summary>
        /// Changed end date value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //Start date must be less than end date
            if (dateTimePicker1.Value >= dateTimePicker2.Value)
            {
                LogPotentialError("ClassTime", true);
                label6.ForeColor = Color.Red;
                label7.ForeColor = Color.Red;
            }
            else
            {
                label6.ForeColor = Color.Black;
                label7.ForeColor = Color.Black;
                LogPotentialError("ClassTime", false);
            }
        }

        /// <summary>
        /// Changed End time value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            //Start time must be less than end time
            if (dateTimePicker4.Value >= dateTimePicker3.Value)
            {
                LogPotentialError("ClassDate", true);
                label3.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
            }
            else
            {
                label3.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                LogPotentialError("ClassDate", false);
            }
        }

        /// <summary>
        /// Changed Start time value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            //Start time must be less than end time
            if (dateTimePicker4.Value >= dateTimePicker3.Value)
            {
                LogPotentialError("ClassDate", true);
                label3.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
            }
            else
            {
                label3.ForeColor = Color.Black;
                label5.ForeColor = Color.Black;
                LogPotentialError("ClassDate", false);
            }
        }
    }
}
