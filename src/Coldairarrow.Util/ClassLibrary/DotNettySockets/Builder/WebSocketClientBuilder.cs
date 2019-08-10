using DotNetty.Codecs.Http;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class WebSocketClientBuilder : BaseGenericClientBuilder<IWebSocketClientBuilder, IWebSocketClient, string>, IWebSocketClientBuilder
    {
        public WebSocketClientBuilder(string ip, int port, string path)
            : base(ip, port)
        {
            _path = path;
        }
        private string _path { get; }

        public async override Task<IWebSocketClient> BuildAsync()
        {
            WebSocketClient tcpClient = new WebSocketClient(_ip, _port, _path, _event);

            var clientChannel = await new Bootstrap()
                .Group(new MultithreadEventLoopGroup())
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(
                        new HttpClientCodec(),
                        new HttpObjectAggregator(8192),
                        new CommonChannelHandler(tcpClient));
                })).ConnectAsync($"{_ip}:{_port}".ToIPEndPoint());
            await tcpClient.HandshakeCompletion;
            tcpClient.SetChannel(clientChannel);

            return await Task.FromResult(tcpClient);
        }
    }
}
