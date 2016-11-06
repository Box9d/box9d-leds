using System;
using System.Threading;
using System.Threading.Tasks;
using Box9.Leds.Core.EventsArguments;

namespace Box9.Leds.FcClient.Search
{
    public interface IClientSearch
    {
        event EventHandler<StringEventArgs> ServerFound;

        event EventHandler<IntegerEventArgs> PercentageSearched;

        void SearchForFadecandyServers(Uri[] uris, int pingTimeoutInMilliseconds, CancellationToken cancellationToken);
    }
}
