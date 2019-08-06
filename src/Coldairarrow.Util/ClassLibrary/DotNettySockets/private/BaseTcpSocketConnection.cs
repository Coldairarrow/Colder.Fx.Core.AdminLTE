using DotNetty.Transport.Channels;
using System;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseTcpSocketConnection<TTcpSocketServer, TConnection, TTcpSocketServerEvent, TData>
        : IBaseSocketConnection
        where TConnection : class, IBaseSocketConnection
        where TTcpSocketServer : IBaseTcpSocketServer<TConnection>
    {
        #region 构造函数

        public BaseTcpSocketConnection(
            TTcpSocketServer server,
            IChannel channel,
            IBaseTcpSocketServerEvent<TTcpSocketServer, TConnection, TData> serverEvent)
        {
            _server = server;
            _channel = channel;
            _serverEvent = serverEvent;
        }

        #endregion

        #region 私有成员

        protected TTcpSocketServer _server { get; }
        protected IChannel _channel { get; }
        protected IBaseTcpSocketServerEvent<TTcpSocketServer, TConnection, TData> _serverEvent { get; }
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
                _server.SetConnectionName(this as TConnection, oldName, newName);
                _connectionName = newName;
            }
        }

        public void Close()
        {
            _channel.CloseAsync();
            _server.RemoveConnection(this as TConnection);
        }

        #endregion
    }
}
