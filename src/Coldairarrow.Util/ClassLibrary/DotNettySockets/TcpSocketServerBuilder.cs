using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    public class TcpSocketServerBuilder
    {
        static TcpSocketServerBuilder()
        {
            _serverBootstrap = new ServerBootstrap()
                .Group(new MultithreadEventLoopGroup(), new MultithreadEventLoopGroup())
                .Channel<TcpServerSocketChannel>()
                .Option(ChannelOption.SoBacklog, 1024)
                .Option(ChannelOption.TcpNodelay, true);
        }
        public TcpSocketServerBuilder(int port)
        {
            _port = port;
        }
        private int _port { get; }
        private EventContainer _eventHandle { get; } = new EventContainer();
        private static ServerBootstrap _serverBootstrap { get; }

        private List<IChannelHandler> _pipeines { get; } = new List<IChannelHandler>();

        /// <summary>
        /// 设置基于长度的解码器,解决粘包与分包问题
        /// </summary>
        /// <param name="maxFrameLength">最大长度</param>
        /// <param name="lengthFieldOffset">长度字段偏移量</param>
        /// <param name="lengthFieldLength">长度字段占字节数</param>
        /// <param name="lengthAdjustment">添加到长度字段的补偿值</param>
        /// <param name="initialBytesToStrip">从解码帧中开始去除的字节数</param>
        /// <returns></returns>
        public TcpSocketServerBuilder SetLengthFieldDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip)
        {
            _pipeines.Add(new LengthFieldBasedFrameDecoder(maxFrameLength, lengthFieldOffset, lengthFieldLength, lengthAdjustment, initialBytesToStrip));

            return this;
        }

        /// <summary>
        /// 设置基于长度的编码器,解决粘包与分包问题
        /// </summary>
        /// <param name="lengthFieldLength">长度字段占字节数</param>
        /// <returns></returns>
        public TcpSocketServerBuilder SetLengthFieldEncoder(int lengthFieldLength)
        {
            _pipeines.Add(new LengthFieldPrepender(lengthFieldLength));

            return this;
        }

        public TcpSocketServerBuilder OnServerStarted(Action<ITcpSocketServer> action)
        {
            _eventHandle.OnServerStarted = action;
            return this;
        }

        public TcpSocketServerBuilder OnNewConnection(Action<ITcpSocketServer, ITcpSocketConnection> action)
        {
            _eventHandle.OnNewConnection = action;

            return this;
        }

        public TcpSocketServerBuilder OnRecieve(Action<ITcpSocketServer, ITcpSocketConnection, byte[]> action)
        {
            _eventHandle.OnRecieve = action;

            return this;
        }

        public TcpSocketServerBuilder OnSend(Action<ITcpSocketServer, ITcpSocketConnection, byte[]> action)
        {
            _eventHandle.OnSend = action;

            return this;
        }

        public TcpSocketServerBuilder OnConnectionClose(Action<ITcpSocketServer, ITcpSocketConnection> action)
        {
            _eventHandle.OnConnectionClose = action;

            return this;
        }

        public TcpSocketServerBuilder OnException(Action<Exception> action)
        {
            _eventHandle.OnException = action;

            return this;
        }

        public async Task<ITcpSocketServer> BuildAsync()
        {
            TcpSocketServer tcpServer = new TcpSocketServer(_port, _eventHandle);
            var serverChannel = await _serverBootstrap
                .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(_pipeines.ToArray());
                    pipeline.AddLast(new CommonChannelHandler(tcpServer));
                })).BindAsync(_port);
            _eventHandle.OnServerStarted?.Invoke(tcpServer);
            tcpServer.SetChannel(serverChannel);

            return await Task.FromResult(tcpServer);
        }
    }
}