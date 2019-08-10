using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpSocketServerBuilder :
        BaseGenericServerBuilder<ITcpSocketServerBuilder, ITcpSocketServer, ITcpSocketConnection, byte[]>,
        ITcpSocketServerBuilder
    {
        public TcpSocketServerBuilder(int port)
            : base(port)
        {
        }

        protected Action<IChannelPipeline> _setEncoder { get; set; }

        public ITcpSocketServerBuilder SetLengthFieldDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip)
        {
            _setEncoder += x => x.AddLast(new LengthFieldBasedFrameDecoder(maxFrameLength, lengthFieldOffset, lengthFieldLength, lengthAdjustment, initialBytesToStrip));

            return this;
        }

        public ITcpSocketServerBuilder SetLengthFieldEncoder(int lengthFieldLength)
        {
            _setEncoder += x => x.AddLast(new LengthFieldPrepender(lengthFieldLength));

            return this;
        }

        public async override Task<ITcpSocketServer> BuildAsync()
        {
            TcpSocketServer tcpServer = new TcpSocketServer(_port, _event);

            var serverChannel = await new ServerBootstrap()
                .Group(new MultithreadEventLoopGroup(), new MultithreadEventLoopGroup())
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 1024)
                .Option(ChannelOption.TcpNodelay, true)
                .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    _setEncoder?.Invoke(pipeline);
                    pipeline.AddLast(new CommonChannelHandler(tcpServer));
                })).BindAsync(_port);
            _event.OnServerStarted?.Invoke(tcpServer);
            tcpServer.SetChannel(serverChannel);

            return await Task.FromResult(tcpServer);
        }
    }
}