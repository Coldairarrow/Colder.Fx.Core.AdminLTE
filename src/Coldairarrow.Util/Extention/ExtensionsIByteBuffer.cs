using DotNetty.Buffers;
using DotNetty.Common.Utilities;

namespace Coldairarrow.Util
{
    public static partial class Extention
    {
        /// <summary>
        /// 获取IByteBuffer中的byte[]
        /// </summary>
        /// <param name="byteBuffer">IByteBuffer</param>
        /// <returns></returns>
        public static byte[] ToArray(this IByteBuffer byteBuffer)
        {
            int readableBytes = byteBuffer.ReadableBytes;
            if (readableBytes == 0)
            {
                return ArrayExtensions.ZeroBytes;
            }

            if (byteBuffer.HasArray)
            {
                return byteBuffer.Array.Slice(byteBuffer.ArrayOffset + byteBuffer.ReaderIndex, readableBytes);
            }

            var bytes = new byte[readableBytes];
            byteBuffer.GetBytes(byteBuffer.ReaderIndex, bytes);
            return bytes;
        }
    }
}
