namespace TextEditor.Forms
{
    partial class AboutForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelAppName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.Button buttonOk;

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
            this.labelAppName = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelYear = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelAppName
            // 
            this.labelAppName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAppName.AutoSize = true;
            this.labelAppName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.labelAppName.Location = new System.Drawing.Point(67, 9);
            this.labelAppName.Name = "labelAppName";
            this.labelAppName.Size = new System.Drawing.Size(216, 24);
            this.labelAppName.TabIndex = 0;
            this.labelAppName.Text = "Текстовый редактор";
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(12, 189);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(62, 13);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "Версия 1.0";
            // 
            // labelAuthor
            // 
            this.labelAuthor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(55, 57);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(228, 13);
            this.labelAuthor.TabIndex = 2;
            this.labelAuthor.Text = "Разработчик: Болтенков Евгений Олегович";
            // 
            // labelYear
            // 
            this.labelYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(279, 189);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(43, 13);
            this.labelYear.TabIndex = 3;
            this.labelYear.Text = "© 2026";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(130, 160);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 211);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelYear);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.labelAppName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}