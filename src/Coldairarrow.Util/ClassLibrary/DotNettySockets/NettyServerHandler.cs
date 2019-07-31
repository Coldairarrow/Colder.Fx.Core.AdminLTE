using Coldairarrow.Util;
using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Text;

namespace Coldairarrow.Util.DotNettySockets
{
    class ServerHandler : ChannelHandlerAdapter
    {
        public ServerHandler(ServerEventHandle eventHandle)
        {
            _eventHandle = eventHandle;
        }
        ServerEventHandle _eventHandle { get; }
        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var msg = message as IByteBuffer;
            
        }
        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            Console.WriteLine("Exception: " + exception);
            context.CloseAsync();
        }
    }
}