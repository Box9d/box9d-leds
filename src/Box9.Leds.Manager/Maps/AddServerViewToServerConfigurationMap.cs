using System.Linq;
using Box9.ExplicitMapping;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Manager.Views;

namespace Box9.Leds.Manager.Maps
{
    public class AddServerViewToServerConfigurationMap : ISingleSourceMap<IAddServerView, ServerConfiguration>
    {
        public ServerConfiguration Map(IAddServerView source)
        {
            return new ServerConfiguration
            {
                IPAddress = source.SelectedServer,
                PixelMappings = source.PixelMappings.ToList(),
                Port = 7890,
                VideoConfiguration = new ServerVideoConfiguration
                {
                    StartAtXPercent = source.StartAtHorizontalPercentage,
                    StartAtYPercent = source.StartAtVerticalPercentage,
                    XPercent = source.HorizontalPercentage,
                    YPercent = source.VerticalPercentage
                },
                XPixels = source.NumberOfHorizontalPixels.HasValue ? source.NumberOfHorizontalPixels.Value : 0,
                YPixels = source.NumberOfVerticalPixels.HasValue ? source.NumberOfVerticalPixels.Value : 0
            };
        }
    }
}
