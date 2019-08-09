using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpSocketClientBuilder : BaseTcpSocketClientBuilder<ITcpSocketClientBuilder, ITcpSocketClient, byte[]>, ITcpSocketClientBuilder
    {
        public TcpSocketClientBuilder(string ip, int port)
            : base(ip, port)
        {

        }

        public async override Task<ITcpSocketClient> BuildAsync()
        {
            TcpSocketClient tcpClient = new TcpSocketClient(_ip, _port, _event);

            var clientChannel = await new Bootstrap()
                .Group(new MultithreadEventLoopGroup())
                .Channel<TcpSocketChannel>()
                .Option(ChannelOption.TcpNodelay, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(_pipeines.ToArray());
                    pipeline.AddLast(new CommonChannelHandler(tcpClient));
                })).ConnectAsync($"{_ip}:{_port}".ToIPEndPoint());

            tcpClient.SetChannel(clientChannel);

            return await Task.FromResult(tcpClient);
        }
    }
}
