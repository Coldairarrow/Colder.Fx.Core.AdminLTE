using System;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface IBuilder<TBuilder, TTarget>
    {
        TBuilder OnException(Action<Exception> action);

        Task<TTarget> BuildAsync();
    }
}
