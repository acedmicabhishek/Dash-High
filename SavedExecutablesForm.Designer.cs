namespace DashHigh
{
    partial class SavedExecutablesForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListBox listBoxTitles;

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
            this.listBoxTitles = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxTitles
            // 
            this.listBoxTitles.FormattingEnabled = true;
            this.listBoxTitles.ItemHeight = 15;
            this.listBoxTitles.Location = new System.Drawing.Point(12, 12);
            this.listBoxTitles.Name = "listBoxTitles";
            this.listBoxTitles.Size = new System.Drawing.Size(360, 229);
            this.listBoxTitles.TabIndex = 0;
            // 
            // SavedExecutablesForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 261);
            this.Controls.Add(this.listBoxTitles);
            this.Name = "SavedExecutablesForm";
            this.Text = "Saved Executables Titles";
            this.ResumeLayout(false);
        }
    }
}
