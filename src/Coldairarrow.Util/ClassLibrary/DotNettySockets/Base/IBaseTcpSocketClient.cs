namespace Coldairarrow.Util.DotNettySockets
{
    public interface IBaseTcpSocketClient : IClose
    {
        string Ip { get; }
        int Port { get; }
    }
}