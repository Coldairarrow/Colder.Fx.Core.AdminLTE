using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Transport.Channels;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class WebSocketConnection : BaseTcpSocketConnection<IWebSocketServer, IWebSocketConnection, string>, IWebSocketConnection
    {
        #region 构造函数

        public WebSocketConnection(IWebSocketServer server, IChannel channel, TcpSocketServerEvent<IWebSocketServer, IWebSocketConnection, string> serverEvent)
            : base(server, channel, serverEvent)
        {

        }

        #endregion

        #region 私有成员

        #endregion

        #region 外部接口

        public async Task Send(string msgStr)
        {
            await _channel.WriteAndFlushAsync(new TextWebSocketFrame(msgStr));
            await Task.Run(() =>
            {
                _serverEvent.OnSend?.Invoke(_server, this, msgStr);
            });
        }

        #endregion
    }
}
