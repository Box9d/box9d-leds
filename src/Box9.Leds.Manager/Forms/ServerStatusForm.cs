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
            Guard.NotNullOrEmpty(ledConfiguration);

            InitializeComponent();

            presenter = new ServerStatusPresenter(this, ledConfiguration);
        }

        public LedConfiguration LedConfiguration { get; set; }

        public NetworkDetails NetworkDetails { get; set; }

        private void ServerStatusForm_Load(object sender, EventArgs e)
        {
            presenter.IsDirty += ReloadForm;
        }

        private void ReloadForm(object sender, EventArgs args)
        {
            Invoke(new Action(() =>
            {
                labelNumberOfServersOnline.Text = string.Format("{0} server(s) online", LedConfiguration.Servers.Count());
                tableLayoutPanel.RowCount = LedConfiguration.Servers.Count();
                tableLayoutPanel.ColumnCount = 1;
                tableLayoutPanel.

                foreach (var server in LedConfiguration.Servers)
                {
                    
                }
            }));
        }
    }
}
