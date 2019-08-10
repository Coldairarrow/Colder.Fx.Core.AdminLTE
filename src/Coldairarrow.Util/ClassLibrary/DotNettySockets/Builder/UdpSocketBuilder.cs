using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class UdpSocketBuilder : IUdpSocketBuilder
    {
        public UdpSocketBuilder(int port)
        {
            Port = port;
        }
        public int Port { get; }
        UdpSocketEvent _socketEvent { get; } = new UdpSocketEvent();

        public IUdpSocketBuilder OnClose(Action<IUdpSocket> action)
        {
            _socketEvent.OnClose = action;

            return this;
        }

        public IUdpSocketBuilder OnException(Action<Exception> action)
        {
            _socketEvent.OnException = action;

            return this;
        }

        public IUdpSocketBuilder OnRecieve(Action<IUdpSocket, EndPoint, byte[]> action)
        {
            _socketEvent.OnRecieve = action;

            return this;
        }

        public IUdpSocketBuilder OnSend(Action<IUdpSocket, EndPoint, byte[]> action)
        {
            _socketEvent.OnSend = action;

            return this;
        }

        public IUdpSocketBuilder OnStarted(Action<IUdpSocket> action)
        {
            _socketEvent.OnStarted = action;

            return this;
        }

        public async Task<IUdpSocket> BuildAsync()
        {
            UdpSocket tcpClient = new UdpSocket(Port, _socketEvent);

            var clientChannel = await new Bootstrap()
                .Group(new MultithreadEventLoopGroup())
                .Channel<SocketDatagramChannel>()
                .Option(ChannelOption.SoBroadcast, true)
                .Handler(new ActionChannelInitializer<IChannel>(channel =>
                {
                    IChannelPipeline pipeline = channel.Pipeline;
                    pipeline.AddLast(new CommonChannelHandler(tcpClient));
                })).BindAsync(Port);

            tcpClient.SetChannel(clientChannel);

            return await Task.FromResult(tcpClient);
        }
    }
}
