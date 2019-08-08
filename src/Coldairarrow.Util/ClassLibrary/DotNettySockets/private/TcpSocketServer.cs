using DotNetty.Buffers;
using DotNetty.Transport.Channels;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpSocketServer : BaseTcpSocketServer<ITcpSocketServer, ITcpSocketConnection, byte[]>, ITcpSocketServer
    {
        public TcpSocketServer(int port, IBaseTcpSocketServerEvent<ITcpSocketServer, ITcpSocketConnection, byte[]> eventHandle)
            : base(port, eventHandle)
        {
        }

        public override void OnChannelReceive(IChannel clientChannel, object msg)
        {
            var bytes = (msg as IByteBuffer).ToArray();
            PackException(() =>
            {
                var theConnection = GetConnection(clientChannel);
                _eventHandle.OnRecieve?.Invoke(this, theConnection, bytes);
            });
        }

        protected override ITcpSocketConnection BuildConnection(IChannel clientChannel)
        {
            return new TcpSocketConnection(this, _serverChannel, _eventHandle);
        }
    }
}
