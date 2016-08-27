using System;
using Newtonsoft.Json;

namespace Box9.Leds.Core.Messages.ColorCorrection
{
    public class ColorCorrectionColor
    {
        [JsonProperty(PropertyName = "gamma")]
        public double Gamma
        {
            get
            {
                return 2.5;
            }
        }

        [JsonProperty(PropertyName = "whitepoint")]
        public double[] Whitepoint { get; private set; }

        public ColorCorrectionColor(int brightnessPercent)
        {
            var whitepoint = Math.Round((double)brightnessPercent / 100, 1);

            Whitepoint = new double[3] { whitepoint, whitepoint, whitepoint };
        }
    }
}
