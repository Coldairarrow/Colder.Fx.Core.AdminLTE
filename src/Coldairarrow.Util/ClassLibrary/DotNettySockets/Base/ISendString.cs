using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface ISendString
    {
        Task Send(string msgStr);
    }
}