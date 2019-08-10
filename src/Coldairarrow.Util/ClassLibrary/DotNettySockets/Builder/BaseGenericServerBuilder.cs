using System;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseGenericServerBuilder<TBuilder, TTarget, IConnection, TData> : 
        BaseBuilder<TBuilder, TTarget>,
        IGenericServerBuilder<TBuilder, TTarget, IConnection, TData>
        where TBuilder : class
    {
        public BaseGenericServerBuilder(int port)
            : base(port)
        {
        }

        protected TcpSocketServerEvent<TTarget, IConnection, TData> _event { get; }
            = new TcpSocketServerEvent<TTarget, IConnection, TData>();
        public TBuilder OnConnectionClose(Action<TTarget, IConnection> action)
        {
            _event.OnConnectionClose = action;

            return this as TBuilder;
        }

        public TBuilder OnNewConnection(Action<TTarget, IConnection> action)
        {
            _event.OnNewConnection = action;

            return this as TBuilder;
        }

        public TBuilder OnServerStarted(Action<TTarget> action)
        {
            _event.OnServerStarted = action;

            return this as TBuilder;
        }

        public override TBuilder OnException(Action<Exception> action)
        {
            _event.OnException = action;

            return this as TBuilder;
        }

        public TBuilder OnRecieve(Action<TTarget, IConnection, TData> action)
        {
            _event.OnRecieve = action;

            return this as TBuilder;
        }

        public TBuilder OnSend(Action<TTarget, IConnection, TData> action)
        {
            _event.OnSend = action;

            return this as TBuilder;
        }
    }
}
