using System.Collections.Generic;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface IBaseTcpSocketServer<SocketConnection> : IClose where SocketConnection : IBaseSocketConnection
    {
        int Port { get; }
        void SetConnectionName(SocketConnection theConnection, string oldConnectionName, string newConnectionName);
        SocketConnection GetConnectionById(string connectionId);
        SocketConnection GetConnectionByName(string connectionName);
        void RemoveConnection(SocketConnection theConnection);
        List<string> GetAllConnectionNames();
        List<SocketConnection> GetAllConnections();
        int GetConnectionCount();
    }
}