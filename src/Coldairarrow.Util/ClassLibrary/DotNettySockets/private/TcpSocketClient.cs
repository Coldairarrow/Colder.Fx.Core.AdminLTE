using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpSocketClient : BaseTcpSocketClient<ITcpSocketClient, byte[]>, ITcpSocketClient
    {
        public TcpSocketClient(string ip, int port, IBaseTcpSocketCientEvent<ITcpSocketClient, byte[]> clientEvent)
            : base(ip, port, clientEvent)
        {
        }

        public override void OnChannelReceive(IChannel channel, object msg)
        {
            PackException(() =>
            {
                var bytes = (msg as IByteBuffer).ToArray();
                _clientEvent.OnRecieve?.Invoke(this, bytes);
            });
        }

        public async Task Send(byte[] bytes)
        {
            await _channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(bytes));
            await Task.Run(() =>
            {
                _clientEvent.OnSend?.Invoke(this, bytes);
            });
        }

        public async Task Send(string msgStr)
        {
            await Send(Encoding.UTF8.GetBytes(msgStr));
        }
    }
}
