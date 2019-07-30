using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System.Text;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpConnection : ITcpConnection
    {
        #region 构造函数

        public TcpConnection(ITcpServer tcpServer, IChannel channel)
        {
            _tcpServer = tcpServer;
            _channel = channel;
        }

        #endregion

        #region 私有成员

        private ITcpServer _tcpServer { get; }
        private IChannel _channel { get; }
        private string _connectionId { get; set; }

        #endregion

        #region 外部接口

        public string ConnectionId
        {
            get
            {
                return _connectionId;
            }
            set
            {
                _tcpServer.SetConnectionId(this, value);
            }
        }

        public void Send(byte[] bytes)
        {
            _channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(bytes));
        }

        public void Send(string msgStr)
        {
            Send(msgStr, Encoding.UTF8);
        }

        public void Send(string msgStr, Encoding encoding)
        {
            Send(encoding.GetBytes(msgStr));
        }

        public void Stop()
        {
            _channel.CloseAsync();
        }

        #endregion
    }
}
