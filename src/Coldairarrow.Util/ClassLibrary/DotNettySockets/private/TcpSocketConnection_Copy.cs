//using DotNetty.Buffers;
//using DotNetty.Transport.Channels;
//using System;
//using System.Text;
//using System.Threading.Tasks;

//namespace Coldairarrow.Util.DotNettySockets
//{
//    class TcpSocketConnection : ITcpSocketConnection
//    {
//        #region 构造函数

//        public TcpSocketConnection(TcpSocketServer tcpServer, IChannel channel)
//        {
//            _tcpServer = tcpServer;
//            _channel = channel;
//        }

//        #endregion

//        #region 私有成员

//        private TcpSocketServer _tcpServer { get; }
//        private IChannel _channel { get; }
//        private string _connectionName { get; set; } = Guid.NewGuid().ToString();

//        #endregion

//        #region 外部接口

//        public string ConnectionId => _channel.Id.AsLongText();

//        public string ConnectionName
//        {
//            get
//            {
//                return _connectionName;
//            }
//            set
//            {
//                string oldName = _connectionName;
//                string newName = value;
//                _tcpServer.SetConnectionName(this, oldName, newName);
//                _connectionName = newName;
//            }
//        }

//        public async Task Send(byte[] bytes)
//        {
//            await _channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(bytes));
//            _tcpServer.EventHandle.OnSend(_tcpServer, this, bytes);
//        }

//        public async Task Send(string msgStr)
//        {
//            await Send(Encoding.UTF8.GetBytes(msgStr));
//        }

//        public void Close()
//        {
//            _tcpServer.RemoveConnection(this);

//            _channel.CloseAsync();
//        }

//        #endregion
//    }
//}
