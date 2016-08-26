using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Box9.Leds.Core.Messages.ColorCorrection
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
