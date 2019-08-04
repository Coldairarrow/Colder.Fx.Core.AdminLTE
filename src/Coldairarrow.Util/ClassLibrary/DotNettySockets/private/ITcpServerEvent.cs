using System;

namespace Coldairarrow.Util.DotNettySockets
{
    interface ITcpServerEvent
    {
        Action<ITcpSocketServer> OnServerStarted { get; set; }
        Action<ITcpSocketServer, ITcpSocketConnection> OnNewConnection { get; set; }
        Action<ITcpSocketServer, ITcpSocketConnection, byte[]> OnRecieve { get; set; }
        Action<ITcpSocketServer, ITcpSocketConnection, byte[]> OnSend { get; set; }
        Action<ITcpSocketServer, ITcpSocketConnection> OnConnectionClose { get; set; }
        Action<Exception> OnException { get; set; }
    }
}
