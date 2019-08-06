using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpSocketClient : ITcpSocketClient, IChannelEvent
    {
        public TcpSocketClient(string ip, int port, ITcpClientEvent clientEvent)
        {
            Ip = ip;
            Port = port;
            _clientEvent = clientEvent;
        }
        private ITcpClientEvent _clientEvent { get; }
        private IChannel _channel { get; set; }
        private void PackException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _clientEvent.OnException?.Invoke(ex);
            }
        }

        public string Ip { get; }

        public int Port { get; }

        public void SetChannel(IChannel channel)
        {
            _channel = channel;
        }

        public void Close()
        {
            _channel.CloseAsync();
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

        public void OnChannelActive(IChannel channel)
        {
            _clientEvent.OnClientStarted?.Invoke(this);
        }

        public void OnChannelReceive(IChannel channel, object msg)
        {
            PackException(() =>
            {
                var bytes = (msg as IByteBuffer).ToArray();

                _clientEvent.OnRecieve(this, bytes);
            });
        }

        public void OnChannelInactive(IChannel channel)
        {
            _clientEvent.OnClientClose(this);
        }

        public void OnException(IChannel channel, Exception exception)
        {
            _clientEvent.OnException?.Invoke(exception);
            Close();
        }
    }
}
