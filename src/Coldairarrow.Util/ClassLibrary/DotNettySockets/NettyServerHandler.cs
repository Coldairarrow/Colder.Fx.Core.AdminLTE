using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;

namespace Coldairarrow.Util.DotNettySockets
{
    class ServerHandler : ChannelHandlerAdapter
    {
        public ServerHandler(TcpServer tcpServer)
        {
            _tcpServer = tcpServer;
        }
        TcpServer _tcpServer { get; }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            var theConnection = new TcpConnection(_tcpServer, context.Channel);
            _tcpServer.AddConnection(theConnection);
            _tcpServer.EventHandle.HandleNewClientConnected?.Invoke(_tcpServer, theConnection);
        }

        public override void ChannelRead(IChannelHandlerContext context, object message)
        {
            var msg = message as IByteBuffer;
            var theConnection = _tcpServer.GetConnectionById(context.Channel.Id.AsLongText());
            _tcpServer.EventHandle.HandleRecMsg?.Invoke(_tcpServer, theConnection, msg.Array);
        }

        public override void ChannelReadComplete(IChannelHandlerContext context) => context.Flush();

        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            _tcpServer.EventHandle.HandleException?.Invoke(exception);
            var theConnection = _tcpServer.GetConnectionById(context.Channel.Id.AsLongText());
            theConnection.StopAsync();
        }
    }
}