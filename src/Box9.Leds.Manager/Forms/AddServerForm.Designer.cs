namespace Box9.Leds.Manager.Forms
{
    partial class AddServerForm
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
            this.scanForServersButton = new System.Windows.Forms.Button();
            this.availableServersList = new System.Windows.Forms.ListBox();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.searchProgress = new System.Windows.Forms.ProgressBar();
            this.cancel = new System.Windows.Forms.Button();
            this.labelStartXPercent = new System.Windows.Forms.Label();
            this.labelStartYPercentage = new System.Windows.Forms.Label();
            this.labelPercentX = new System.Windows.Forms.Label();
            this.labelPercentY = new System.Windows.Forms.Label();
            this.labelNumberOfPixelsX = new System.Windows.Forms.Label();
            this.labelNumberOfPixelsY = new System.Windows.Forms.Label();
            this.labelLayout = new System.Windows.Forms.Label();
            this.labelVideoSplitting = new System.Windows.Forms.Label();
            this.startAtPercentageX = new System.Windows.Forms.ComboBox();
            this.startAtPercentageY = new System.Windows.Forms.ComboBox();
            this.widthPercentage = new System.Windows.Forms.ComboBox();
            this.heightPercentage = new System.Windows.Forms.ComboBox();
            this.textBoxXPixels = new System.Windows.Forms.TextBox();
            this.textBoxYPixels = new System.Windows.Forms.TextBox();
            this.labelLedMapping = new System.Windows.Forms.Label();
            this.buttonConfigureLedMapping = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // scanForServersButton
            // 
            this.scanForServersButton.Location = new System.Drawing.Point(12, 12);
            this.scanForServersButton.Name = "scanForServersButton";
            this.scanForServersButton.Size = new System.Drawing.Size(110, 23);
            this.scanForServersButton.TabIndex = 0;
            this.scanForServersButton.Text = "Scan for servers...";
            this.scanForServersButton.UseVisualStyleBackColor = true;
            // 
            // availableServersList
            // 
            this.availableServersList.FormattingEnabled = true;
            this.availableServersList.Location = new System.Drawing.Point(12, 50);
            this.availableServersList.Name = "availableServersList";
            this.availableServersList.Size = new System.Drawing.Size(251, 290);
            this.availableServersList.TabIndex = 1;
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Enabled = false;
            this.buttonConfirm.Location = new System.Drawing.Point(332, 327);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(82, 23);
            this.buttonConfirm.TabIndex = 2;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            // 
            // searchProgress
            // 
            this.searchProgress.Location = new System.Drawing.Point(129, 13);
            this.searchProgress.Name = "searchProgress";
            this.searchProgress.Size = new System.Drawing.Size(134, 23);
            this.searchProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.searchProgress.TabIndex = 3;
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(420, 327);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // labelStartXPercent
            // 
            this.labelStartXPercent.AutoSize = true;
            this.labelStartXPercent.Location = new System.Drawing.Point(285, 150);
            this.labelStartXPercent.Name = "labelStartXPercent";
            this.labelStartXPercent.Size = new System.Drawing.Size(152, 13);
            this.labelStartXPercent.TabIndex = 5;
            this.labelStartXPercent.Text = "Start at percentage (horizontal)";
            // 
            // labelStartYPercentage
            // 
            this.labelStartYPercentage.AutoSize = true;
            this.labelStartYPercentage.Location = new System.Drawing.Point(285, 172);
            this.labelStartYPercentage.Name = "labelStartYPercentage";
            this.labelStartYPercentage.Size = new System.Drawing.Size(141, 13);
            this.labelStartYPercentage.TabIndex = 6;
            this.labelStartYPercentage.Text = "Start at percentage (vertical)";
            // 
            // labelPercentX
            // 
            this.labelPercentX.AutoSize = true;
            this.labelPercentX.Location = new System.Drawing.Point(285, 194);
            this.labelPercentX.Name = "labelPercentX";
            this.labelPercentX.Size = new System.Drawing.Size(92, 13);
            this.labelPercentX.TabIndex = 7;
            this.labelPercentX.Text = "Width percentage";
            // 
            // labelPercentY
            // 
            this.labelPercentY.AutoSize = true;
            this.labelPercentY.Location = new System.Drawing.Point(285, 217);
            this.labelPercentY.Name = "labelPercentY";
            this.labelPercentY.Size = new System.Drawing.Size(95, 13);
            this.labelPercentY.TabIndex = 8;
            this.labelPercentY.Text = "Height percentage";
            // 
            // labelNumberOfPixelsX
            // 
            this.labelNumberOfPixelsX.AutoSize = true;
            this.labelNumberOfPixelsX.Location = new System.Drawing.Point(285, 75);
            this.labelNumberOfPixelsX.Name = "labelNumberOfPixelsX";
            this.labelNumberOfPixelsX.Size = new System.Drawing.Size(140, 13);
            this.labelNumberOfPixelsX.TabIndex = 9;
            this.labelNumberOfPixelsX.Text = "Number of Pixels (horizontal)";
            // 
            // labelNumberOfPixelsY
            // 
            this.labelNumberOfPixelsY.AutoSize = true;
            this.labelNumberOfPixelsY.Location = new System.Drawing.Point(285, 97);
            this.labelNumberOfPixelsY.Name = "labelNumberOfPixelsY";
            this.labelNumberOfPixelsY.Size = new System.Drawing.Size(129, 13);
            this.labelNumberOfPixelsY.TabIndex = 10;
            this.labelNumberOfPixelsY.Text = "Number of Pixels (vertical)";
            // 
            // labelLayout
            // 
            this.labelLayout.AutoSize = true;
            this.labelLayout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLayout.Location = new System.Drawing.Point(285, 50);
            this.labelLayout.Name = "labelLayout";
            this.labelLayout.Size = new System.Drawing.Size(39, 13);
            this.labelLayout.TabIndex = 11;
            this.labelLayout.Text = "Layout";
            // 
            // labelVideoSplitting
            // 
            this.labelVideoSplitting.AutoSize = true;
            this.labelVideoSplitting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVideoSplitting.Location = new System.Drawing.Point(285, 125);
            this.labelVideoSplitting.Name = "labelVideoSplitting";
            this.labelVideoSplitting.Size = new System.Drawing.Size(72, 13);
            this.labelVideoSplitting.TabIndex = 12;
            this.labelVideoSplitting.Text = "Video splitting";
            // 
            // startAtPercentageX
            // 
            this.startAtPercentageX.FormattingEnabled = true;
            this.startAtPercentageX.Location = new System.Drawing.Point(453, 147);
            this.startAtPercentageX.Name = "startAtPercentageX";
            this.startAtPercentageX.Size = new System.Drawing.Size(51, 21);
            this.startAtPercentageX.TabIndex = 13;
            // 
            // startAtPercentageY
            // 
            this.startAtPercentageY.FormattingEnabled = true;
            this.startAtPercentageY.Location = new System.Drawing.Point(453, 169);
            this.startAtPercentageY.Name = "startAtPercentageY";
            this.startAtPercentageY.Size = new System.Drawing.Size(51, 21);
            this.startAtPercentageY.TabIndex = 14;
            // 
            // widthPercentage
            // 
            this.widthPercentage.FormattingEnabled = true;
            this.widthPercentage.Location = new System.Drawing.Point(453, 191);
            this.widthPercentage.Name = "widthPercentage";
            this.widthPercentage.Size = new System.Drawing.Size(51, 21);
            this.widthPercentage.TabIndex = 15;
            // 
            // heightPercentage
            // 
            this.heightPercentage.FormattingEnabled = true;
            this.heightPercentage.Location = new System.Drawing.Point(453, 214);
            this.heightPercentage.Name = "heightPercentage";
            this.heightPercentage.Size = new System.Drawing.Size(51, 21);
            this.heightPercentage.TabIndex = 16;
            // 
            // textBoxXPixels
            // 
            this.textBoxXPixels.Location = new System.Drawing.Point(453, 72);
            this.textBoxXPixels.Name = "textBoxXPixels";
            this.textBoxXPixels.Size = new System.Drawing.Size(35, 20);
            this.textBoxXPixels.TabIndex = 17;
            // 
            // textBoxYPixels
            // 
            this.textBoxYPixels.Location = new System.Drawing.Point(453, 94);
            this.textBoxYPixels.Name = "textBoxYPixels";
            this.textBoxYPixels.Size = new System.Drawing.Size(35, 20);
            this.textBoxYPixels.TabIndex = 18;
            // 
            // labelLedMapping
            // 
            this.labelLedMapping.AutoSize = true;
            this.labelLedMapping.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLedMapping.Location = new System.Drawing.Point(285, 246);
            this.labelLedMapping.Name = "labelLedMapping";
            this.labelLedMapping.Size = new System.Drawing.Size(71, 13);
            this.labelLedMapping.TabIndex = 19;
            this.labelLedMapping.Text = "LED mapping";
            // 
            // buttonConfigureLedMapping
            // 
            this.buttonConfigureLedMapping.Enabled = false;
            this.buttonConfigureLedMapping.Location = new System.Drawing.Point(288, 281);
            this.buttonConfigureLedMapping.Name = "buttonConfigureLedMapping";
            this.buttonConfigureLedMapping.Size = new System.Drawing.Size(75, 23);
            this.buttonConfigureLedMapping.TabIndex = 20;
            this.buttonConfigureLedMapping.Text = "Configure...";
            this.buttonConfigureLedMapping.UseVisualStyleBackColor = true;
            // 
            // AddServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 362);
            this.Controls.Add(this.buttonConfigureLedMapping);
            this.Controls.Add(this.labelLedMapping);
            this.Controls.Add(this.textBoxYPixels);
            this.Controls.Add(this.textBoxXPixels);
            this.Controls.Add(this.heightPercentage);
            this.Controls.Add(this.widthPercentage);
            this.Controls.Add(this.startAtPercentageY);
            this.Controls.Add(this.startAtPercentageX);
            this.Controls.Add(this.labelVideoSplitting);
            this.Controls.Add(this.labelLayout);
            this.Controls.Add(this.labelNumberOfPixelsY);
            this.Controls.Add(this.labelNumberOfPixelsX);
            this.Controls.Add(this.labelPercentY);
            this.Controls.Add(this.labelPercentX);
            this.Controls.Add(this.labelStartYPercentage);
            this.Controls.Add(this.labelStartXPercent);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.searchProgress);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.availableServersList);
            this.Controls.Add(this.scanForServersButton);
            this.Name = "AddServerForm";
            this.Text = "Add server";
            this.Load += new System.EventHandler(this.AddServerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button scanForServersButton;
        private System.Windows.Forms.ListBox availableServersList;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.ProgressBar searchProgress;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Label labelStartXPercent;
        private System.Windows.Forms.Label labelStartYPercentage;
        private System.Windows.Forms.Label labelPercentX;
        private System.Windows.Forms.Label labelPercentY;
        private System.Windows.Forms.Label labelNumberOfPixelsX;
        private System.Windows.Forms.Label labelNumberOfPixelsY;
        private System.Windows.Forms.Label labelLayout;
        private System.Windows.Forms.Label labelVideoSplitting;
        private System.Windows.Forms.ComboBox startAtPercentageX;
        private System.Windows.Forms.ComboBox startAtPercentageY;
        private System.Windows.Forms.ComboBox widthPercentage;
        private System.Windows.Forms.ComboBox heightPercentage;
        private System.Windows.Forms.TextBox textBoxXPixels;
        private System.Windows.Forms.TextBox textBoxYPixels;
        private System.Windows.Forms.Label labelLedMapping;
        private System.Windows.Forms.Button buttonConfigureLedMapping;
    }
}