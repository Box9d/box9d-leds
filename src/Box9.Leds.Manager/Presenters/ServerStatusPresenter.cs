﻿using System;
using System.Threading;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Services;
using Box9.Leds.Manager.Views;

namespace Box9.Leds.Manager.Presenters
{
    public class ServerStatusPresenter : PresenterBase<int>
    {
        private readonly IServerStatusView view;

        private Timer networkDetailsTimer;

        public ServerStatusPresenter(IServerStatusView view,
            LedConfiguration ledConfiguration)
        {
            this.view = view;
            this.view.LedConfiguration = ledConfiguration;
        }

        public void StartMonitoring(string routerIpAddress, int refreshPeriodInMilliseconds = 2000)
        {
            INetworkService networkService = new NetworkService();

            var callback = new TimerCallback((obj) =>
            {
                try
                {
                    view.NetworkDetails = networkService.GetDdwrtNetworkDetails(routerIpAddress);
                    MarkAsDirty();
                }
                catch (Exception)
                {
                    // Surpress errors in updating network details
                }
            });

            networkDetailsTimer = new Timer(callback, null, 0, refreshPeriodInMilliseconds); 
        }

        public void UpdateLedConfiguration(LedConfiguration ledConfiguration)
        {
            lock (view.LedConfiguration)
            {
                view.LedConfiguration = ledConfiguration;
            }
        }

        public void FinishMonitoring()
        {
            networkDetailsTimer.Dispose();
        }
    }
}
