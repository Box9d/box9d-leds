using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Box9.Leds.Business.EventArgs;
using Box9.Leds.Core.UpdatePixels;
using Box9.Leds.Manager.Presenters;
using Box9.Leds.Manager.Views;

namespace Box9.Leds.Manager.Forms
{
    public partial class ConfigureLedMappingForm : Form, IConfigureLedMappingView
    {
        private readonly ConfigureLedMappingPresenter presenter;

        public Bitmap Image { get; set; }

        public event MouseEventHandler MousePositionChanged;
        public event EventHandler Clear;
        public event EventHandler Undo;
        public event MouseEventHandler ReadyToDraw;
        public event MouseEventHandler StopDrawing;
        public event EventHandler Confirm;
        public event EventHandler<LedMappingEventArgs> FinishedMapping;

        public ConfigureLedMappingForm(int xPixels, int yPixels, List<PixelInfo> initialPixelMappings = null)
        {
            InitializeComponent();

            presenter = new ConfigureLedMappingPresenter(this, xPixels, yPixels, initialPixelMappings);
        }

        public void ServerForm_Load(object sender, EventArgs args)
        {
            displayPanel.MouseDown += ReadyToDraw;
            displayPanel.MouseUp += StopDrawing;
            displayPanel.MouseMove += MousePositionChanged;
            buttonClear.Click += Clear;
            buttonUndo.Click += Undo;

            displayPanel.PreviewKeyDown += (s, a) =>
            {
                if (a.Control && a.KeyCode == Keys.Z)
                {
                    Undo(null, null);
                }
            };

            buttonConfirm.Click += (s, a) =>
            {
                Confirm(s, a);
            };

            presenter.IsDirty += Reload;
            presenter.CancelledPresenting += (s, a) =>
            {
                Close();
                Dispose();
            };
            presenter.FinishedPresenting += (s, val) =>
            {
                FinishedMapping(s, new LedMappingEventArgs(val));
                Close();
                Dispose();
            };
            presenter.Load();
        }

        private void Reload(object sender, EventArgs args)
        {
            Invoke(new Action(() =>
            {
                displayPanel.BackgroundImage = Image;
                displayPanel.Height = Image.Height;
                displayPanel.Width = Image.Width;
                this.Height = displayPanel.Height + 160;
                displayPanel.Focus();
            }));
        }
    }
}
