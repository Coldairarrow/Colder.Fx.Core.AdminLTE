using System;

namespace Coldairarrow.Util.DotNettySockets
{
    class EventContainer : ITcpSocketServerEvent, ITcpClientEvent
    {
        public Action<ITcpSocketServer> OnServerStarted { get; set; }
        public Action<ITcpSocketServer, ITcpSocketConnection> OnNewConnection { get; set; }
        public Action<ITcpSocketServer, ITcpSocketConnection, byte[]> OnRecieve { get; set; }
        public Action<ITcpSocketServer, ITcpSocketConnection, byte[]> OnSend { get; set; }
        public Action<ITcpSocketServer, ITcpSocketConnection> OnConnectionClose { get; set; }
        public Action<Exception> OnException { get; set; }

        public Action<ITcpSocketClient> OnClientStarted { get; set; }
        public Action<ITcpSocketClient> OnClientClose { get; set; }
        Action<ITcpSocketClient, byte[]> ITcpClientEvent.OnRecieve { get; set; }
        Action<ITcpSocketClient, byte[]> ITcpClientEvent.OnSend { get; set; }
    }
}