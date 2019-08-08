using System;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface IBaseTcpSocketClientBuilder<TBuilder, TSocketClient, TData> : IBaseBuilder<TBuilder>
    {
        TBuilder OnClientStarted(Action<TSocketClient> action);

        TBuilder OnRecieve(Action<TSocketClient, TData> action);

        TBuilder OnSend(Action<TSocketClient, TData> action);

        TBuilder OnClientClose(Action<TSocketClient> action);
    }
}
