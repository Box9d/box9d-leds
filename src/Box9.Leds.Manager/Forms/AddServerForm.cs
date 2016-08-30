using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.Core.Configuration;
using Box9.Leds.Core.Coordination;
using Box9.Leds.Core.Messages.ConnectedDevices;
using Box9.Leds.Core.Servers;
using Box9.Leds.FcClient.Search;
using Box9.Leds.Manager.Extensions;

namespace Box9.Leds.Manager.Forms
{
    public partial class AddServerForm : Form
    {
        private readonly bool isEdit;
        private readonly ServerConfiguration editConfig;

        private ClientSearch clientSearch;
        private int ipsSearched;
        private Task searchTask;
        private CancellationTokenSource cts;
        private List<PixelMapping> pixelMappings;

        public delegate void ServerAddedHandler(ServerConfiguration server);
        public event ServerAddedHandler ServerAdded;

        public delegate void ServerEditedHandler(ServerConfiguration server);
        public event ServerEditedHandler ServerEdited;

        public AddServerForm()
        {
            InitializeComponent();

            pixelMappings = new List<PixelMapping>();
            isEdit = false;
        }

        public AddServerForm(ServerConfiguration serverConfiguration)
        {
            InitializeComponent();

            editConfig = serverConfiguration;
            pixelMappings = serverConfiguration.PixelMappings;
            isEdit = true;
        }

        private void AddServerForm_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            this.clientSearch = new ClientSearch();
            clientSearch.ServerFound += AddServerToList;
            clientSearch.IPAddressSearched += UpdateProgress;
            clientSearch.SearchStatusChanged += StatusChanged;

            this.FormClosing += OnFormClosing;

            this.startAtPercentageX.AsPercentageOptions();
            this.startAtPercentageY.AsPercentageOptions();
            this.widthPercentage.AsPercentageOptions();
            this.heightPercentage.AsPercentageOptions();

            ServerAdded += ServerAddedHandle;

            this.startAtPercentageX.DropDownStyle = ComboBoxStyle.DropDownList;
            this.startAtPercentageY.DropDownStyle = ComboBoxStyle.DropDownList;
            this.widthPercentage.DropDownStyle = ComboBoxStyle.DropDownList;
            this.heightPercentage.DropDownStyle = ComboBoxStyle.DropDownList;

            if (isEdit)
            {
                if (editConfig.ServerType == ServerType.FadeCandy)
                {
                    var server = new FadecandyServer(IPAddress.Parse(editConfig.IPAddress), editConfig.Port);

                    availableServersList.Items.Add(server);
                    availableServersList.SelectedItem = server;
                }
                else
                {
                    var server = new DisplayOnlyServer();

                    availableServersList.Items.Add(server);
                    availableServersList.SelectedItem = server;
                }

                this.textBoxXPixels.Text = editConfig.XPixels.ToString();
                this.textBoxYPixels.Text = editConfig.YPixels.ToString();
                this.startAtPercentageX.SelectedItem = editConfig.VideoConfiguration.StartAtXPercent;
                this.startAtPercentageY.SelectedItem = editConfig.VideoConfiguration.StartAtYPercent;
                this.widthPercentage.SelectedItem = editConfig.VideoConfiguration.XPercent;
                this.heightPercentage.SelectedItem = editConfig.VideoConfiguration.YPercent;

                ValidateVideoSplitting();
            }
        }

        private void scanForServersButton_Click(object sender, EventArgs e)
        {
            this.searchProgress.Value = 0;
            this.ipsSearched = 0;
            this.availableServersList.Items.Clear();

            availableServersList.Items.Add(new DisplayOnlyServer());

            searchTask = new Task(() => clientSearch.SearchForFadecandyServers(7890, cts.Token), cts.Token);
            searchTask.Start();
        }

        private void AddServerToList(IPAddress ipAddress, IEnumerable<ConnectedDeviceResponse> devices)
        {
            availableServersList.Invoke(new Action(() =>
            {
                availableServersList.Items.Add(new FadecandyServer(ipAddress, 7890));
            }));            
        }

        private void UpdateProgress()
        {
            ipsSearched++;

            searchProgress.Invoke(new Action(() =>
            {
                searchProgress.Value = (int)Math.Round((((double)ipsSearched / (double)clientSearch.TotalIPSearches) * 100), 0);
            }));
        }

        private void StatusChanged(SearchStatus status)
        {
            switch (status)
            {
                case SearchStatus.Finished:
                    ipsSearched = clientSearch.TotalIPSearches - 1;
                    UpdateProgress();
                    break;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            this.Close();
        }

        private void availableServersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateSelectButtonAvailability();
        }

        private void availableLedLayouts_SelectedIndexChanged(object sender, EventArgs e)
        {
            availableServersList_SelectedIndexChanged(sender, e);
        }

        private void selectServerButton_Click(object sender, EventArgs e)
        {
            cts.Cancel();

            var server = (ServerBase)availableServersList.SelectedItem;

            var configuration = new ServerConfiguration();
            if (server.ServerType == ServerType.FadeCandy)
            {
                var fadeCandyServer = (FadecandyServer)server;
                configuration.IPAddress = fadeCandyServer.IPAddress.ToString();
                configuration.Port = fadeCandyServer.Port;
            }

            configuration.ServerType = server.ServerType;
            configuration.XPixels = int.Parse(textBoxXPixels.Text);
            configuration.YPixels = int.Parse(textBoxYPixels.Text);
            configuration.PixelMappings = pixelMappings;
            configuration.VideoConfiguration = new ServerVideoConfiguration
            {
                StartAtXPercent = (int)startAtPercentageX.SelectedItem,
                StartAtYPercent = (int)startAtPercentageY.SelectedItem,
                XPercent = (int)widthPercentage.SelectedItem,
                YPercent = (int)heightPercentage.SelectedItem
            };

            ServerAdded(configuration);

            this.Close();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
            this.Visible = false;
        }

        private void ServerAddedHandle(ServerConfiguration server)
        {
        }

        private void startAtPercentageX_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateVideoSplitting();
            ValidateSelectButtonAvailability();
        }

        private void startAtPercentageY_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateVideoSplitting();
            ValidateSelectButtonAvailability();
        }

        private void widthPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateVideoSplitting();
            ValidateSelectButtonAvailability();
        }

        private void heightPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateVideoSplitting();
            ValidateSelectButtonAvailability();
        }

        private void textBoxXPixels_TextChanged(object sender, EventArgs e)
        {
            this.textBoxXPixels.ValidateAsInteger();
            this.textBoxXPixels.ValidateTextLength(3);

            ValidateConfigureMappingButtonAvailability();
            ValidateSelectButtonAvailability();
        }

        private void textBoxYPixels_TextChanged(object sender, EventArgs e)
        {
            this.textBoxYPixels.ValidateAsInteger();
            this.textBoxYPixels.ValidateTextLength(3);

            ValidateConfigureMappingButtonAvailability();
            ValidateSelectButtonAvailability();
        }

        private void ValidateVideoSplitting()
        {
            var startAtXPercent = this.startAtPercentageX.SelectedItem == null ? default(int) : (int)this.startAtPercentageX.SelectedItem;
            var startAtYPercent = this.startAtPercentageY.SelectedItem == null ? default(int) : (int)this.startAtPercentageY.SelectedItem;
            var xPercent = this.widthPercentage.SelectedItem == null ? default(int) : (int)this.widthPercentage.SelectedItem;
            var yPercent = this.heightPercentage.SelectedItem == null ? default(int) : (int)this.heightPercentage.SelectedItem;

            this.widthPercentage.AsPercentageOptions(0, 100 - startAtXPercent);
            this.heightPercentage.AsPercentageOptions(0, 100 - startAtYPercent);
        }

        private void ValidateSelectButtonAvailability()
        {
            this.selectServerButton.Enabled = 
                this.startAtPercentageX.SelectedIndex > -1 &&
                this.startAtPercentageY.SelectedIndex > -1 &&
                this.widthPercentage.SelectedIndex > -1 &&
                this.heightPercentage.SelectedIndex > -1 &&
                this.availableServersList.SelectedIndex > -1 &&
                this.pixelMappings.Any() &&
                !string.IsNullOrEmpty(this.textBoxXPixels.Text) &&
                !string.IsNullOrEmpty(this.textBoxYPixels.Text);
        }

        private void ValidateConfigureMappingButtonAvailability()
        {
            this.buttonConfigureLedMapping.Enabled = 
                !string.IsNullOrEmpty(this.textBoxXPixels.Text) &&
                !string.IsNullOrEmpty(this.textBoxYPixels.Text);
        }

        private void buttonConfigureLedMapping_Click(object sender, EventArgs e)
        {
            var configureForm = new ConfigureLedMappingForm(int.Parse(textBoxXPixels.Text), int.Parse(textBoxYPixels.Text), pixelMappings);
            configureForm.StartPosition = FormStartPosition.Manual;
            configureForm.Location = new System.Drawing.Point(this.Location.X + 20, this.Location.X + 20);

            configureForm.ConfigurationConfirmed += (form, mappings) =>
            {
                form.Close();

                pixelMappings = mappings
                    .Select(m => new PixelMapping { Order = m.Key, X = m.Value.X, Y = m.Value.Y })
                    .ToList();

                ValidateSelectButtonAvailability();
            };

            configureForm.Show();
        }
    }
}
