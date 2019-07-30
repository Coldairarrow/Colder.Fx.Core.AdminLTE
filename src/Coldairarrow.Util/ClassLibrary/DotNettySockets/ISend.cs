using System.Text;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface ISend
    {
        void Send(byte[] bytes);
        void Send(string msgStr);
        void Send(string msgStr, Encoding encoding);
    }
}
