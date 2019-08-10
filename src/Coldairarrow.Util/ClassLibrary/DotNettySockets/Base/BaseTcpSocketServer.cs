using DotNetty.Transport.Channels;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseTcpSocketServer<TSocketServer, TConnection, TData> : IBaseTcpSocketServer<TConnection>, IChannelEvent
        where TConnection : class, IBaseSocketConnection
        where TSocketServer : class, IBaseTcpSocketServer<TConnection>
    {
        public BaseTcpSocketServer(int port, TcpSocketServerEvent<TSocketServer, TConnection, TData> eventHandle)
        {
            Port = port;
            _eventHandle = eventHandle;
        }

        #region 私有成员

        ConcurrentDictionary<string, TConnection> _idMapConnections { get; }
            = new ConcurrentDictionary<string, TConnection>();
        ConcurrentDictionary<string, string> _nameMapId { get; }
            = new ConcurrentDictionary<string, string>();
        protected IChannel _serverChannel { get; set; }
        protected void AddConnection(TConnection theConnection)
        {
            _idMapConnections[theConnection.ConnectionId] = theConnection;
            _nameMapId[theConnection.ConnectionName] = theConnection.ConnectionId;
        }
        protected TConnection GetConnection(IChannel clientChannel)
        {
            return _idMapConnections[clientChannel.Id.AsShortText()];
        }
        protected void PackException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _eventHandle.OnException?.Invoke(ex);
            }
        }
        protected TcpSocketServerEvent<TSocketServer, TConnection, TData> _eventHandle { get; }
        protected abstract TConnection BuildConnection(IChannel clientChannel);

        #endregion

        #region 外部接口

        public void OnChannelActive(IChannelHandlerContext ctx)
        {
            var theConnection = BuildConnection(ctx.Channel);
            AddConnection(theConnection);
            _eventHandle.OnNewConnection?.Invoke(this as TSocketServer, theConnection);
        }

        public void OnException(IChannel clientChannel, Exception ex)
        {
            PackException(() =>
            {
                var theConnection = GetConnection(clientChannel);
                CloseConnection(theConnection);
                _eventHandle.OnException?.Invoke(ex);
            });
        }

        public void OnChannelInactive(IChannel clientChannel)
        {
            PackException(() =>
            {
                var theConnection = GetConnection(clientChannel);
                _eventHandle.OnConnectionClose(this as TSocketServer, theConnection);
            });
        }

        public void SetChannel(IChannel channel)
        {
            _serverChannel = channel;
        }

        public int Port { get; }

        public List<TConnection> GetAllConnections()
        {
            return _idMapConnections.Values.Select(x => x as TConnection).ToList();
        }

        public TConnection GetConnectionById(string connectionId)
        {
            return _idMapConnections[connectionId];
        }

        public TConnection GetConnectionByName(string connectionName)
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

        public void RemoveConnection(TConnection theConnection)
        {
            _idMapConnections.TryRemove(theConnection.ConnectionId, out _);
            _nameMapId.TryRemove(theConnection.ConnectionName, out _);
        }

        public void SetConnectionName(TConnection theConnection, string oldConnectionName, string newConnectionName)
        {
            _nameMapId.TryRemove(oldConnectionName, out _);
            _nameMapId[newConnectionName] = theConnection.ConnectionId;
        }

        public void CloseConnection(TConnection theConnection)
        {
            RemoveConnection(theConnection);
            theConnection.Close();
        }

        public void Close()
        {
            _serverChannel.CloseAsync();
        }

        public abstract void OnChannelReceive(IChannelHandlerContext ctx, object msg);

        #endregion
    }
}