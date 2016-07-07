namespace Box9.Leds.Manager
{
    partial class LedManager
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonAddServer = new System.Windows.Forms.Button();
            this.buttonRemoveServer = new System.Windows.Forms.Button();
            this.listBoxServers = new System.Windows.Forms.ListBox();
            this.labelServers = new System.Windows.Forms.Label();
            this.saveConfigurationDialog = new System.Windows.Forms.SaveFileDialog();
            this.loadConfigurationDialog = new System.Windows.Forms.OpenFileDialog();
            this.videoBrowserDialog = new System.Windows.Forms.OpenFileDialog();
            this.importVideoButton = new System.Windows.Forms.Button();
            this.labelVideoFilePath = new System.Windows.Forms.Label();
            this.labelVideo = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(716, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadConfigurationToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadConfigurationToolStripMenuItem
            // 
            this.loadConfigurationToolStripMenuItem.Name = "loadConfigurationToolStripMenuItem";
            this.loadConfigurationToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.loadConfigurationToolStripMenuItem.Text = "Load configuration...";
            this.loadConfigurationToolStripMenuItem.Click += new System.EventHandler(this.loadConfigurationToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.saveToolStripMenuItem.Text = "Save confuguration";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.saveAsToolStripMenuItem.Text = "Save configuration as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // buttonAddServer
            // 
            this.buttonAddServer.Location = new System.Drawing.Point(12, 301);
            this.buttonAddServer.Name = "buttonAddServer";
            this.buttonAddServer.Size = new System.Drawing.Size(75, 23);
            this.buttonAddServer.TabIndex = 2;
            this.buttonAddServer.Text = "Add new...";
            this.buttonAddServer.UseVisualStyleBackColor = true;
            this.buttonAddServer.Click += new System.EventHandler(this.buttonAddServer_Click);
            // 
            // buttonRemoveServer
            // 
            this.buttonRemoveServer.Location = new System.Drawing.Point(103, 301);
            this.buttonRemoveServer.Name = "buttonRemoveServer";
            this.buttonRemoveServer.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveServer.TabIndex = 3;
            this.buttonRemoveServer.Text = "Remove";
            this.buttonRemoveServer.UseVisualStyleBackColor = true;
            this.buttonRemoveServer.Click += new System.EventHandler(this.buttonRemoveServer_Click);
            // 
            // listBoxServers
            // 
            this.listBoxServers.FormattingEnabled = true;
            this.listBoxServers.Location = new System.Drawing.Point(12, 65);
            this.listBoxServers.Name = "listBoxServers";
            this.listBoxServers.Size = new System.Drawing.Size(166, 225);
            this.listBoxServers.TabIndex = 4;
            // 
            // labelServers
            // 
            this.labelServers.AutoSize = true;
            this.labelServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelServers.Location = new System.Drawing.Point(12, 39);
            this.labelServers.Name = "labelServers";
            this.labelServers.Size = new System.Drawing.Size(43, 13);
            this.labelServers.TabIndex = 5;
            this.labelServers.Text = "Servers";
            // 
            // saveConfigurationDialog
            // 
            this.saveConfigurationDialog.Filter = "|*.ledConf";
            // 
            // loadConfigurationDialog
            // 
            this.loadConfigurationDialog.Filter = "|*.ledConf";
            // 
            // videoBrowserDialog
            // 
            this.videoBrowserDialog.Filter = "|*.mp4";
            // 
            // importVideoButton
            // 
            this.importVideoButton.Location = new System.Drawing.Point(245, 65);
            this.importVideoButton.Name = "importVideoButton";
            this.importVideoButton.Size = new System.Drawing.Size(103, 23);
            this.importVideoButton.TabIndex = 6;
            this.importVideoButton.Text = "Import video...";
            this.importVideoButton.UseVisualStyleBackColor = true;
            this.importVideoButton.Click += new System.EventHandler(this.importVideoButton_Click);
            // 
            // labelVideoFilePath
            // 
            this.labelVideoFilePath.AutoSize = true;
            this.labelVideoFilePath.Location = new System.Drawing.Point(367, 70);
            this.labelVideoFilePath.Name = "labelVideoFilePath";
            this.labelVideoFilePath.Size = new System.Drawing.Size(30, 13);
            this.labelVideoFilePath.TabIndex = 7;
            this.labelVideoFilePath.Text = "temp";
            // 
            // labelVideo
            // 
            this.labelVideo.AutoSize = true;
            this.labelVideo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVideo.Location = new System.Drawing.Point(242, 39);
            this.labelVideo.Name = "labelVideo";
            this.labelVideo.Size = new System.Drawing.Size(34, 13);
            this.labelVideo.TabIndex = 8;
            this.labelVideo.Text = "Video";
            // 
            // LedManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 346);
            this.Controls.Add(this.labelVideo);
            this.Controls.Add(this.labelVideoFilePath);
            this.Controls.Add(this.importVideoButton);
            this.Controls.Add(this.labelServers);
            this.Controls.Add(this.listBoxServers);
            this.Controls.Add(this.buttonRemoveServer);
            this.Controls.Add(this.buttonAddServer);
            this.Controls.Add(this.menuStrip1);
            this.Name = "LedManager";
            this.Text = "LED Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.Button buttonAddServer;
        private System.Windows.Forms.Button buttonRemoveServer;
        private System.Windows.Forms.ListBox listBoxServers;
        private System.Windows.Forms.Label labelServers;
        private System.Windows.Forms.SaveFileDialog saveConfigurationDialog;
        private System.Windows.Forms.OpenFileDialog loadConfigurationDialog;
        private System.Windows.Forms.OpenFileDialog videoBrowserDialog;
        private System.Windows.Forms.Button importVideoButton;
        private System.Windows.Forms.Label labelVideoFilePath;
        private System.Windows.Forms.Label labelVideo;
    }
}

