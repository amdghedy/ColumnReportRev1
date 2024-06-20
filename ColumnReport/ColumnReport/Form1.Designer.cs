namespace ColumnReport
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.browseButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.linkedInButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(373, 16);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(96, 46);
            this.browseButton.TabIndex = 0;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(47, 87);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(92, 47);
            this.exportButton.TabIndex = 1;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(213, 87);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(86, 47);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // linkedInButton
            // 
            this.linkedInButton.Image = global::ColumnReport.Properties.Resources.LinkedIn_logo_initials;
            this.linkedInButton.Location = new System.Drawing.Point(383, 72);
            this.linkedInButton.Name = "linkedInButton";
            this.linkedInButton.Size = new System.Drawing.Size(77, 77);
            this.linkedInButton.TabIndex = 3;
            this.linkedInButton.UseVisualStyleBackColor = true;
            this.linkedInButton.Click += new System.EventHandler(this.linkedInButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(12, 22);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(336, 26);
            this.pathTextBox.TabIndex = 4;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(481, 161);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.linkedInButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.browseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button linkedInButton;
        private System.Windows.Forms.TextBox pathTextBox;
    }
}
