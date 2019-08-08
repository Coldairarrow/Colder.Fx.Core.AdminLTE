using DotNetty.Transport.Channels;
using System;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseTcpSocketClient<TSocketClient, TData> : IBaseTcpSocketClient, IChannelEvent
        where TSocketClient : class, IBaseTcpSocketClient
    {
        public BaseTcpSocketClient(string ip, int port, IBaseTcpSocketCientEvent<TSocketClient, TData> clientEvent)
        {
            Ip = ip;
            Port = port;
            _clientEvent = clientEvent;
        }
        protected IBaseTcpSocketCientEvent<TSocketClient, TData> _clientEvent { get; }
        protected IChannel _channel { get; set; }
        protected void PackException(Action action)
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

        public void OnChannelActive(IChannel channel)
        {
            _clientEvent.OnClientStarted?.Invoke(this as TSocketClient);
        }

        public abstract void OnChannelReceive(IChannel channel, object msg);

        public void OnChannelInactive(IChannel channel)
        {
            _clientEvent.OnClientClose(this as TSocketClient);
        }

        public void OnException(IChannel channel, Exception exception)
        {
            _clientEvent.OnException?.Invoke(exception);
            Close();
        }
    }
}
