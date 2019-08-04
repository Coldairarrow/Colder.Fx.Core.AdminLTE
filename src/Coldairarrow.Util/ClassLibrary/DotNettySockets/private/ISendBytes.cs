using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface ISendBytes
    {
        Task Send(byte[] bytes);
    }
}