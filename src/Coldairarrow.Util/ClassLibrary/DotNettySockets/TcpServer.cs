using System;
using System.Collections.Generic;

namespace Coldairarrow.Util.DotNettySockets
{
    class TcpServer : ITcpServer
    {
        public int Port => throw new NotImplementedException();

        public void AddConnection(ITcpConnection theConnection)
        {
            throw new NotImplementedException();
        }

        public void CloseConnection(ITcpConnection theConnection)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllConnectionIds()
        {
            throw new NotImplementedException();
        }

        public List<ITcpConnection> GetAllConnections()
        {
            throw new NotImplementedException();
        }

        public ITcpConnection GetConnection(string connectionId)
        {
            throw new NotImplementedException();
        }

        public int GetConnectionCount()
        {
            throw new NotImplementedException();
        }

        public void RemoveConnection(ITcpConnection theConnection)
        {
            throw new NotImplementedException();
        }

        public void SetConnectionId(ITcpConnection theConnection, string newConnectionId)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}