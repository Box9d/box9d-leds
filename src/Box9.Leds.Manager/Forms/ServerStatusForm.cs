using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Dtos;
using Box9.Leds.Core.Validation;
using Box9.Leds.Manager.Presenters;
using Box9.Leds.Manager.Views;

namespace Box9.Leds.Manager.Forms
{
    public partial class ServerStatusForm : Form, IServerStatusView
    {
        private readonly ServerStatusPresenter presenter;

        public ServerStatusForm(LedConfiguration ledConfiguration)
        {
            InitializeComponent();

            Guard.NotNullOrEmpty(ledConfiguration);

            presenter = new ServerStatusPresenter(this, ledConfiguration);
        }

        public LedConfiguration LedConfiguration { get; set; }

        public NetworkDetails NetworkDetails { get; set; }

        private void ServerStatusForm_Load(object sender, EventArgs e)
        {
            presenter.IsDirty += ReloadForm;

            presenter.StartMonitoring("192.168.1.1");
        }

        private void ReloadForm(object sender, EventArgs args)
        {
            if (NetworkDetails == null)
            {
                return;
            }

            Invoke(new Action(() =>
            {
                labelNumberOfServersOnline.Text = string.Format("Monitoring {0} server(s)", LedConfiguration.Servers.Count());

                var servers = LedConfiguration.Servers.ToArray();
                var textTemplate = "Server: '{0}' - Signal strength {1}%";

                var output = string.Empty;

                for (int i = 0; i < servers.Length; i++)
                {
                    var deviceDetails = NetworkDetails.Devices.SingleOrDefault(d => d.IPAddress == servers[i].NetworkDeviceDetails.IPAddress);

                    output += deviceDetails != null && deviceDetails.SignalStrengthPercentage.HasValue
                        ? string.Format(textTemplate, servers[i].NetworkDeviceDetails.DeviceName, deviceDetails.SignalStrengthPercentage.Value)
                        : string.Format(textTemplate, servers[i].NetworkDeviceDetails.DeviceName, 0);
                    output += Environment.NewLine;
                }

                labelStatus.Text = output;
            }));
        }
    }
}
