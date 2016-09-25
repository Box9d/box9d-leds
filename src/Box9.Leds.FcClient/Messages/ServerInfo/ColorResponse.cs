using Newtonsoft.Json;

namespace Box9.Leds.FcClient.Messages.ServerInfo
{
    public class ColorResponse
    {
        [JsonProperty(PropertyName = "gamma")]
        public double Gamma { get; set; }

        [JsonProperty(PropertyName = "whitepoint")]
        public int[] WhitePoint { get; set; }
    }
}
