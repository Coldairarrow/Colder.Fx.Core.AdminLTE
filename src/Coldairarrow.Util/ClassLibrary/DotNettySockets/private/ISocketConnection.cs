namespace Coldairarrow.Util.DotNettySockets
{
    public interface ISocketConnection : IClose
    {
        string ConnectionId { get; }
        string ConnectionName { get; set; }
    }
}
