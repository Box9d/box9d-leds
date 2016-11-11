using Newtonsoft.Json;

namespace Box9.Leds.FcClient.PiSocketMessages.Frames
{
    public class Pixel
    {
        [JsonProperty(PropertyName = "r")]
        public int R { get; internal set; }

        [JsonProperty(PropertyName = "g")]
        public int G { get; internal set; }

        [JsonProperty(PropertyName = "b")]
        public int B { get; internal set; }

        public Pixel(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}
