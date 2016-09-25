namespace Glimr.Plugins.ManagementApi.UploadTool
{
    partial class Upload
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelFolderName = new System.Windows.Forms.Label();
            this.buttonUploadPluginFolder = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonUploadFiles = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelFolderName
            // 
            this.labelFolderName.AutoSize = true;
            this.labelFolderName.Location = new System.Drawing.Point(172, 22);
            this.labelFolderName.Name = "labelFolderName";
            this.labelFolderName.Size = new System.Drawing.Size(93, 13);
            this.labelFolderName.TabIndex = 0;
            this.labelFolderName.Text = "No folder selected";
            // 
            // buttonUploadPluginFolder
            // 
            this.buttonUploadPluginFolder.Location = new System.Drawing.Point(12, 17);
            this.buttonUploadPluginFolder.Name = "buttonUploadPluginFolder";
            this.buttonUploadPluginFolder.Size = new System.Drawing.Size(144, 23);
            this.buttonUploadPluginFolder.TabIndex = 1;
            this.buttonUploadPluginFolder.Text = "Browse for plugin folder...";
            this.buttonUploadPluginFolder.UseVisualStyleBackColor = true;
            this.buttonUploadPluginFolder.Click += new System.EventHandler(this.buttonUploadPluginFolder_Click);
            // 
            // buttonUploadFiles
            // 
            this.buttonUploadFiles.Enabled = false;
            this.buttonUploadFiles.Location = new System.Drawing.Point(13, 89);
            this.buttonUploadFiles.Name = "buttonUploadFiles";
            this.buttonUploadFiles.Size = new System.Drawing.Size(143, 23);
            this.buttonUploadFiles.TabIndex = 2;
            this.buttonUploadFiles.Text = "Upload Files";
            this.buttonUploadFiles.UseVisualStyleBackColor = true;
            this.buttonUploadFiles.Click += new System.EventHandler(this.buttonUploadFiles_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(175, 98);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 3;
            // 
            // Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 124);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonUploadFiles);
            this.Controls.Add(this.buttonUploadPluginFolder);
            this.Controls.Add(this.labelFolderName);
            this.Name = "Upload";
            this.Text = "Upload plugin folder";
            this.Load += new System.EventHandler(this.Upload_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFolderName;
        private System.Windows.Forms.Button buttonUploadPluginFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button buttonUploadFiles;
        private System.Windows.Forms.Label labelStatus;
    }
}

