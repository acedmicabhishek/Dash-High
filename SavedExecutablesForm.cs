using System;
using System.IO;
using System.Windows.Forms;

namespace DashHigh
{
    public partial class SavedExecutablesForm : Form
    {
        private const string SavedExecutablesFile = "saveddir.conf";

        public SavedExecutablesForm()
        {
            InitializeComponent();
            LoadSavedTitles();
        }

        private void LoadSavedTitles()
        {
            // Clear the list before loading new titles
            listBoxTitles.Items.Clear();

            if (!File.Exists(SavedExecutablesFile))
            {
                MessageBox.Show("No saved executables found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] savedEntries = File.ReadAllLines(SavedExecutablesFile);
            foreach (string entry in savedEntries)
            {
                // Split the entry into exePath and title
                string[] parts = entry.Split(new string[] { " : " }, StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    string title = parts[1].Replace("title : ", "").Trim();
                    listBoxTitles.Items.Add(title);
                }
            }
        }
    }
}
