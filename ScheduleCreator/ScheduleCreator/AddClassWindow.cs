using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleCreator
{
    public partial class AddClassWindow : Form
    {
        private List<string> Errors;

        public AddClassWindow()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
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
        /// Add Class button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            string data = "";
            //Day (Monday - Friday)
            //Class Name (Learning Fellow: Intro to Programming)
            //Time Period (11:00 AM - 1:45 PM)
            //Location (Robert Frost Hall Room 12)
            //Start/End Date (9/5/2019 - 12/19/2019)
            //Professor (first last (f.l@snhu.edu))
            //Credits 3
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
            //Label8
        }

        /// <summary>
        /// Entered Professor's email
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            //Label9
        }

        /// <summary>
        /// Entered Credits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            //Label10
        }

        /// <summary>
        /// Entered location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            //Label4
        }

        /// <summary>
        /// Entered name of Class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            //Label2
        }

        /// <summary>
        /// Changed the day of the class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Label1
        }

        /// <summary>
        /// Changed start date value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //Label6 and 7
            //Start date must be less than end date
        }

        /// <summary>
        /// Changed end date value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //Label6 and 7
        }

        /// <summary>
        /// Changed End time value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            //Label3 and 5
            //Start time must be less than end time
        }

        /// <summary>
        /// Changed Start time value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            //Label3 and 5
        }
    }
}
