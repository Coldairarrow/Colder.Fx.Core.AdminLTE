namespace Coldairarrow.Util.DotNettySockets
{
    public interface ITcpSocketClient : IClose, ISendBytes, ISendString
    {
        string Ip { get; }
        int Port { get; }
    }
}