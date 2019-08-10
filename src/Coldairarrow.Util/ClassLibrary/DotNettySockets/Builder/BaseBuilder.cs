using System;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseBuilder<TBuilder, TTarget> : IBuilder<TBuilder, TTarget> where TBuilder : class
    {
        public BaseBuilder(int port)
        {
            _port = port;
        }
        protected int _port { get; }

        public abstract Task<TTarget> BuildAsync();

        public abstract TBuilder OnException(Action<Exception> action);
    }
}
