using DotNetty.Transport.Channels;
using System;

namespace Coldairarrow.Util.DotNettySockets
{
    interface IChannelEvent
    {
        void OnChannelActive(IChannel channel);
        void OnChannelReceive(IChannel channel, object msg);
        void OnChannelInactive(IChannel channel);
        void OnException(IChannel channel, Exception exception);
    }
}