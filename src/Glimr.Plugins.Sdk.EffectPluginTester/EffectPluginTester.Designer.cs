namespace Glimr.Plugins.Sdk.EffectPluginTester
{
    partial class EffectPluginTester
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
            this.displayPanel = new System.Windows.Forms.Panel();
            this.listBoxAvailableInputPlugins = new System.Windows.Forms.ListBox();
            this.listBoxAvailableEffectPlugins = new System.Windows.Forms.ListBox();
            this.buttonSelectPlugins = new System.Windows.Forms.Button();
            this.labelAvailableInputPlugins = new System.Windows.Forms.Label();
            this.labelAvailableEffectsPlugins = new System.Windows.Forms.Label();
            this.listBoxAvailableOutputPlugins = new System.Windows.Forms.ListBox();
            this.labelAvailableOutputPlugins = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // displayPanel
            // 
            this.displayPanel.BackColor = System.Drawing.Color.Black;
            this.displayPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.displayPanel.Location = new System.Drawing.Point(808, 105);
            this.displayPanel.Name = "displayPanel";
            this.displayPanel.Size = new System.Drawing.Size(230, 200);
            this.displayPanel.TabIndex = 0;
            // 
            // listBoxAvailableInputPlugins
            // 
            this.listBoxAvailableInputPlugins.FormattingEnabled = true;
            this.listBoxAvailableInputPlugins.Location = new System.Drawing.Point(12, 64);
            this.listBoxAvailableInputPlugins.Name = "listBoxAvailableInputPlugins";
            this.listBoxAvailableInputPlugins.Size = new System.Drawing.Size(118, 316);
            this.listBoxAvailableInputPlugins.TabIndex = 1;
            this.listBoxAvailableInputPlugins.SelectedIndexChanged += new System.EventHandler(this.listBoxAvailableInputPlugins_SelectedIndexChanged);
            // 
            // listBoxAvailableEffectPlugins
            // 
            this.listBoxAvailableEffectPlugins.FormattingEnabled = true;
            this.listBoxAvailableEffectPlugins.Location = new System.Drawing.Point(160, 64);
            this.listBoxAvailableEffectPlugins.Name = "listBoxAvailableEffectPlugins";
            this.listBoxAvailableEffectPlugins.Size = new System.Drawing.Size(118, 316);
            this.listBoxAvailableEffectPlugins.TabIndex = 2;
            this.listBoxAvailableEffectPlugins.SelectedIndexChanged += new System.EventHandler(this.listBoxAvailableEffectPlugins_SelectedIndexChanged);
            // 
            // buttonSelectPlugins
            // 
            this.buttonSelectPlugins.Enabled = false;
            this.buttonSelectPlugins.Location = new System.Drawing.Point(541, 212);
            this.buttonSelectPlugins.Name = "buttonSelectPlugins";
            this.buttonSelectPlugins.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectPlugins.TabIndex = 3;
            this.buttonSelectPlugins.Text = "Run";
            this.buttonSelectPlugins.UseVisualStyleBackColor = true;
            this.buttonSelectPlugins.Click += new System.EventHandler(this.buttonSelectPlugins_Click);
            // 
            // labelAvailableInputPlugins
            // 
            this.labelAvailableInputPlugins.AutoSize = true;
            this.labelAvailableInputPlugins.Location = new System.Drawing.Point(12, 39);
            this.labelAvailableInputPlugins.Name = "labelAvailableInputPlugins";
            this.labelAvailableInputPlugins.Size = new System.Drawing.Size(114, 13);
            this.labelAvailableInputPlugins.TabIndex = 4;
            this.labelAvailableInputPlugins.Text = "Available Input Plugins";
            // 
            // labelAvailableEffectsPlugins
            // 
            this.labelAvailableEffectsPlugins.AutoSize = true;
            this.labelAvailableEffectsPlugins.Location = new System.Drawing.Point(157, 39);
            this.labelAvailableEffectsPlugins.Name = "labelAvailableEffectsPlugins";
            this.labelAvailableEffectsPlugins.Size = new System.Drawing.Size(123, 13);
            this.labelAvailableEffectsPlugins.TabIndex = 5;
            this.labelAvailableEffectsPlugins.Text = "Available Effects Plugins";
            // 
            // listBoxAvailableOutputPlugins
            // 
            this.listBoxAvailableOutputPlugins.FormattingEnabled = true;
            this.listBoxAvailableOutputPlugins.Location = new System.Drawing.Point(313, 64);
            this.listBoxAvailableOutputPlugins.Name = "listBoxAvailableOutputPlugins";
            this.listBoxAvailableOutputPlugins.Size = new System.Drawing.Size(118, 316);
            this.listBoxAvailableOutputPlugins.TabIndex = 6;
            this.listBoxAvailableOutputPlugins.SelectedIndexChanged += new System.EventHandler(this.listBoxAvailableOutputPlugins_SelectedIndexChanged);
            // 
            // labelAvailableOutputPlugins
            // 
            this.labelAvailableOutputPlugins.AutoSize = true;
            this.labelAvailableOutputPlugins.Location = new System.Drawing.Point(310, 39);
            this.labelAvailableOutputPlugins.Name = "labelAvailableOutputPlugins";
            this.labelAvailableOutputPlugins.Size = new System.Drawing.Size(122, 13);
            this.labelAvailableOutputPlugins.TabIndex = 7;
            this.labelAvailableOutputPlugins.Text = "Available Output Plugins";
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(541, 262);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 8;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // EffectPluginTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 426);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.labelAvailableOutputPlugins);
            this.Controls.Add(this.listBoxAvailableOutputPlugins);
            this.Controls.Add(this.labelAvailableEffectsPlugins);
            this.Controls.Add(this.labelAvailableInputPlugins);
            this.Controls.Add(this.buttonSelectPlugins);
            this.Controls.Add(this.listBoxAvailableEffectPlugins);
            this.Controls.Add(this.listBoxAvailableInputPlugins);
            this.Controls.Add(this.displayPanel);
            this.Name = "EffectPluginTester";
            this.Text = "Effect Plugin Tester";
            this.Load += new System.EventHandler(this.EffectPluginTester_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel displayPanel;
        private System.Windows.Forms.ListBox listBoxAvailableInputPlugins;
        private System.Windows.Forms.ListBox listBoxAvailableEffectPlugins;
        private System.Windows.Forms.Button buttonSelectPlugins;
        private System.Windows.Forms.Label labelAvailableInputPlugins;
        private System.Windows.Forms.Label labelAvailableEffectsPlugins;
        private System.Windows.Forms.ListBox listBoxAvailableOutputPlugins;
        private System.Windows.Forms.Label labelAvailableOutputPlugins;
        private System.Windows.Forms.Button buttonStop;
    }
}

