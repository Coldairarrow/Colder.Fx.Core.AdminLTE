using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class UdpSocket : IUdpSocket, IChannelEvent
    {
        public UdpSocket(int port, UdpSocketEvent socketEvent)
        {
            Port = port;
            _event = socketEvent;
        }
        UdpSocketEvent _event { get; }
        public int Port { get; }
        private IChannel _channel { get; set; }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void SetChannel(IChannel channel)
        {
            _channel = channel;
        }

        public void OnChannelActive(IChannelHandlerContext ctx)
        {
            _event.OnStarted?.Invoke(this);
        }

        public void OnChannelInactive(IChannel channel)
        {
            _event.OnClose?.Invoke(this);
        }

        public void OnChannelReceive(IChannelHandlerContext ctx, object msg)
        {
            DatagramPacket packet = msg as DatagramPacket;
            if (!packet.Content.IsReadable())
            {
                return;
            }
            else
            {
                var bytes = packet.Content.ToArray();
                _event.OnRecieve?.Invoke(this, packet.Sender, bytes);
            }
        }

        public void OnException(IChannel channel, Exception exception)
        {
            _event.OnException?.Invoke(exception);
        }

        public async Task Send(byte[] bytes, EndPoint point)
        {
            IByteBuffer buffer = Unpooled.WrappedBuffer(bytes);
            await _channel.WriteAndFlushAsync(new DatagramPacket(buffer, point));
            await Task.Run(() =>
            {
                _event.OnSend?.Invoke(this, point, bytes);
            });
        }

        public async Task Send(string msgStr, EndPoint point)
        {
            await Send(Encoding.UTF8.GetBytes(msgStr), point);
        }
    }
}
