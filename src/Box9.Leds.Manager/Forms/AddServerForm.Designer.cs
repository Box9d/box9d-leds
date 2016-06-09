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
            this.selectServerButton = new System.Windows.Forms.Button();
            this.searchProgress = new System.Windows.Forms.ProgressBar();
            this.cancel = new System.Windows.Forms.Button();
            this.availableLedLayouts = new System.Windows.Forms.ListBox();
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
            this.scanForServersButton.Click += new System.EventHandler(this.scanForServersButton_Click);
            // 
            // availableServersList
            // 
            this.availableServersList.FormattingEnabled = true;
            this.availableServersList.Location = new System.Drawing.Point(12, 50);
            this.availableServersList.Name = "availableServersList";
            this.availableServersList.Size = new System.Drawing.Size(251, 199);
            this.availableServersList.TabIndex = 1;
            this.availableServersList.SelectedIndexChanged += new System.EventHandler(this.availableServersList_SelectedIndexChanged);
            // 
            // selectServerButton
            // 
            this.selectServerButton.Enabled = false;
            this.selectServerButton.Location = new System.Drawing.Point(384, 272);
            this.selectServerButton.Name = "selectServerButton";
            this.selectServerButton.Size = new System.Drawing.Size(82, 23);
            this.selectServerButton.TabIndex = 2;
            this.selectServerButton.Text = "Select";
            this.selectServerButton.UseVisualStyleBackColor = true;
            this.selectServerButton.Click += new System.EventHandler(this.selectServerButton_Click);
            // 
            // searchProgress
            // 
            this.searchProgress.Location = new System.Drawing.Point(129, 13);
            this.searchProgress.Name = "searchProgress";
            this.searchProgress.Size = new System.Drawing.Size(134, 23);
            this.searchProgress.TabIndex = 3;
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(472, 272);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 4;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // availableLedLayouts
            // 
            this.availableLedLayouts.FormattingEnabled = true;
            this.availableLedLayouts.Location = new System.Drawing.Point(298, 50);
            this.availableLedLayouts.Name = "availableLedLayouts";
            this.availableLedLayouts.Size = new System.Drawing.Size(249, 199);
            this.availableLedLayouts.TabIndex = 5;
            this.availableLedLayouts.SelectedIndexChanged += new System.EventHandler(this.availableLedLayouts_SelectedIndexChanged);
            // 
            // AddServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 307);
            this.Controls.Add(this.availableLedLayouts);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.searchProgress);
            this.Controls.Add(this.selectServerButton);
            this.Controls.Add(this.availableServersList);
            this.Controls.Add(this.scanForServersButton);
            this.Name = "AddServerForm";
            this.Text = "Add server";
            this.Load += new System.EventHandler(this.AddServerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button scanForServersButton;
        private System.Windows.Forms.ListBox availableServersList;
        private System.Windows.Forms.Button selectServerButton;
        private System.Windows.Forms.ProgressBar searchProgress;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.ListBox availableLedLayouts;
    }
}