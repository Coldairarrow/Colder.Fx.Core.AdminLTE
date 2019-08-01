using System.Collections.Generic;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface ITcpServer : IStop
    {
        int Port { get; }
        void SetConnectionName(ITcpConnection theConnection, string oldConnectionName, string newConnectionName);
        void CloseConnection(ITcpConnection theConnection);
        void AddConnection(ITcpConnection theConnection);
        void RemoveConnection(ITcpConnection theConnection);
        ITcpConnection GetConnectionById(string connectionId);
        ITcpConnection GetConnectionByName(string connectionName);
        List<string> GetAllConnectionNames();
        List<ITcpConnection> GetAllConnections();
        int GetConnectionCount();
    }
}