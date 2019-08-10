namespace Coldairarrow.Util.DotNettySockets
{
    public interface ITcpSocketServerBuilder :
        IGenericServerBuilder<ITcpSocketServerBuilder, ITcpSocketServer, ITcpSocketConnection, byte[]>,
        ICoderBuilder<ITcpSocketServerBuilder>
    {

    }
}
