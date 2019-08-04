using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface ISendString
    {
        Task Send(string msgStr);
        Task Send(string msgStr, Encoding encoding);
    }
}