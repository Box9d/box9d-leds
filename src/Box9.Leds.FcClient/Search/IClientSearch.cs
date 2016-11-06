using System;
using System.Collections.Generic;
using System.Threading;
using Box9.Leds.Core.EventsArguments;

namespace Box9.Leds.FcClient.Search
{
    public interface IClientSearch
    {
        event EventHandler<StringEventArgs> ServerFound;

        event EventHandler<IntegerEventArgs> PercentageSearched;

        void SearchForFadecandyServers(IEnumerable<string> ipAddresses, int pingTimeoutInMilliseconds, CancellationToken cancellationToken);
    }
}
