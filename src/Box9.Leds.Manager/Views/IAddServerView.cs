using System;
using System.Collections.Generic;
using Box9.Leds.Business.EventsArguments;
using Box9.Leds.Core.EventsArguments;
using Box9.Leds.Core.UpdatePixels;

namespace Box9.Leds.Manager.Views
{
    public interface IAddServerView
    {
        event EventHandler<EventArgs> ScanForServers;

        event EventHandler<StringEventArgs> ServerSelected;

        event EventHandler<StringEventArgs> NumberOfHorizontalPixelsChanged;

        event EventHandler<StringEventArgs> NumberOfVerticalPixelsChanged;

        event EventHandler<IntegerEventArgs> StartAtHorizontalPercentageChanged;

        event EventHandler<IntegerEventArgs> StartAtVerticalPercentageChanged;

        event EventHandler<IntegerEventArgs> HorizontalPercentageChanged;

        event EventHandler<IntegerEventArgs> VerticalPercentageChanged;

        event EventHandler<EventArgs> ConfigureLedMapping;

        event EventHandler<EventArgs> Cancel;

        event EventHandler<EventArgs> Confirm;

        event EventHandler<ServerConfigurationEventArgs> ServerAddedOrUpdated;

        string SelectedServer { get; set; }

        int? ScanProgressPercentage { get; set; }

        List<string> Servers { get; set; }

        int? NumberOfHorizontalPixels { get; set; }

        int? NumberOfVerticalPixels { get; set; }

        int StartAtHorizontalPercentage { get; set; }

        int StartAtVerticalPercentage { get; set; }

        int HorizontalPercentage { get; set; }

        int MaxAvailableHorizontalPercentage { get; set; }

        int VerticalPercentage { get; set; }

        int MaxAvailableVerticalPercentage { get; set; }

        IEnumerable<PixelInfo> PixelMappings { get; set; }
    }
}
