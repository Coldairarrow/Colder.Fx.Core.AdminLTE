using System.Collections.Generic;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface ITcpServer : IStart, IStop
    {
        int Port { get; }
        void SetConnectionName(ITcpConnection theConnection, string oldConnectionName, string newConnectionName);
        void CloseConnection(ITcpConnection theConnection);
        void AddConnection(ITcpConnection theConnection);
        void RemoveConnection(ITcpConnection theConnection);
        ITcpConnection GetConnection(string connectionId);
        List<string> GetAllConnectionIds();
        List<ITcpConnection> GetAllConnections();
        int GetConnectionCount();
    }
}