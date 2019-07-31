using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpServer : ITcpServer
    {
        #region 私有成员

        ConcurrentDictionary<string, ITcpConnection> _connections { get; }
            = new ConcurrentDictionary<string, ITcpConnection>();

        #endregion

        #region 外部接口

        public int Port => throw new NotImplementedException();

        public void AddConnection(ITcpConnection theConnection)
        {
            _connections[theConnection.ConnectionName] = theConnection;
        }

        public void CloseConnection(ITcpConnection theConnection)
        {
            theConnection.Stop();
        }

        public List<string> GetAllConnectionIds()
        {
            return _connections.Keys.ToList();
        }

        public List<ITcpConnection> GetAllConnections()
        {
            return _connections.Values.ToList();
        }

        public ITcpConnection GetConnection(string connectionId)
        {
            return _connections[connectionId];
        }

        public int GetConnectionCount()
        {
            return _connections.Count;
        }

        public void RemoveConnection(ITcpConnection theConnection)
        {
            _connections.TryRemove(theConnection.ConnectionName, out _);
        }

        public void SetConnectionName(ITcpConnection theConnection, string oldConnectionId, string newConnectionId)
        {
            _connections.TryRemove(oldConnectionId, out _);
            _connections[newConnectionId] = theConnection;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}