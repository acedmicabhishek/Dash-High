namespace DashHigh
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtExePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ComboBox comboBoxPriority;
        private System.Windows.Forms.Button btnSaveExe;
        private System.Windows.Forms.Button btnUpdatePriority;
        private System.Windows.Forms.Label lblExePath;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnOpenConfig;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtExePath = new TextBox();
            btnBrowse = new Button();
            comboBoxPriority = new ComboBox();
            btnSaveExe = new Button();
            btnUpdatePriority = new Button();
            lblExePath = new Label();
            lblPriority = new Label();
            openFileDialog = new OpenFileDialog();
            btnOpenConfig = new Button();
            SuspendLayout();
            // 
            // txtExePath
            // 
            txtExePath.Location = new Point(12, 38);
            txtExePath.Name = "txtExePath";
            txtExePath.Size = new Size(156, 23);
            txtExePath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            btnBrowse.Location = new Point(174, 38);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 23);
            btnBrowse.TabIndex = 2;
            btnBrowse.Text = "Browse";
            btnBrowse.Click += btnBrowse_Click;
            // 
            // comboBoxPriority
            // 
            comboBoxPriority.DisplayMember = "4";
            comboBoxPriority.FormattingEnabled = true;
            comboBoxPriority.Items.AddRange(new object[] { "Idle", "Normal", "High", "Realtime" });
            comboBoxPriority.Location = new Point(12, 99);
            comboBoxPriority.Name = "comboBoxPriority";
            comboBoxPriority.Size = new Size(156, 23);
            comboBoxPriority.TabIndex = 3;
            // 
            // btnSaveExe
            // 
            btnSaveExe.Location = new Point(12, 128);
            btnSaveExe.Name = "btnSaveExe";
            btnSaveExe.Size = new Size(75, 23);
            btnSaveExe.TabIndex = 4;
            btnSaveExe.Text = "Save";
            btnSaveExe.Click += btnSaveExe_Click;
            // 
            // btnUpdatePriority
            // 
            btnUpdatePriority.Location = new Point(93, 128);
            btnUpdatePriority.Name = "btnUpdatePriority";
            btnUpdatePriority.Size = new Size(75, 23);
            btnUpdatePriority.TabIndex = 5;
            btnUpdatePriority.Text = "Apply";
            btnUpdatePriority.Click += btnUpdatePriority_Click;
            // 
            // lblExePath
            // 
            lblExePath.Location = new Point(12, 18);
            lblExePath.Name = "lblExePath";
            lblExePath.Size = new Size(75, 23);
            lblExePath.TabIndex = 1;
            lblExePath.Text = "Add Games";
            // 
            // lblPriority
            // 
            lblPriority.Location = new Point(12, 73);
            lblPriority.Name = "lblPriority";
            lblPriority.Size = new Size(122, 22);
            lblPriority.TabIndex = 0;
            lblPriority.Text = "CPU-Mode (Priority)";
            // 
            // btnOpenConfig
            // 
            btnOpenConfig.Location = new Point(174, 128);
            btnOpenConfig.Name = "btnOpenConfig";
            btnOpenConfig.Size = new Size(75, 23);
            btnOpenConfig.TabIndex = 7;
            btnOpenConfig.Text = "Open Config";
            btnOpenConfig.Click += btnOpenConfig_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(269, 180);
            Controls.Add(btnOpenConfig);
            Controls.Add(btnUpdatePriority);
            Controls.Add(btnSaveExe);
            Controls.Add(comboBoxPriority);
            Controls.Add(btnBrowse);
            Controls.Add(txtExePath);
            Controls.Add(lblPriority);
            Controls.Add(lblExePath);
            Name = "Form1";
            Text = "Dash High";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
