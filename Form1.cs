using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DashHigh
{
    public partial class Form1 : Form
    {
        private const string SavedExecutablesFile = "saveddir.conf";
        private bool isUpdatingExePath = false;
        //private NotifyIcon trayIcon;  // Tray icon object
        //private ContextMenuStrip trayMenu;  // Menu for the tray icon

        public Form1()
        {
            InitializeComponent();
            txtExePath.TextChanged += new System.EventHandler(this.txtExePath_TextChanged);

            // Initialize the tray icon and menu
            //InitializeTrayIcon();
        }

        //private void InitializeTrayIcon()
        //{
        //    trayMenu = new ContextMenuStrip();
        //    trayMenu.Items.Add("Open", null, TrayMenuOpen_Click);
        //    trayMenu.Items.Add("Exit", null, TrayMenuExit_Click);

        //    trayIcon = new NotifyIcon();
        //    trayIcon.Text = "Dash High";
        //    trayIcon.Icon = SystemIcons.Information;  // You can change the icon here
        //    trayIcon.ContextMenuStrip = trayMenu;
        //    trayIcon.Visible = true;

        //    // When the tray icon is double-clicked, show the form
        //    trayIcon.DoubleClick += TrayIcon_DoubleClick;
        //}

        //private void TrayIcon_DoubleClick(object sender, EventArgs e)
        //{
        //    // Show the form when the tray icon is double-clicked
        //    this.Show();
        //    this.WindowState = FormWindowState.Normal;
        //}

        //private void TrayMenuOpen_Click(object sender, EventArgs e)
        //{
        //    // Show the form when "Open" is clicked from the tray menu
        //    this.Show();
        //    this.WindowState = FormWindowState.Normal;
        //}

        //private void TrayMenuExit_Click(object sender, EventArgs e)
        //{
        //    // Close the application when "Exit" is clicked from the tray menu
        //    Application.Exit();
        //}

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtExePath.Text = openFileDialog.FileName;
            }
        }

        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            string configFilePath = Path.Combine(Application.StartupPath, "saveddir.conf");

            if (File.Exists(configFilePath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = configFilePath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Config file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveExe_Click(object sender, EventArgs e)
        {
            string exePath = txtExePath.Text.Trim('\"');
            string title = Path.GetFileNameWithoutExtension(exePath);

            if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath) || !exePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Invalid executable path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(SavedExecutablesFile))
            {
                string[] savedEntries = File.ReadAllLines(SavedExecutablesFile);
                foreach (string entry in savedEntries)
                {
                    if (entry.Contains(exePath))
                    {
                        MessageBox.Show("Executable already saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(SavedExecutablesFile, true))
            {
                writer.WriteLine($"{exePath} : title : {title}");
            }

            MessageBox.Show($"Executable \"{title}\" saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdatePriority_Click(object sender, EventArgs e)
        {
            string? priority = comboBoxPriority.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(priority))
            {
                MessageBox.Show("Select a valid priority.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string exePath = txtExePath.Text.Trim('\"');
            if (string.IsNullOrEmpty(exePath))
            {
                List<string> exePaths = ReadAllExecutablePathsFromConfig();

                if (exePaths.Count == 0)
                {
                    MessageBox.Show("No executables found in config.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var path in exePaths)
                {
                    try
                    {
                        ChangePriority(path, priority);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error changing priority for {Path.GetFileNameWithoutExtension(path)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                MessageBox.Show("Priority updated for all executables.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath) || !exePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Invalid executable path.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                ChangePriority(exePath, priority);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing priority for {Path.GetFileNameWithoutExtension(exePath)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> ReadAllExecutablePathsFromConfig()
        {
            List<string> exePaths = new List<string>();
            string configFilePath = Path.Combine(Application.StartupPath, "saveddir.conf");

            if (File.Exists(configFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(configFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(new[] { " : " }, StringSplitOptions.None);
                        if (parts.Length > 1)
                        {
                            exePaths.Add(parts[0].Trim());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading config: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Config file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return exePaths;
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            string? priority = comboBoxPriority.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(priority))
            {
                MessageBox.Show("Select a priority.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(SavedExecutablesFile))
            {
                string[] savedEntries = File.ReadAllLines(SavedExecutablesFile);
                foreach (string entry in savedEntries)
                {
                    string[] parts = entry.Split(new string[] { " : title : " }, StringSplitOptions.None);
                    if (parts.Length >= 1)
                    {
                        string exePath = parts[0].Trim();
                        if (File.Exists(exePath) && exePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
                        {
                            ChangePriority(exePath, priority);
                        }
                    }
                }
                MessageBox.Show("Priority updated for all executables.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Config file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangePriority(string exePath, string? priority)
        {
            try
            {
                string exeName = Path.GetFileNameWithoutExtension(exePath);
                var processList = Process.GetProcessesByName(exeName);

                if (processList.Length == 0)
                {
                    MessageBox.Show($"No instance found for {exeName}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var process = processList[0];
                switch (priority)
                {
                    case "Idle":
                        process.PriorityClass = ProcessPriorityClass.Idle;
                        break;
                    case "Normal":
                        process.PriorityClass = ProcessPriorityClass.Normal;
                        break;
                    case "High":
                        process.PriorityClass = ProcessPriorityClass.High;
                        break;
                    case "Realtime":
                        process.PriorityClass = ProcessPriorityClass.RealTime;
                        break;
                    default:
                        MessageBox.Show("Unknown priority.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                MessageBox.Show($"Priority for {exeName} set to {priority}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting priority: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtExePath_TextChanged(object sender, EventArgs e)
        {
            if (isUpdatingExePath)
                return;
            isUpdatingExePath = true;
            string exePath = txtExePath.Text.Trim('\"');

            if (File.Exists(exePath) && exePath.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
            {
                btnUpdatePriority.Enabled = true;
            }
            else
            {
                btnUpdatePriority.Enabled = false;
            }
            isUpdatingExePath = false;
        }
    }
}
