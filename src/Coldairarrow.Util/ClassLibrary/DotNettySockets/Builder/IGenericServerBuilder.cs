using System;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface IGenericServerBuilder<TBuilder, TTarget, IConnection, TData> : IBuilder<TBuilder, TTarget>
    {
        TBuilder OnServerStarted(Action<TTarget> action);

        TBuilder OnNewConnection(Action<TTarget, IConnection> action);

        TBuilder OnRecieve(Action<TTarget, IConnection, TData> action);

        TBuilder OnSend(Action<TTarget, IConnection, TData> action);

        TBuilder OnConnectionClose(Action<TTarget, IConnection> action);
    }
}
