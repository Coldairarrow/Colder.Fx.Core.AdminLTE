using System;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseTcpSocketServerBuilder<TBuilder, ITcpSocketServer, IConnection, TData> : BaseBuilder<TBuilder, ITcpSocketServer>, IBaseTcpSocketServerBuilder<TBuilder, ITcpSocketServer, IConnection, TData>
        where TBuilder : class
    {
        public BaseTcpSocketServerBuilder(int port)
            : base(port)
        {
        }

        protected TcpSocketServerEvent<ITcpSocketServer, IConnection, TData> _event { get; }
            = new TcpSocketServerEvent<ITcpSocketServer, IConnection, TData>();
        public TBuilder OnConnectionClose(Action<ITcpSocketServer, IConnection> action)
        {
            _event.OnConnectionClose = action;

            return this as TBuilder;
        }

        public TBuilder OnNewConnection(Action<ITcpSocketServer, IConnection> action)
        {
            _event.OnNewConnection = action;

            return this as TBuilder;
        }

        public TBuilder OnServerStarted(Action<ITcpSocketServer> action)
        {
            _event.OnServerStarted = action;

            return this as TBuilder;
        }

        public override TBuilder OnException(Action<Exception> action)
        {
            _event.OnException = action;

            return this as TBuilder;
        }

        public TBuilder OnRecieve(Action<ITcpSocketServer, IConnection, TData> action)
        {
            _event.OnRecieve = action;

            return this as TBuilder;
        }

        public TBuilder OnSend(Action<ITcpSocketServer, IConnection, TData> action)
        {
            _event.OnSend = action;

            return this as TBuilder;
        }
    }
}
