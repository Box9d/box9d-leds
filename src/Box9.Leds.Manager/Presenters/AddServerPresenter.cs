using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Dtos;
using Box9.Leds.Business.Services;
using Box9.Leds.Core.Multitasking;
using Box9.Leds.FcClient;
using Box9.Leds.Manager.Extensions;
using Box9.Leds.Manager.Forms;
using Box9.Leds.Manager.Maps;
using Box9.Leds.Manager.Views;
using RickPowell.ExplicitMapping;

namespace Box9.Leds.Manager.Presenters
{
    public class AddServerPresenter : PresenterBase<ServerConfiguration>
    {
        private readonly IAddServerView view;
        private CancellationTokenSource cts;

        public AddServerPresenter(IAddServerView view,
            ServerConfiguration serverConfiguration)
        {
            this.view = view;

            this.view.HorizontalPercentage = -1;
            this.view.VerticalPercentage = -1;
            this.view.StartAtHorizontalPercentage = -1;
            this.view.StartAtVerticalPercentage = -1;

            this.view.MaxAvailableHorizontalPercentage = 100;
            this.view.MaxAvailableVerticalPercentage = 100;

            if (serverConfiguration != null)
            {
                this.view.NumberOfHorizontalPixels = serverConfiguration.XPixels;
                this.view.NumberOfVerticalPixels = serverConfiguration.YPixels;
                this.view.StartAtHorizontalPercentage = serverConfiguration.VideoConfiguration.StartAtXPercent;
                this.view.StartAtVerticalPercentage = serverConfiguration.VideoConfiguration.StartAtYPercent;
                this.view.HorizontalPercentage = serverConfiguration.VideoConfiguration.XPercent;
                this.view.VerticalPercentage = serverConfiguration.VideoConfiguration.YPercent;
            }

            this.view.Servers = new List<NetworkDeviceDetails>();

            this.view.ScanForServers += (s, args) =>
            {
                ScanForServers();
            };

            this.view.ServerSelected += (s, args) =>
            {
                ServerSelected(args.Value);
            };

            this.view.NumberOfHorizontalPixelsChanged += (s, args) =>
            {
                NumberOfHorizontalPixelsChanged(args.Value);
            };

            this.view.NumberOfVerticalPixelsChanged += (s, args) =>
            {
                NumberOfVerticalPixelsChanged(args.Value);
            };

            this.view.StartAtHorizontalPercentageChanged += (s, args) =>
            {
                StartAtHorizontalPercentageChanged(args.Value);
            };

            this.view.StartAtVerticalPercentageChanged += (s, args) =>
            {
                StartAtVerticalPercentageChanged(args.Value);
            };

            this.view.HorizontalPercentageChanged += (s, args) =>
            {
                HorizontalPercentageChanged(args.Value);
            };

            this.view.VerticalPercentageChanged += (s, args) =>
            {
                VerticalPercentageChanged(args.Value);
            };

            this.view.ConfigureLedMapping += (s, args) =>
            {
                ConfigureLedMapping();
            };

            this.view.Cancel += (s, args) =>
            {
                Cancel();
            };

            this.view.Confirm += (s, args) =>
            {
                Confirm();
            };
        }

        public void ScanForServers()
        {
            INetworkService networkService = new NetworkService();

            cts = new CancellationTokenSource();
            view.Servers.Clear();

            Task.Run(() =>
            {
                view.ScanProgressPercentage = 1;
                ProgressChanged();

                var networkDetails = networkService.GetNetworkDetails("192.168.0.1", cts.Token);
                view.Servers.AddRange(networkDetails.Devices
                    .Where(d => networkService.IsFadecandyDevice(d)));

                view.ScanProgressPercentage = 100;
                ProgressChanged();
                MarkAsDirty();
            }).Forget();
        }

        public void ServerSelected(string ipAddress)
        {
            view.SelectedServer = view.Servers.Single(s => s.IPAddress == ipAddress);

            MarkAsDirty();
        }

        public void NumberOfHorizontalPixelsChanged(string numberOfPixels)
        {
            view.NumberOfHorizontalPixels = numberOfPixels.EnsureIsInteger();

            MarkAsDirty();
        }

        public void NumberOfVerticalPixelsChanged(string numberOfPixels)
        {
            view.NumberOfVerticalPixels = numberOfPixels.EnsureIsInteger();

            MarkAsDirty();
        }

        public void StartAtHorizontalPercentageChanged(int horizontalPercentage)
        {
            view.StartAtHorizontalPercentage = horizontalPercentage;

            view.MaxAvailableHorizontalPercentage = horizontalPercentage > 0
                    ? 100 - horizontalPercentage
                    : 100;

            if (view.HorizontalPercentage > view.MaxAvailableHorizontalPercentage)
            {
                HorizontalPercentageChanged(-1);
            }

            MarkAsDirty();
        }

        public void StartAtVerticalPercentageChanged(int verticalPercentage)
        {
            view.StartAtVerticalPercentage = verticalPercentage;
            view.MaxAvailableVerticalPercentage = verticalPercentage > 0
                    ? 100 - verticalPercentage
                    : 100;

            if (view.VerticalPercentage > view.MaxAvailableVerticalPercentage)
            {
                VerticalPercentageChanged(-1);
            }

            MarkAsDirty();
        }

        public void HorizontalPercentageChanged(int horizontalPercentage)
        {
            view.HorizontalPercentage = horizontalPercentage;

            MarkAsDirty();
        }

        public void VerticalPercentageChanged(int verticalPercentage)
        {
            view.VerticalPercentage = verticalPercentage;

            MarkAsDirty();
        }

        public void ConfigureLedMapping()
        {
            var ledMappingForm = new ConfigureLedMappingForm(
                view.NumberOfHorizontalPixels.Value, 
                view.NumberOfVerticalPixels.Value, 
                view.PixelMappings == null ? null : view.PixelMappings.ToList());
            ledMappingForm.FinishedMapping += (s, a) =>
            {
                view.PixelMappings = a.Value;

                MarkAsDirty();
            };

            ledMappingForm.Show();
        }

        public void Cancel()
        {
            if (cts != null)
            {
                try
                {
                    cts.Cancel();
                    cts.Dispose();
                }
                catch (ObjectDisposedException)
                {
                }
            }

            CancelPresenting();
        }

        public void Confirm()
        {
            if (cts != null)
            {
                try
                {
                    cts.Cancel();
                    cts.Dispose();
                }
                catch (ObjectDisposedException)
                {
                }              
            }

            var serverConfig = ExplicitlyMap
                .TheseTypes<IAddServerView, ServerConfiguration>()
                .Using<AddServerViewToServerConfigurationMap>()
                .Map(view);

            FinishPresenting(serverConfig);
        }
    }
}
