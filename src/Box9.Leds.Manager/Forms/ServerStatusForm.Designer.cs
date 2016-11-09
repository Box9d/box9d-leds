namespace Box9.Leds.Manager.Forms
{
    partial class ServerStatusForm
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
            this.labelNumberOfServersOnline = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelNumberOfServersOnline
            // 
            this.labelNumberOfServersOnline.AutoSize = true;
            this.labelNumberOfServersOnline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNumberOfServersOnline.Location = new System.Drawing.Point(13, 13);
            this.labelNumberOfServersOnline.Name = "labelNumberOfServersOnline";
            this.labelNumberOfServersOnline.Size = new System.Drawing.Size(81, 13);
            this.labelNumberOfServersOnline.TabIndex = 0;
            this.labelNumberOfServersOnline.Text = "0 servers online";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(13, 43);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 1;
            // 
            // ServerStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 318);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelNumberOfServersOnline);
            this.Name = "ServerStatusForm";
            this.Text = "Server Status";
            this.Load += new System.EventHandler(this.ServerStatusForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNumberOfServersOnline;
        private System.Windows.Forms.Label labelStatus;
    }
}