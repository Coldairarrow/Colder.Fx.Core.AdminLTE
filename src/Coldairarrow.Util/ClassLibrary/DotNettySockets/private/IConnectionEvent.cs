namespace Coldairarrow.Util.DotNettySockets
{
    interface IConnectionEvent<TData>
    {
        void OnSend(IBaseSocketConnection connection, TData data);
        void OnClose(IBaseSocketConnection connection);
        void OnSetConnectionName(IBaseSocketConnection connection, string oldConnectioName, string newConnectionName);
    }
}