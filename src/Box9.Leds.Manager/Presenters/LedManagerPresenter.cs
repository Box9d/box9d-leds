using System;
using System.Collections.Generic;
using System.Linq;
using Box9.ExplicitMapping;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Services;
using Box9.Leds.Core.EventsArguments;
using Box9.Leds.Manager.Forms;
using Box9.Leds.Manager.Maps;
using Box9.Leds.Manager.Views;

namespace Box9.Leds.Manager.Presenters
{
    public class LedManagerPresenter : PresenterBase<int>
    {
        private readonly ILedManagerView view;
        private readonly IConfigurationStorageService configurationStorageService;
        private readonly IBrightnessService brightnessService;
        private readonly IVideoMetadataService videoMetadataService;

        public LedManagerPresenter(ILedManagerView view, 
            IConfigurationStorageService configurationStorageService, 
            IBrightnessService brightnessService,
            IVideoMetadataService videoMetadataService)
        {
            this.configurationStorageService = configurationStorageService;
            this.brightnessService = brightnessService;
            this.videoMetadataService = videoMetadataService;
            this.view = view;

            this.view.NewConfiguration += NewConfiguration;
            this.view.SaveConfiguration += SaveConfiguration;
            this.view.OpenConfiguration += OpenConfiguration;

            this.view.AddNewServer += AddNewServer;
            this.view.EditServer += EditServer;
            this.view.RemoveServer += RemoveServer;

            this.view.PreviewBrightnessChanged += PreviewBrightnessChanged;
            this.view.BrightnessChanged += BrightnessChanged;

            this.view.ImportVideo += ImportVideo;
            this.view.StartTimeChanged += StartTimeChanged;
            this.view.DisplayVideoToggle += DisplayVideoToggle;

            this.view.InitializePlayback += InitializePlayback;
            this.view.Play += Play;
            this.view.Stop += Stop;

            NewConfiguration(null, EventArgs.Empty);
        }

        private void NewConfiguration(object sender, EventArgs e)
        {
            NewView();
            MarkAsDirty();
        }

        private void SaveConfiguration(object sender, StringEventArgs e)
        {
            var configuration = ExplicitlyMap
                .TheseTypes<ILedManagerView, LedConfiguration>()
                .Using<LedManagerViewToConfigurationMap>()
                .Map(view);

            configurationStorageService.Save(configuration, e.Value);
            view.ConfigurationFilePath = e.Value;
        }

        private void OpenConfiguration(object sender, StringEventArgs e)
        {
            var config = configurationStorageService.Get(e.Value);
            var videoMetadata = config.VideoConfig != null ? videoMetadataService.GetMetadata(config.VideoConfig.SourceFilePath) : null;

            NewView();

            view.ConfigurationFilePath = e.Value;
            view.VideoMetadata = videoMetadata;
            view.Servers = config.Servers;
            view.TotalVideoLength = config.VideoConfig != null ? config.VideoConfig.VideoLength : (TimeSpan?)null;

            MarkAsDirty();
        }

        private void AddNewServer(object sender, EventArgs e)
        {
            var addServerForm = new AddServerForm();
            addServerForm.ServerAddedOrUpdated += (s, args) =>
            {
                view.Servers.Add(args.Value);

                MarkAsDirty();
            };
            addServerForm.Show();
        }

        private void EditServer(object sender, StringEventArgs e)
        {
            var selectedServer = view.Servers.SingleOrDefault(s => s.ToString() == e.Value);
            var addServerForm = new AddServerForm(selectedServer);

            addServerForm.ServerAddedOrUpdated += (s, args) =>
            {
                var index = view.Servers.IndexOf(selectedServer);

                view.Servers.Remove(selectedServer);
                view.Servers.Insert(index, args.Value);
            };
            addServerForm.Show();
        }

        private void RemoveServer(object sender, StringEventArgs e)
        {
            var servers = view.Servers.ToList();
            var removedServer = view.Servers.SingleOrDefault(s => s.ToString() == e.Value);
            if (removedServer != null)
            {
                servers.Remove(removedServer);
            }

            view.Servers = servers;

            MarkAsDirty();
        }

        private void PreviewBrightnessChanged(object sender, IntegerEventArgs e)
        {
            view.BrightnessPercentage = e.Value;
            MarkAsDirty();
        }

        private async void BrightnessChanged(object sender, IntegerEventArgs e)
        {
            var configuration = ExplicitlyMap
                .TheseTypes<ILedManagerView, LedConfiguration>()
                .Using<LedManagerViewToConfigurationMap>()
                .Map(view);

            var adjustBrightnessTask = brightnessService.AdjustBrightness(e.Value, configuration.Servers);

            try
            {
                await adjustBrightnessTask;
            }
            catch (Exception ex)
            {
                NotifyError(ex);
            }
        }

        private void ImportVideo(object sender, StringEventArgs e)
        {
            try
            {
                view.VideoMetadata = videoMetadataService.GetMetadata(e.Value);
            }
            catch (Exception ex)
            {
                NotifyError(ex);
            }

            MarkAsDirty();
        }

        private void StartTimeChanged(object sender, IntegerEventArgs e)
        {
            view.VideoMetadata.SetStartTime(e.Value);

            MarkAsDirty();
        }

        private void DisplayVideoToggle(object sender, BooleanEventArgs e)
        {
            view.DisplayVideo = e.Value;
        }

        private void InitializePlayback(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Play(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Stop(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewView()
        {
            view.ConfigurationFilePath = null;
            view.TotalVideoLength = new TimeSpan(0, 0, 0);
            view.Servers = new List<ServerConfiguration>();
            view.VideoMetadata = null;
            view.BrightnessPercentage = 100;
        }
    }
}
