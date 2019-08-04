using System;

namespace Coldairarrow.Util.DotNettySockets
{
    interface ITcpClientEvent
    {
        Action<ITcpSocketClient> OnClientStarted { get; set; }
        Action<ITcpSocketClient, byte[]> OnRecieve { get; set; }
        Action<ITcpSocketClient, byte[]> OnSend { get; set; }
        Action<ITcpSocketClient> OnClientClose { get; set; }
        Action<Exception> OnException { get; set; }
    }
}
