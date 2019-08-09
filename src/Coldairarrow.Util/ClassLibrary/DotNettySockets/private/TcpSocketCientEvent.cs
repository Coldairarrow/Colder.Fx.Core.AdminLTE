using System;

namespace Coldairarrow.Util.DotNettySockets
{
    public class TcpSocketCientEvent<TSocketClient, TData>
    {
        public Action<TSocketClient> OnClientStarted { get; set; }
        public Action<TSocketClient, TData> OnRecieve { get; set; }
        public Action<TSocketClient, TData> OnSend { get; set; }
        public Action<TSocketClient> OnClientClose { get; set; }
        public Action<Exception> OnException { get; set; }
    }
}
