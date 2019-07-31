namespace Coldairarrow.Util.DotNettySockets
{
    public interface ITcpConnection : IStop, ISend
    {
        string ConnectionId { get; }
        string ConnectionName { get; set; }
    }
}