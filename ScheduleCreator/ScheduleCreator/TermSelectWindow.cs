using System;
using System.Data;
using System.Windows.Forms;

namespace ScheduleCreator
{
    public partial class TermSelectWindow : Form
    {
        public TermSelectWindow()
        {
            InitializeComponent();
            ControlBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private string[] valuesFound;
        
        /// <summary>
        /// Adds every option that was found on the login window to a combo box
        /// </summary>
        /// <param name="options"></param>
        public void LoadComboBoxData(string[] options, string[] vals)
        {
            valuesFound = vals;

            DataTable data = new DataTable();
            data.Columns.Add("Display");
           

            for (int i = 0; i < options.Length; i++)
            {
                data.Rows.Add(options[i]);
            }

            comboBox1.DisplayMember = "Display";
            comboBox1.DataSource = new BindingSource(data, null);
        }

        /// <summary>
        /// Select Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                button1.Text = "Loading...";
                button1.Enabled = false;

                mySNHULoginWindow.instance.LoadSelectedTerm(comboBox1.SelectedText, valuesFound[comboBox1.SelectedIndex]);

                Close();
            }
            else
            {
                MessageBox.Show("No term was selected!");
            }
        }

        /// <summary>
        /// Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button2_Click(object sender, EventArgs e)
        {
            mySNHULoginWindow.instance.ResetWindowUI();
            Close();
        }
    }
}
