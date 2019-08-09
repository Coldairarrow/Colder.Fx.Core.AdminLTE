using Microsoft.AspNetCore.Hosting.Server;
using System;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface IBaseTcpSocketServerBuilder<TBuilder, TTarget, IConnection, TData> : IBaseBuilder<TBuilder, TTarget>
    {
        TBuilder OnServerStarted(Action<TTarget> action);

        TBuilder OnNewConnection(Action<TTarget, IConnection> action);

        TBuilder OnRecieve(Action<TTarget, IConnection, TData> action);

        TBuilder OnSend(Action<TTarget, IConnection, TData> action);

        TBuilder OnConnectionClose(Action<TTarget, IConnection> action);
    }
}
