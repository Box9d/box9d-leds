namespace Box9.Leds.Manager.Forms
{
    partial class DisplayOnlyServerForm
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
            this.videoControlPanel = new System.Windows.Forms.Panel();
            this.stopButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.importVideoButton = new System.Windows.Forms.Button();
            this.browseForVideoButton = new System.Windows.Forms.Button();
            this.videoBrowserDialog = new System.Windows.Forms.OpenFileDialog();
            this.displayPanel = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.videoControlPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoControlPanel
            // 
            this.videoControlPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.videoControlPanel.BackColor = System.Drawing.SystemColors.Control;
            this.videoControlPanel.Controls.Add(this.stopButton);
            this.videoControlPanel.Controls.Add(this.playButton);
            this.videoControlPanel.Controls.Add(this.fileNameLabel);
            this.videoControlPanel.Controls.Add(this.importVideoButton);
            this.videoControlPanel.Controls.Add(this.browseForVideoButton);
            this.videoControlPanel.Location = new System.Drawing.Point(-9, 206);
            this.videoControlPanel.Name = "videoControlPanel";
            this.videoControlPanel.Size = new System.Drawing.Size(302, 121);
            this.videoControlPanel.TabIndex = 0;
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(103, 13);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 4;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new System.Drawing.Point(22, 13);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 3;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Location = new System.Drawing.Point(130, 87);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(0, 13);
            this.fileNameLabel.TabIndex = 2;
            // 
            // importVideoButton
            // 
            this.importVideoButton.Location = new System.Drawing.Point(21, 77);
            this.importVideoButton.Name = "importVideoButton";
            this.importVideoButton.Size = new System.Drawing.Size(103, 23);
            this.importVideoButton.TabIndex = 1;
            this.importVideoButton.Text = "Import video...";
            this.importVideoButton.UseVisualStyleBackColor = true;
            this.importVideoButton.Click += new System.EventHandler(this.importVideoButton_Click);
            // 
            // browseForVideoButton
            // 
            this.browseForVideoButton.Location = new System.Drawing.Point(12, 226);
            this.browseForVideoButton.Name = "browseForVideoButton";
            this.browseForVideoButton.Size = new System.Drawing.Size(112, 23);
            this.browseForVideoButton.TabIndex = 0;
            this.browseForVideoButton.Text = "Import video...";
            this.browseForVideoButton.UseVisualStyleBackColor = true;
            // 
            // videoBrowserDialog
            // 
            this.videoBrowserDialog.Filter = "|*.mp4";
            // 
            // displayPanel
            // 
            this.displayPanel.BackColor = System.Drawing.Color.Black;
            this.displayPanel.Location = new System.Drawing.Point(3, 2);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(278, 141);
            this.displayPanel.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(284, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // DisplayOnlyServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(284, 375);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.displayPanel);
            this.Controls.Add(this.videoControlPanel);
            this.Name = "DisplayOnlyServerForm";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.ServerForm_Load);
            this.videoControlPanel.ResumeLayout(false);
            this.videoControlPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel videoControlPanel;
        private System.Windows.Forms.Button browseForVideoButton;
        private System.Windows.Forms.Button importVideoButton;
        private System.Windows.Forms.OpenFileDialog videoBrowserDialog;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}