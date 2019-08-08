using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Util.DotNettySockets
{
    abstract class BaseBuilder<TBuilder> : IBaseBuilder<TBuilder>
    {
        protected int _port { get; }

        private List<IChannelHandler> _pipeines { get; } = new List<IChannelHandler>();

        public abstract TBuilder BuildAsync();
         
        public TBuilder OnException(Action<Exception> action)
        {
            throw new NotImplementedException();
        }

        public TBuilder SetLengthFieldDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip)
        {
            throw new NotImplementedException();
        }

        public TBuilder SetLengthFieldEncoder(int lengthFieldLength)
        {
            throw new NotImplementedException();
        }
    }
}
