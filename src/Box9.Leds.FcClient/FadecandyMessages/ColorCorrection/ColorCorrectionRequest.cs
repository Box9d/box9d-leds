using Newtonsoft.Json;

namespace Box9.Leds.FcClient.FadecandyMessages.ColorCorrection
{
    public class ColorCorrectionRequest : IJsonRequest<ColorCorrectionResponse>
    {
        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                return "device_color_correction";
            }
        }

        [JsonProperty(PropertyName = "device")]
        public ColorCorrectionDevice Device { get; private set; }

        [JsonProperty(PropertyName = "color")]
        public ColorCorrectionColor Color { get; private set; }

        public ColorCorrectionRequest(int brightnessPercent, string serial)
        {
            Device = new ColorCorrectionDevice(serial);
            Color = new ColorCorrectionColor(brightnessPercent);
        }
    }
}
