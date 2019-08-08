using System;

namespace Coldairarrow.Util.DotNettySockets
{
    public interface IBaseBuilder<TBuilder>
    {
        /// <summary>
        /// 设置基于长度的解码器,解决粘包与分包问题
        /// </summary>
        /// <param name="maxFrameLength">最大长度</param>
        /// <param name="lengthFieldOffset">长度字段偏移量</param>
        /// <param name="lengthFieldLength">长度字段占字节数</param>
        /// <param name="lengthAdjustment">添加到长度字段的补偿值</param>
        /// <param name="initialBytesToStrip">从解码帧中开始去除的字节数</param>
        /// <returns></returns>
        TBuilder SetLengthFieldDecoder(int maxFrameLength, int lengthFieldOffset, int lengthFieldLength, int lengthAdjustment, int initialBytesToStrip);

        /// <summary>
        /// 设置基于长度的编码器,解决粘包与分包问题
        /// </summary>
        /// <param name="lengthFieldLength">长度字段占字节数</param>
        /// <returns></returns>
        TBuilder SetLengthFieldEncoder(int lengthFieldLength);

        TBuilder BuildAsync();

        TBuilder OnException(Action<Exception> action);
    }
}
