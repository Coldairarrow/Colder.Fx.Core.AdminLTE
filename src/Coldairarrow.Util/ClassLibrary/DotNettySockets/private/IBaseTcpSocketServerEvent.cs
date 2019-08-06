using System;

namespace Coldairarrow.Util.DotNettySockets
{
    interface IBaseTcpSocketServerEvent<TSocketServer, TConnection, TData>
    {
        Action<TSocketServer> OnServerStarted { get; set; }
        Action<TSocketServer, TConnection> OnNewConnection { get; set; }
        Action<TSocketServer, TConnection, TData> OnRecieve { get; set; }
        Action<TSocketServer, TConnection, TData> OnSend { get; set; }
        Action<TSocketServer, TConnection> OnConnectionClose { get; set; }
        Action<Exception> OnException { get; set; }
    }
}
