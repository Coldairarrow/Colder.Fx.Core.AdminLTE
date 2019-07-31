using System;

namespace Coldairarrow.Util.DotNettySockets
{
    class ServerEventHandle
    {
        public Action<ITcpServer> HandleServerStarted { get; set; }
        public Action<ITcpServer, ITcpConnection> HandleNewClientConnected { get; set; }
        public Action<ITcpServer, ITcpConnection, byte[]> HandleRecMsg { get; set; }
        public Action<ITcpServer, ITcpConnection, byte[]> HandleSendMsg { get; set; }
        public Action<ITcpServer, ITcpConnection> HandleClientClose { get; set; }
        public Action<Exception> HandleException { get; set; }
    }
}