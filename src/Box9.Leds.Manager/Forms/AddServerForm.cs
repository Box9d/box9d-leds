using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.EventsArguments;
using Box9.Leds.Core.EventsArguments;
using Box9.Leds.Core.UpdatePixels;
using Box9.Leds.Manager.Extensions;
using Box9.Leds.Manager.Presenters;
using Box9.Leds.Manager.Views;

namespace Box9.Leds.Manager.Forms
{
    public partial class AddServerForm : Form, IAddServerView
    {
        private readonly AddServerPresenter presenter;

        public string SelectedServer { get; set; }

        public int? ScanProgressPercentage { get; set; }

        public List<string> Servers { get; set; }

        public int? NumberOfHorizontalPixels { get; set; }

        public int? NumberOfVerticalPixels { get; set; }

        public int StartAtHorizontalPercentage { get; set; }

        public int StartAtVerticalPercentage { get; set; }

        public int HorizontalPercentage { get; set; }

        public int VerticalPercentage { get; set; }

        public int MaxAvailableHorizontalPercentage { get; set; }

        public int MaxAvailableVerticalPercentage { get; set; }

        public IEnumerable<PixelInfo> PixelMappings { get; set; }

        public event EventHandler<EventArgs> ScanForServers;
        public event EventHandler<StringEventArgs> ServerSelected;
        public event EventHandler<StringEventArgs> NumberOfHorizontalPixelsChanged;
        public event EventHandler<StringEventArgs> NumberOfVerticalPixelsChanged;
        public event EventHandler<IntegerEventArgs> StartAtHorizontalPercentageChanged;
        public event EventHandler<IntegerEventArgs> StartAtVerticalPercentageChanged;
        public event EventHandler<IntegerEventArgs> HorizontalPercentageChanged;
        public event EventHandler<IntegerEventArgs> VerticalPercentageChanged;
        public event EventHandler<EventArgs> ConfigureLedMapping;
        public event EventHandler<EventArgs> Cancel;
        public event EventHandler<EventArgs> Confirm;
        public event EventHandler<ServerConfigurationEventArgs> ServerAddedOrUpdated;

        public AddServerForm(ServerConfiguration configuration = null)
        {
            InitializeComponent();

            presenter = new AddServerPresenter(this, configuration);
        }

        private void AddServerForm_Load(object sender, EventArgs e)
        {
            startAtPercentageX.AsPercentageOptions();
            startAtPercentageY.AsPercentageOptions();
            widthPercentage.AsPercentageOptions(1, 100);
            heightPercentage.AsPercentageOptions(1, 100);

            startAtPercentageX.DropDownStyle = ComboBoxStyle.DropDownList;
            startAtPercentageY.DropDownStyle = ComboBoxStyle.DropDownList;
            widthPercentage.DropDownStyle = ComboBoxStyle.DropDownList;
            heightPercentage.DropDownStyle = ComboBoxStyle.DropDownList;

            scanForServersButton.Click += (s, args) =>
            {
                ScanForServers(s, args);
            };

            availableServersList.SelectedIndexChanged += SelectedServerChangedOnUI;

            textBoxXPixels.TextChanged += (s, args) =>
            {
                NumberOfHorizontalPixelsChanged(s, new StringEventArgs(textBoxXPixels.Text));
            };

            textBoxYPixels.TextChanged += (s, args) =>
            {
                NumberOfVerticalPixelsChanged(s, new StringEventArgs(textBoxYPixels.Text));
            };

            startAtPercentageX.SelectedIndexChanged += (s, args) =>
            {
                StartAtHorizontalPercentageChanged(s, new IntegerEventArgs(int.Parse(startAtPercentageX.SelectedIndex.ToString())));
            };

            startAtPercentageY.SelectedIndexChanged += (s, args) =>
            {
                StartAtVerticalPercentageChanged(s, new IntegerEventArgs(int.Parse(startAtPercentageY.SelectedIndex.ToString())));
            };

            widthPercentage.SelectedIndexChanged += HorizontalPercentageChangedOnUI;

            heightPercentage.SelectedIndexChanged += VerticalPercentageChangedOnUI;

            buttonConfigureLedMapping.Click += (s, args) =>
            {
                ConfigureLedMapping(s, args);
            };

            cancel.Click += (s, args) =>
            {
                Cancel(s, args);
            };

            buttonConfirm.Click += (s, args) =>
            {
                Confirm(s, args);
            };

            presenter.FinishedPresenting += (s, args) =>
            {
                presenter.ProgressUpdate -= ProgressUpdate;
                ServerAddedOrUpdated(s, new ServerConfigurationEventArgs(args));
                Close();
                Dispose();
            };

            presenter.CancelledPresenting += (s, args) =>
            {
                presenter.ProgressUpdate -= ProgressUpdate;
                Close();
                Dispose();
            };

            presenter.IsDirty += Reload;
            presenter.ProgressUpdate += ProgressUpdate;
        }

        private void Reload(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                availableServersList.Items.Clear();
                foreach (var server in Servers)
                {
                    availableServersList.Items.Add(server);
                    if (SelectedServer == server)
                    {
                        availableServersList.SelectedIndexChanged -= SelectedServerChangedOnUI;
                        availableServersList.SelectedItem = server;
                        availableServersList.SelectedIndexChanged += SelectedServerChangedOnUI;
                    }
                }

                textBoxXPixels.Text = NumberOfHorizontalPixels.HasValue
                    ? NumberOfHorizontalPixels.Value.ToString()
                    : string.Empty;
                textBoxXPixels.ValidateTextLength(3);
                textBoxXPixels.SetCursorToEnd();

                textBoxYPixels.Text = NumberOfVerticalPixels.HasValue
                    ? NumberOfVerticalPixels.Value.ToString()
                    : string.Empty;
                textBoxYPixels.ValidateTextLength(3);
                textBoxYPixels.SetCursorToEnd();

                widthPercentage.SelectedIndexChanged -= HorizontalPercentageChangedOnUI;
                widthPercentage.Items.Clear();
                widthPercentage.AsPercentageOptions(1, MaxAvailableHorizontalPercentage);
                widthPercentage.SelectedItem = HorizontalPercentage;
                widthPercentage.SelectedIndexChanged += HorizontalPercentageChangedOnUI;

                heightPercentage.SelectedIndexChanged -= VerticalPercentageChangedOnUI;
                heightPercentage.Items.Clear();
                heightPercentage.AsPercentageOptions(1, MaxAvailableVerticalPercentage);
                heightPercentage.SelectedItem = VerticalPercentage;
                heightPercentage.SelectedIndexChanged += VerticalPercentageChangedOnUI;

                buttonConfirm.Enabled = HorizontalPercentage > 0
                    && VerticalPercentage > 0
                    && StartAtHorizontalPercentage >= 0
                    && StartAtVerticalPercentage >= 0
                    && SelectedServer != null
                    && NumberOfVerticalPixels.HasValue && NumberOfVerticalPixels.Value > 0
                    && NumberOfHorizontalPixels.HasValue && NumberOfHorizontalPixels.Value > 0
                    && PixelMappings != null && PixelMappings.Any(pm => pm.Order > 0);

                buttonConfigureLedMapping.Enabled = NumberOfVerticalPixels.HasValue && NumberOfVerticalPixels.Value > 0
                    && NumberOfHorizontalPixels.HasValue && NumberOfHorizontalPixels.Value > 0;
            }));
        }

        private void ProgressUpdate(object sender, EventArgs args)
        {
            Invoke(new Action(() =>
            {
                searchProgress.Value = ScanProgressPercentage.HasValue
                    ? ScanProgressPercentage.Value
                    : 0;
            }));            
        }

        private void SelectedServerChangedOnUI(object sender, EventArgs args)
        {
            var address = availableServersList.SelectedItem != null
                    ? availableServersList.SelectedItem.ToString()
                    : null;

            ServerSelected(sender, new StringEventArgs(address));
        }

        private void HorizontalPercentageChangedOnUI(object sender, EventArgs args)
        {
            HorizontalPercentageChanged(sender, new IntegerEventArgs(int.Parse(widthPercentage.SelectedItem.ToString())));
        }

        private void VerticalPercentageChangedOnUI(object sender, EventArgs args)
        {
            VerticalPercentageChanged(sender, new IntegerEventArgs(int.Parse(heightPercentage.SelectedItem.ToString())));
        }
    }
}
