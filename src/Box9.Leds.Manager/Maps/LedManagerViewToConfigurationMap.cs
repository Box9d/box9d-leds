using System.Linq;
using Box9.Leds.Business.Configuration;
using Box9.Leds.Manager.Views;
using RickPowell.ExplicitMapping;

namespace Box9.Leds.Manager.Maps
{
    public class LedManagerViewToConfigurationMap : ISingleSourceMap<ILedManagerView, LedConfiguration>
    {
        public LedConfiguration Map(ILedManagerView source)
        {
            var ledConfig = new LedConfiguration();
            ledConfig.Servers = source.Servers.ToList();

            if (source.VideoMetadata != null)
            {
                ledConfig.VideoConfig = new VideoConfiguration
                {
                    SourceFilePath = source.VideoMetadata.FilePath,
                    VideoLength = source.VideoMetadata.TotalTime
                };
            }

            return ledConfig;
        }
    }
}
