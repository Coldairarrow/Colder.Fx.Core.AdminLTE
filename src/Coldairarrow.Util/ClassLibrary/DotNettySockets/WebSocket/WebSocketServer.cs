using DotNetty.Buffers;
using DotNetty.Codecs.Http;
using DotNetty.Codecs.Http.WebSockets;
using DotNetty.Common.Utilities;
using DotNetty.Transport.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util.DotNettySockets
{
    class WebSocketServer : BaseTcpSocketServer<IWebSocketServer, IWebSocketConnection, string>, IWebSocketServer
    {
        public WebSocketServer(int port, string path, TcpSocketServerEvent<IWebSocketServer, IWebSocketConnection, string> eventHandle)
            : base(port, eventHandle)
        {
            _path = path;
        }

        protected string _path { get; }
        protected override IWebSocketConnection BuildConnection(IChannel clientChannel)
        {
            return new WebSocketConnection(this, clientChannel, _eventHandle);
        }

        WebSocketServerHandshaker handshaker;

        public override void OnChannelReceive(IChannelHandlerContext ctx, object msg)
        {
            if (msg is IFullHttpRequest request)
            {
                this.HandleHttpRequest(ctx, request);
            }
            else if (msg is WebSocketFrame frame)
            {
                this.HandleWebSocketFrame(ctx, frame);
            }
        }

        void HandleHttpRequest(IChannelHandlerContext ctx, IFullHttpRequest req)
        {
            // Handle a bad request.
            if (!req.Result.IsSuccess)
            {
                SendHttpResponse(ctx, req, new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.BadRequest));
                return;
            }

            // Allow only GET methods.
            if (req.Method.ToString().ToUpper() != "GET")
            {
                SendHttpResponse(ctx, req, new DefaultFullHttpResponse(HttpVersion.Http11, HttpResponseStatus.Forbidden));
                return;
            }

            //Wesocket握手协议
            if (req.Uri == _path)
            {
                var wsFactory = new WebSocketServerHandshakerFactory(
                    GetWebSocketLocation(req), null, true, 5 * 1024 * 1024);
                this.handshaker = wsFactory.NewHandshaker(req);
                if (this.handshaker == null)
                {
                    WebSocketServerHandshakerFactory.SendUnsupportedVersionResponse(ctx.Channel);
                }
                else
                {
                    this.handshaker.HandshakeAsync(ctx.Channel, req);
                }
            }
        }

        void HandleWebSocketFrame(IChannelHandlerContext ctx, WebSocketFrame frame)
        {
            // Check for closing frame
            if (frame is CloseWebSocketFrame)
            {
                this.handshaker.CloseAsync(ctx.Channel, (CloseWebSocketFrame)frame.Retain());
                return;
            }

            if (frame is PingWebSocketFrame)
            {
                ctx.WriteAsync(new PongWebSocketFrame((IByteBuffer)frame.Content.Retain()));
                return;
            }

            if (frame is TextWebSocketFrame textIframe)
            {
                PackException(() =>
                {
                    string msg = textIframe.Text();
                    var theConnection = GetConnection(ctx.Channel);
                    _eventHandle.OnRecieve?.Invoke(this, theConnection, msg);
                });

                return;
            }

            if (frame is BinaryWebSocketFrame)
            {
                // Echo the frame
                ctx.WriteAsync(frame.Retain());
            }
        }

        static void SendHttpResponse(IChannelHandlerContext ctx, IFullHttpRequest req, IFullHttpResponse res)
        {
            // Generate an error page if response getStatus code is not OK (200).
            if (res.Status.Code != 200)
            {
                IByteBuffer buf = Unpooled.CopiedBuffer(Encoding.UTF8.GetBytes(res.Status.ToString()));
                res.Content.WriteBytes(buf);
                buf.Release();
                HttpUtil.SetContentLength(res, res.Content.ReadableBytes);
            }

            // Send the response and close the connection if necessary.
            Task task = ctx.Channel.WriteAndFlushAsync(res);
            if (!HttpUtil.IsKeepAlive(req) || res.Status.Code != 200)
            {
                task.ContinueWith((t, c) => ((IChannelHandlerContext)c).CloseAsync(),
                    ctx, TaskContinuationOptions.ExecuteSynchronously);
            }
        }

        string GetWebSocketLocation(IFullHttpRequest req)
        {
            _ = req.Headers.TryGet(HttpHeaderNames.Host, out ICharSequence value);
            string location = value.ToString() + _path;
            return "ws://" + location;
        }
    }
}
