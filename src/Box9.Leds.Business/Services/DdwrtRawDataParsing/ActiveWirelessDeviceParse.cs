using System.Collections.Generic;
using System.Linq;

namespace Box9.Leds.Business.Services.DdwrtRawDataParsing
{
    public class ActiveWirelessDeviceParse
    {
        internal string MacAddress { get; private set; }

        internal int SignalStrengthPercentage { get; private set; }

        internal ActiveWirelessDeviceParse(IEnumerable<string> dataBlocks)
        {
            var data = dataBlocks
                .ToArray();

            MacAddress = data[0].Replace("'", string.Empty);
            SignalStrengthPercentage = int.Parse(data[9].Replace("'", string.Empty)) / 10;
        }
    }
}
