using System;
using Newtonsoft.Json;

namespace Box9.Leds.Core.Messages.ColorCorrection
{
    public class ColorCorrectionColor
    {
        [JsonProperty(PropertyName = "gamma")]
        public string Gamma
        {
            get
            {
                return "2.5";
            }
        }

        [JsonProperty(PropertyName = "whitepoint")]
        public string Whitepoint { get; private set; }

        public ColorCorrectionColor(int brightnessPercent)
        {
            Whitepoint = Math.Round((double)brightnessPercent / 100, 1).ToString();
        }
    }
}
