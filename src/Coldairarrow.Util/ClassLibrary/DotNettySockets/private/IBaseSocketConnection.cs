namespace Coldairarrow.Util.DotNettySockets
{
    public interface IBaseSocketConnection : IClose
    {
        string ConnectionId { get; }
        string ConnectionName { get; set; }
    }
}
