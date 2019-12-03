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
            //Time Period (11:00 - 1:45)
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
    }
}
