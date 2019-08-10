namespace Coldairarrow.Util.DotNettySockets
{
    public interface ITcpSocketClientBuilder :
        IGenericClientBuilder<ITcpSocketClientBuilder, ITcpSocketClient, byte[]>,
        ICoderBuilder<ITcpSocketClientBuilder>
    {

    }
}
