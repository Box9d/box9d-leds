using System;
using System.Drawing;
using System.Windows.Forms;
using Box9.Leds.Core.LedLayouts;

namespace Box9.Leds.Manager.Controls
{
    public class InputPanel : Panel
    {
        private readonly Button addServerButton;

        public delegate void AddServer(LedLayout ledLayout);
        public event AddServer AddedServer;

        public InputPanel(int width, int height)
        {
            this.Height = height;
            this.Width = width;

            this.BackColor = Color.Transparent;
            this.Dock = DockStyle.Left;

            addServerButton = new Button
            {
                Text = "Add new FadeCandy server...",
                Width = 200,
            };

            addServerButton.Top = this.Height / 3;
            addServerButton.Left = this.Width / 2 - addServerButton.Width / 2;

            addServerButton.Click += OnAddServerClick;

            AddedServer += OnServerAdded;

            this.Controls.Add(addServerButton);
        }

        public void OnAddServerClick(object sender, EventArgs e)
        {
            AddedServer(new SnareDrumLedLayout()); // This should be a choice
        }

        private void OnServerAdded(LedLayout ledLayout)
        {
            // Placeholder to subscribe to the AddedServer event
        }
    }
}
