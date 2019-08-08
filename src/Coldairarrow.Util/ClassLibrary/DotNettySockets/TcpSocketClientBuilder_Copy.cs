//using DotNetty.Codecs;
//using DotNetty.Transport.Bootstrapping;
//using DotNetty.Transport.Channels;
//using DotNetty.Transport.Channels.Sockets;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Coldairarrow.Util.DotNettySockets
//{
//    public class TcpSocketClientBuilder
//    {
//        static TcpSocketClientBuilder()
//        {
//            _bootstrap = new Bootstrap()
//                .Group(new MultithreadEventLoopGroup())
//                .Channel<TcpSocketChannel>()
//                .Option(ChannelOption.TcpNodelay, true);
//        }
//        public TcpSocketClientBuilder(string ip, int port)
//        {
//            _ip = ip;
//            _port = port;
//        }
//        static Bootstrap _bootstrap { get; }
//        private string _ip { get; }
//        private int _port { get; }
//        private ITcpClientEvent _event { get; } = new EventContainer();

//        private List<IChannelHandler> _pipeines { get; } = new List<IChannelHandler>();

//        /// <summary>
//        /// 设置基于长度的解码器,解决粘包与分包问题
//        /// </summary>
//        /// <param name="maxFrameLength">最大长度</param>
//        /// <param name="lengthFieldOffset">长度字段偏移量</param>
//        /// <param name="lengthFieldLength">长度字段占字节数</param>
//        /// <param name="lengthAdjustment">添加到长度字段的补偿值</param>
//        /// <param name="initialBytesToStrip">从解码帧中开始去除的字节数</param>
//        /// <returns></returns>
//        public TcpSocketClientBuilder SetLengthFieldDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip)
//        {
//            _pipeines.Add(new LengthFieldBasedFrameDecoder(maxFrameLength, lengthFieldOffset, lengthFieldLength, lengthAdjustment, initialBytesToStrip));

//            return this;
//        }

//        /// <summary>
//        /// 设置基于长度的编码器,解决粘包与分包问题
//        /// </summary>
//        /// <param name="lengthFieldLength">长度字段占字节数</param>
//        /// <returns></returns>
//        public TcpSocketClientBuilder SetLengthFieldEncoder(int lengthFieldLength)
//        {
//            _pipeines.Add(new LengthFieldPrepender(lengthFieldLength));

//            return this;
//        }

//        public TcpSocketClientBuilder OnClientStarted(Action<ITcpSocketClient> action)
//        {
//            _event.OnClientStarted = action;

//            return this;
//        }

//        public TcpSocketClientBuilder OnRecieve(Action<ITcpSocketClient, byte[]> action)
//        {
//            _event.OnRecieve = action;

//            return this;
//        }

//        public TcpSocketClientBuilder OnSend(Action<ITcpSocketClient, byte[]> action)
//        {
//            _event.OnSend = action;

//            return this;
//        }

//        public TcpSocketClientBuilder OnClientClose(Action<ITcpSocketClient> action)
//        {
//            _event.OnClientClose = action;

//            return this;
//        }

//        public TcpSocketClientBuilder OnException(Action<Exception> action)
//        {
//            _event.OnException = action;

//            return this;
//        }

//        public async Task<ITcpSocketClient> BuildAsync()
//        {
//            TcpSocketClient tcpClient = new TcpSocketClient(_ip, _port, _event);
//            var clientChannel = await _bootstrap
//                .Handler(new ActionChannelInitializer<IChannel>(channel =>
//                {
//                    IChannelPipeline pipeline = channel.Pipeline;
//                    pipeline.AddLast(_pipeines.ToArray());
//                    pipeline.AddLast(new CommonChannelHandler(tcpClient));
//                })).ConnectAsync($"{_ip}:{_port}".ToIPEndPoint());

//            tcpClient.SetChannel(clientChannel);

//            return await Task.FromResult(tcpClient);
//        }
//    }
//}
