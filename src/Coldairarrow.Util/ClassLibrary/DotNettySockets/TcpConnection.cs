using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Text;
using System.Threading.Tasks;

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
        private string _connectionName { get; set; } = Guid.NewGuid().ToString();

        #endregion

        #region 外部接口

        public string ConnectionId => _channel.Id.AsLongText();

        public string ConnectionName
        {
            get
            {
                return _connectionName;
            }
            set
            {
                string oldName = _connectionName;
                string newName = value;
                _tcpServer.SetConnectionName(this, oldName, newName);
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

        public async Task StopAsync()
        {
            _tcpServer.RemoveConnection(this);

            await _channel.CloseAsync();
        }

        #endregion
    }
}
