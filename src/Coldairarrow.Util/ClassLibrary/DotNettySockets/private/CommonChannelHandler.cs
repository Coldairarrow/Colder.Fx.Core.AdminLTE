using DotNetty.Transport.Channels;
using System;

namespace Coldairarrow.Util.DotNettySockets
{
    class CommonChannelHandler : ChannelHandlerAdapter
    {
        public CommonChannelHandler(IChannelEvent channelEvent)
        {
            _channelEvent = channelEvent;
        }
        IChannelEvent _channelEvent { get; }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            _channelEvent.OnChannelActive(context.Channel);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            _channelEvent.OnChannelReceive(context.Channel, message);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context)
        {
            context.Flush();
        }

        public override void ChannelInactive(IChannelHandlerContext context)
        {
            _channelEvent.OnChannelInactive(context.Channel);
        }

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            _channelEvent.OnException(context.Channel, exception);
        }
    }
}