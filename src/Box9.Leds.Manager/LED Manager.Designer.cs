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
            this.newConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.labelPlayback = new System.Windows.Forms.Label();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.stripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.trackBarStartTime = new System.Windows.Forms.TrackBar();
            this.labelStartTime = new System.Windows.Forms.Label();
            this.trackBarBrightness = new System.Windows.Forms.TrackBar();
            this.labelBrightness = new System.Windows.Forms.Label();
            this.checkBoxDisplayOutputOnScreen = new System.Windows.Forms.CheckBox();
            this.buttonValidatePlayback = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(742, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newConfigurationToolStripMenuItem,
            this.loadConfigurationToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newConfigurationToolStripMenuItem
            // 
            this.newConfigurationToolStripMenuItem.Name = "newConfigurationToolStripMenuItem";
            this.newConfigurationToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.newConfigurationToolStripMenuItem.Text = "New configuration";
            this.newConfigurationToolStripMenuItem.Click += new System.EventHandler(this.newConfigurationToolStripMenuItem_Click);
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
            this.buttonAddServer.Location = new System.Drawing.Point(12, 293);
            this.buttonAddServer.Name = "buttonAddServer";
            this.buttonAddServer.Size = new System.Drawing.Size(75, 23);
            this.buttonAddServer.TabIndex = 2;
            this.buttonAddServer.Text = "Add new...";
            this.buttonAddServer.UseVisualStyleBackColor = true;
            this.buttonAddServer.Click += new System.EventHandler(this.buttonAddServer_Click);
            // 
            // buttonRemoveServer
            // 
            this.buttonRemoveServer.Location = new System.Drawing.Point(113, 293);
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
            this.listBoxServers.Size = new System.Drawing.Size(176, 160);
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
            this.labelVideoFilePath.Location = new System.Drawing.Point(246, 104);
            this.labelVideoFilePath.Name = "labelVideoFilePath";
            this.labelVideoFilePath.Size = new System.Drawing.Size(201, 59);
            this.labelVideoFilePath.TabIndex = 7;
            this.labelVideoFilePath.Text = "No video selected";
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
            // labelPlayback
            // 
            this.labelPlayback.AutoSize = true;
            this.labelPlayback.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayback.Location = new System.Drawing.Point(484, 39);
            this.labelPlayback.Name = "labelPlayback";
            this.labelPlayback.Size = new System.Drawing.Size(51, 13);
            this.labelPlayback.TabIndex = 10;
            this.labelPlayback.Text = "Playback";
            // 
            // buttonPlay
            // 
            this.buttonPlay.Enabled = false;
            this.buttonPlay.Location = new System.Drawing.Point(487, 293);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 12;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(596, 293);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 13;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 335);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(742, 22);
            this.statusStrip.TabIndex = 14;
            this.statusStrip.Text = "statusStrip";
            // 
            // stripStatusLabel
            // 
            this.stripStatusLabel.Name = "stripStatusLabel";
            this.stripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // trackBarStartTime
            // 
            this.trackBarStartTime.Enabled = false;
            this.trackBarStartTime.Location = new System.Drawing.Point(490, 65);
            this.trackBarStartTime.Name = "trackBarStartTime";
            this.trackBarStartTime.Size = new System.Drawing.Size(184, 45);
            this.trackBarStartTime.TabIndex = 15;
            // 
            // labelStartTime
            // 
            this.labelStartTime.AutoSize = true;
            this.labelStartTime.Location = new System.Drawing.Point(487, 97);
            this.labelStartTime.Name = "labelStartTime";
            this.labelStartTime.Size = new System.Drawing.Size(111, 13);
            this.labelStartTime.TabIndex = 16;
            this.labelStartTime.Text = "Start playback at 0:00";
            // 
            // trackBarBrightness
            // 
            this.trackBarBrightness.LargeChange = 10;
            this.trackBarBrightness.Location = new System.Drawing.Point(487, 128);
            this.trackBarBrightness.Maximum = 100;
            this.trackBarBrightness.Name = "trackBarBrightness";
            this.trackBarBrightness.Size = new System.Drawing.Size(184, 45);
            this.trackBarBrightness.SmallChange = 10;
            this.trackBarBrightness.TabIndex = 17;
            this.trackBarBrightness.TickFrequency = 10;
            this.trackBarBrightness.Value = 100;
            // 
            // labelBrightness
            // 
            this.labelBrightness.AutoSize = true;
            this.labelBrightness.Location = new System.Drawing.Point(487, 174);
            this.labelBrightness.Name = "labelBrightness";
            this.labelBrightness.Size = new System.Drawing.Size(85, 13);
            this.labelBrightness.TabIndex = 18;
            this.labelBrightness.Text = "Brightness 100%";
            // 
            // checkBoxDisplayOutputOnScreen
            // 
            this.checkBoxDisplayOutputOnScreen.AutoSize = true;
            this.checkBoxDisplayOutputOnScreen.Location = new System.Drawing.Point(487, 208);
            this.checkBoxDisplayOutputOnScreen.Name = "checkBoxDisplayOutputOnScreen";
            this.checkBoxDisplayOutputOnScreen.Size = new System.Drawing.Size(143, 17);
            this.checkBoxDisplayOutputOnScreen.TabIndex = 19;
            this.checkBoxDisplayOutputOnScreen.Text = "Display output on screen";
            this.checkBoxDisplayOutputOnScreen.UseVisualStyleBackColor = true;
            this.checkBoxDisplayOutputOnScreen.CheckedChanged += new System.EventHandler(this.checkBoxDisplayOutputOnScreen_CheckedChanged);
            // 
            // buttonValidatePlayback
            // 
            this.buttonValidatePlayback.Location = new System.Drawing.Point(487, 254);
            this.buttonValidatePlayback.Name = "buttonValidatePlayback";
            this.buttonValidatePlayback.Size = new System.Drawing.Size(122, 23);
            this.buttonValidatePlayback.TabIndex = 20;
            this.buttonValidatePlayback.Text = "Validate playback";
            this.buttonValidatePlayback.UseVisualStyleBackColor = true;
            this.buttonValidatePlayback.Click += new System.EventHandler(this.buttonValidatePlayback_Click);
            // 
            // LedManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 357);
            this.Controls.Add(this.buttonValidatePlayback);
            this.Controls.Add(this.checkBoxDisplayOutputOnScreen);
            this.Controls.Add(this.labelBrightness);
            this.Controls.Add(this.trackBarBrightness);
            this.Controls.Add(this.labelStartTime);
            this.Controls.Add(this.trackBarStartTime);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.labelPlayback);
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
            this.Load += new System.EventHandler(this.LedManager_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBrightness)).EndInit();
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
        private System.Windows.Forms.Label labelPlayback;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.ToolStripMenuItem newConfigurationToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stripStatusLabel;
        private System.Windows.Forms.TrackBar trackBarStartTime;
        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.TrackBar trackBarBrightness;
        private System.Windows.Forms.Label labelBrightness;
        private System.Windows.Forms.CheckBox checkBoxDisplayOutputOnScreen;
        private System.Windows.Forms.Button buttonValidatePlayback;
    }
}

