namespace Coldairarrow.Util.DotNettySockets
{
    public class SocketBuilderFactory
    {
        public static ITcpSocketClientBuilder GetTcpSocketClientBuilder(string ip, int port)
        {
            return new TcpSocketClientBuilder(ip, port);
        }

        public static ITcpSocketServerBuilder GetTcpSocketServerBuilder(int port)
        {
            return new TcpSocketServerBuilder(port);
        }

        public static IWebSocketServerBuilder GetWebSocketServerBuilder(int port, string path = "/")
        {
            return new WebSocketServerBuilder(port, path);
        }

        public static IWebSocketClientBuilder GetWebSocketClientBuilder(string ip, int port, string path = "/")
        {
            return new WebSocketClientBuilder(ip, port, path);
        }

        public static IUdpSocketBuilder GetUdpSocketBuilder(int port = 0)
        {
            return new UdpSocketBuilder(port);
        }
    }
}
