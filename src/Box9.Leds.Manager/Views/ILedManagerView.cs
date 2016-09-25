using System;
using System.Collections.Generic;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Dtos;
using Box9.Leds.Core.EventsArguments;

namespace Box9.Leds.Manager.Views
{
    public interface ILedManagerView
    {
        event EventHandler<StringEventArgs> SaveConfiguration;

        event EventHandler<StringEventArgs> OpenConfiguration;

        event EventHandler<EventArgs> NewConfiguration;

        event EventHandler<EventArgs> AddNewServer;

        event EventHandler<StringEventArgs> EditServer;

        event EventHandler<StringEventArgs> RemoveServer;

        event EventHandler<StringEventArgs> ImportVideo;

        event EventHandler<BooleanEventArgs> DisplayVideoToggle;

        event EventHandler<IntegerEventArgs> PreviewBrightnessChanged;

        event EventHandler<IntegerEventArgs> BrightnessChanged;

        event EventHandler<IntegerEventArgs> StartTimeChanged;

        event EventHandler<EventArgs> InitializePlayback;

        event EventHandler<EventArgs> Play;

        event EventHandler<EventArgs> Stop;

        List<ServerConfiguration> Servers { get; set; }

        string ConfigurationFilePath { get; set; }

        VideoMetadata VideoMetadata { get; set; }

        TimeSpan? TotalVideoLength { get; set; }

        bool DisplayVideo { get; set; }

        int BrightnessPercentage { get; set; }
    }
}
