using Newtonsoft.Json;

namespace Box9.Leds.FcClient.Messages.ColorCorrection
{
    public class ColorCorrectionDevice
    {
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                return "fadecandy";
            }
        }

        [JsonProperty(PropertyName = "serial")]
        public string Serial { get; private set; }

        public ColorCorrectionDevice(string serial)
        {
            Serial = serial;
        }
    }
}
