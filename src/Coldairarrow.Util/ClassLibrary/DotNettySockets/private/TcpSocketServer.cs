using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpSocketServer : ITcpSocketServer, IChannelEvent
    {
        public TcpSocketServer(int port, ITcpServerEvent eventHandle)
        {
            Port = port;
            EventHandle = eventHandle;
        }

        #region 私有成员

        ConcurrentDictionary<string, TcpSocketConnection> _idMapConnections { get; }
            = new ConcurrentDictionary<string, TcpSocketConnection>();
        ConcurrentDictionary<string, string> _nameMapId { get; }
            = new ConcurrentDictionary<string, string>();
        private IChannel _serverChannel { get; set; }

        private void AddConnection(TcpSocketConnection theConnection)
        {
            _idMapConnections[theConnection.ConnectionId] = theConnection;
            _nameMapId[theConnection.ConnectionName] = theConnection.ConnectionId;
        }

        public void CloseConnection(TcpSocketConnection theConnection)
        {
            RemoveConnection(theConnection);
            theConnection.Close();
        }

        private TcpSocketConnection GetConnection(IChannel clientChannel)
        {
            return _idMapConnections[clientChannel.Id.AsLongText()];
        }

        private void PackException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                EventHandle.OnException?.Invoke(ex);
            }
        }

        #endregion

        #region 外部接口

        public ITcpServerEvent EventHandle { get; } = new EventContainer();

        public void OnChannelActive(IChannel clientChannel)
        {
            var theConnection = new TcpSocketConnection(this, clientChannel);
            AddConnection(theConnection);
            EventHandle.OnNewConnection?.Invoke(this, theConnection);
        }

        public void OnChannelReceive(IChannel clientChannel, object msg)
        {
            var bytes = (msg as IByteBuffer).ToArray();
            PackException(() =>
            {
                var theConnection = GetConnection(clientChannel);
                EventHandle.OnRecieve?.Invoke(this, theConnection, bytes);
            });
        }

        public void OnException(IChannel clientChannel, Exception ex)
        {
            PackException(() =>
            {
                var theConnection = GetConnection(clientChannel);
                CloseConnection(theConnection);
                EventHandle.OnException?.Invoke(ex);
            });
        }

        public void OnChannelInactive(IChannel clientChannel)
        {
            PackException(() =>
            {
                var theConnection = GetConnection(clientChannel);
                EventHandle.OnConnectionClose(this, theConnection);
            });
        }

        public void SetChannel(IChannel channel)
        {
            _serverChannel = channel;
        }

        public int Port { get; }

        public List<ITcpSocketConnection> GetAllConnections()
        {
            return _idMapConnections.Values.Select(x => x as ITcpSocketConnection).ToList();
        }

        public ITcpSocketConnection GetConnectionById(string connectionId)
        {
            return _idMapConnections[connectionId];
        }

        public ITcpSocketConnection GetConnectionByName(string connectionName)
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

        public void RemoveConnection(ITcpSocketConnection theConnection)
        {
            _idMapConnections.TryRemove(theConnection.ConnectionId, out _);
            _nameMapId.TryRemove(theConnection.ConnectionName, out _);
        }

        public void SetConnectionName(ITcpSocketConnection theConnection, string oldConnectionName, string newConnectionName)
        {
            _nameMapId.TryRemove(oldConnectionName, out _);
            _nameMapId[newConnectionName] = theConnection.ConnectionId;
        }

        public void Close()
        {
            _serverChannel.CloseAsync();
        }

        #endregion
    }
}