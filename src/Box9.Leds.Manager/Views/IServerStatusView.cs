using Box9.Leds.Business.Configuration;
using Box9.Leds.Business.Dtos;

namespace Box9.Leds.Manager.Views
{
    public interface IServerStatusView
    {
        NetworkDetails NetworkDetails { get; set; }

        LedConfiguration LedConfiguration { get; set; }
    }
}
