using System;
using System.Text;

namespace Coldairarrow.Util.Sockets
{
    /// <summary>
    /// WebSocket连接
    /// </summary>
    public class WebSocketConnection
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="webSocketServer">WebSocket端</param>
        /// <param name="con">TcpSocketConnection连接对象</param>
        public WebSocketConnection(WebSocketServer webSocketServer, TcpSocketConnection con)
        {
            _webSocketServer = webSocketServer;
            _theCon = con;
        }

        #endregion

        #region 私有成员

        private WebSocketServer _webSocketServer { get; set; }
        private TcpSocketConnection _theCon { get; set; }
        private static byte[] PackData(string message)
        {
            byte[] contentBytes = null;
            byte[] temp = Encoding.UTF8.GetBytes(message);

            if (temp.Length < 126)
            {
                contentBytes = new byte[temp.Length + 2];
                contentBytes[0] = 0x81;
                contentBytes[1] = (byte)temp.Length;
                Array.Copy(temp, 0, contentBytes, 2, temp.Length);
            }
            else if (temp.Length < 0xFFFF)
            {
                contentBytes = new byte[temp.Length + 4];
                contentBytes[0] = 0x81;
                contentBytes[1] = 126;
                contentBytes[2] = (byte)(temp.Length & 0xFF);
                contentBytes[3] = (byte)(temp.Length >> 8 & 0xFF);
                Array.Copy(temp, 0, contentBytes, 4, temp.Length);
            }
            else
            {
                // 暂不处理超长内容
            }

            return contentBytes;
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 连接标识Id
        /// 注:用于标识与客户端的连接
        /// </summary>
        public string ConnectionId
        {
            get
            {
                return _theCon.ConnectionId;
            }
            set
            {
                _webSocketServer.SetConnectionId(this, _theCon.ConnectionId, value);
            }
        }

        /// <summary>
        /// 发送字符串（UTF-8编码）
        /// </summary>
        /// <param name="msgStr">字符串</param>
        public void Send(string msgStr)
        {
            _theCon.Send(PackData(msgStr));
            HandleSendMsg?.Invoke(_webSocketServer, this, msgStr);
        }

        /// <summary>
        /// 关闭当前连接
        /// </summary>
        public void Close()
        {
            _theCon.Close();
            _webSocketServer.RemoveConnection(this);
            HandleClientClose?.Invoke(_webSocketServer, this);
        }

        /// <summary>
        /// 判断是否处于已连接状态
        /// </summary>
        /// <returns></returns>
        public bool IsSocketConnected()
        {
            return _theCon.IsSocketConnected();
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 客户端连接发送消息后回调
        /// </summary>
        public Action<WebSocketServer, WebSocketConnection, string> HandleSendMsg { get; set; }

        /// <summary>
        /// 客户端连接关闭后回调
        /// </summary>
        public Action<WebSocketServer, WebSocketConnection> HandleClientClose { get; set; }

        #endregion
    }
}
