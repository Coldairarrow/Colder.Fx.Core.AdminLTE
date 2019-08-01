using DotNetty.Transport.Channels;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpServer : ITcpServer
    {
        public TcpServer(int port)
        {
            Port = port;
        }

        #region 私有成员

        ConcurrentDictionary<string, ITcpConnection> _idMapConnections { get; }
            = new ConcurrentDictionary<string, ITcpConnection>();
        ConcurrentDictionary<string, string> _nameMapId { get; }
            = new ConcurrentDictionary<string, string>();
        private IChannel _channel { get; set; }

        #endregion

        #region 外部接口
        public ServerEventHandle EventHandle { get; } = new ServerEventHandle();
        public void SetChannel(IChannel channel)
        {
            _channel = channel;
        }

        public int Port { get; }

        public void AddConnection(ITcpConnection theConnection)
        {
            _idMapConnections[theConnection.ConnectionId] = theConnection;
            _nameMapId[theConnection.ConnectionName] = theConnection.ConnectionId;
        }

        public void CloseConnection(ITcpConnection theConnection)
        {
            theConnection.StopAsync();
        }

        public List<ITcpConnection> GetAllConnections()
        {
            return _idMapConnections.Values.ToList();
        }

        public ITcpConnection GetConnectionById(string connectionId)
        {
            return _idMapConnections[connectionId];
        }

        public ITcpConnection GetConnectionByName(string connectionName)
        {
            return _idMapConnections[_nameMapId[connectionName]];
        }

        public List<string> GetAllConnectionNames()
        {
            return _nameMapId.Keys.ToList();
        }

        public int GetConnectionCount()
        {
            return _idMapConnections.Count;
        }

        public void RemoveConnection(ITcpConnection theConnection)
        {
            _idMapConnections.TryRemove(theConnection.ConnectionId, out _);
            _nameMapId.TryRemove(theConnection.ConnectionName, out _);
        }

        public async Task StopAsync()
        {
            await _channel.CloseAsync();
        }

        public void SetConnectionName(ITcpConnection theConnection, string oldConnectionName, string newConnectionName)
        {
            _nameMapId.TryRemove(oldConnectionName, out _);
            _nameMapId[newConnectionName] = theConnection.ConnectionId;
        }

        #endregion
    }
}