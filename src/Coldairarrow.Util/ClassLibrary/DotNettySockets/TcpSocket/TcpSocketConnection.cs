using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpSocketConnection : BaseTcpSocketConnection<ITcpSocketServer, ITcpSocketConnection, byte[]>, ITcpSocketConnection
    {
        #region 构造函数

        public TcpSocketConnection(ITcpSocketServer server, IChannel channel, TcpSocketServerEvent<ITcpSocketServer, ITcpSocketConnection, byte[]> serverEvent)
            : base(server, channel, serverEvent)
        {

        }

        #endregion

        #region 私有成员

        #endregion

        #region 外部接口

        public async Task Send(byte[] bytes)
        {
            await _channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(bytes));
            await Task.Run(() =>
            {
                _serverEvent.OnSend?.Invoke(_server, this, bytes);
            });
        }

        public async Task Send(string msgStr)
        {
            await Send(Encoding.UTF8.GetBytes(msgStr));
        }

        #endregion
    }
}
