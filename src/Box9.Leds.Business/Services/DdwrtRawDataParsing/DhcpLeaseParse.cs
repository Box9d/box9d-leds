using System.Collections.Generic;
using System.Linq;

namespace Box9.Leds.Business.Services.DdwrtRawDataParsing
{
    public class DhcpLeaseParse
    {
        internal string DeviceName { get; private set; }

        internal string IpAddress { get; private set; }

        internal string MacAddress { get; private set; }

        internal DhcpLeaseParse(IEnumerable<string> dataBlocks)
        {
            var data = dataBlocks.ToArray();

            DeviceName = data[0].Replace("'", string.Empty);
            IpAddress = data[1].Replace("'", string.Empty);
            MacAddress = data[2].Replace("'", string.Empty);
        }
    }
}
