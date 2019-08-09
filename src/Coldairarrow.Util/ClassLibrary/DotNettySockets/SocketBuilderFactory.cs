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
    }
}
