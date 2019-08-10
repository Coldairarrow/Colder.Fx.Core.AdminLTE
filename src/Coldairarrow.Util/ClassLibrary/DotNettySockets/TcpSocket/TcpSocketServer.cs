using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpSocketServer : BaseTcpSocketServer<ITcpSocketServer, ITcpSocketConnection, byte[]>, ITcpSocketServer
    {
        public TcpSocketServer(int port, TcpSocketServerEvent<ITcpSocketServer, ITcpSocketConnection, byte[]> eventHandle)
            : base(port, eventHandle)
        {
        }

        public override void OnChannelReceive(IChannelHandlerContext ctx, object msg)
        {
            PackException(() =>
            {
                var bytes = (msg as IByteBuffer).ToArray();
                var theConnection = GetConnection(ctx.Channel);
                _eventHandle.OnRecieve?.Invoke(this, theConnection, bytes);
            });
        }

        protected override ITcpSocketConnection BuildConnection(IChannel clientChannel)
        {
            return new TcpSocketConnection(this, clientChannel, _eventHandle);
        }
    }
}
