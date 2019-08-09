using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseBuilder<TBuilder, TTarget> : IBaseBuilder<TBuilder, TTarget> where TBuilder : class
    {
        public BaseBuilder(int port)
        {
            _port = port;
        }
        protected int _port { get; }

        protected List<IChannelHandler> _pipeines { get; } = new List<IChannelHandler>();

        public abstract Task<TTarget> BuildAsync();

        public abstract TBuilder OnException(Action<Exception> action);

        public TBuilder SetLengthFieldDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip)
        {
            _pipeines.Add(new LengthFieldBasedFrameDecoder(maxFrameLength, lengthFieldOffset, lengthFieldLength, lengthAdjustment, initialBytesToStrip));

            return this as TBuilder;
        }

        public TBuilder SetLengthFieldEncoder(int lengthFieldLength)
        {
            _pipeines.Add(new LengthFieldPrepender(lengthFieldLength));

            return this as TBuilder;
        }
    }
}
