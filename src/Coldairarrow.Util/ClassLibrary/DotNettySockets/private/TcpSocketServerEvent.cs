using System;

namespace Coldairarrow.Util.DotNettySockets
{
    public class TcpSocketServerEvent<TSocketServer, TConnection, TData>
    {
        public Action<TSocketServer> OnServerStarted { get; set; }
        public Action<TSocketServer, TConnection> OnNewConnection { get; set; }
        public Action<TSocketServer, TConnection, TData> OnRecieve { get; set; }
        public Action<TSocketServer, TConnection, TData> OnSend { get; set; }
        public Action<TSocketServer, TConnection> OnConnectionClose { get; set; }
        public Action<Exception> OnException { get; set; }
    }
}
