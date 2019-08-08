using System;

namespace Coldairarrow.Util.DotNettySockets
{
    interface IBaseTcpSocketCientEvent<TSocketClient, TData>
    {
        Action<TSocketClient> OnClientStarted { get; set; }
        Action<TSocketClient, TData> OnRecieve { get; set; }
        Action<TSocketClient, TData> OnSend { get; set; }
        Action<TSocketClient> OnClientClose { get; set; }
        Action<Exception> OnException { get; set; }
    }
}
