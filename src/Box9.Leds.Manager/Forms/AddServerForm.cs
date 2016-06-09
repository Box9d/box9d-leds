using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.Core.LedLayouts;
using Box9.Leds.Core.Messages.ConnectedDevices;
using Box9.Leds.Core.Servers;
using Box9.Leds.FcClient.Search;

namespace Box9.Leds.Manager.Forms
{
    public partial class AddServerForm : Form
    {
        private ClientSearch clientSearch;
        private int ipsSearched;
        private Task searchTask;
        private CancellationTokenSource cts;

        public delegate void ServerAddedHandler(ServerBase server, LedLayout ledLayout);
        public event ServerAddedHandler ServerAdded;

        public AddServerForm()
        {
            InitializeComponent();
        }

        private void AddServerForm_Load(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            this.clientSearch = new ClientSearch();
            clientSearch.ServerFound += AddServerToList;
            clientSearch.IPAddressSearched += UpdateProgress;
            clientSearch.SearchStatusChanged += StatusChanged;

            this.FormClosing += OnFormClosing;

            // TODO: Add available layouts dynamically
            availableLedLayouts.Items.Add(new SnareDrumLedLayout());

            availableServersList.Items.Add(new DisplayOnlyServer());

            ServerAdded += ServerAddedHandle;
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
            this.selectServerButton.Enabled = 
                availableServersList.SelectedIndex > -1
                && availableLedLayouts.SelectedIndex > -1;
        }

        private void availableLedLayouts_SelectedIndexChanged(object sender, EventArgs e)
        {
            availableServersList_SelectedIndexChanged(sender, e);
        }

        private void selectServerButton_Click(object sender, EventArgs e)
        {
            cts.Cancel();

            var server = (ServerBase)availableServersList.SelectedItem;
            var serverLedLayout = (LedLayout)availableLedLayouts.SelectedItem;
            ServerAdded(server, serverLedLayout);

            this.Close();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
            this.Visible = false;
        }

        private void ServerAddedHandle(ServerBase server, LedLayout ledLayout)
        {
        }
    }
}
