using System;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseTcpSocketClientBuilder<TBuilder, TSocketClient, TData> : BaseBuilder<TBuilder, TSocketClient>, IBaseTcpSocketClientBuilder<TBuilder, TSocketClient, TData>
        where TBuilder : class
    {
        public BaseTcpSocketClientBuilder(string ip, int port)
            : base(port)
        {
            _ip = ip;
        }

        protected string _ip { get; }
        protected TcpSocketCientEvent<TSocketClient, TData> _event { get; }
            = new TcpSocketCientEvent<TSocketClient, TData>();

        public TBuilder OnClientClose(Action<TSocketClient> action)
        {
            _event.OnClientClose = action;

            return this as TBuilder;
        }

        public TBuilder OnClientStarted(Action<TSocketClient> action)
        {
            _event.OnClientStarted = action;

            return this as TBuilder;
        }

        public TBuilder OnRecieve(Action<TSocketClient, TData> action)
        {
            _event.OnRecieve = action;

            return this as TBuilder;
        }

        public TBuilder OnSend(Action<TSocketClient, TData> action)
        {
            _event.OnSend = action;

            return this as TBuilder;
        }

        public override TBuilder OnException(Action<Exception> action)
        {
            _event.OnException = action;

            return this as TBuilder;
        }
    }
}
