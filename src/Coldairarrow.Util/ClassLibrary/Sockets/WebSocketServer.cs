using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coldairarrow.Util.Sockets
{
    /// <summary>
    /// WebSocket服务端
    /// </summary>
    public class WebSocketServer
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// 注：默认监听0.0.0.0
        /// </summary>
        /// <param name="port">监听端口</param>
        /// <param name="recLength">数据接受区大小</param>
        public WebSocketServer(int port, int recLength = 1024)
        {
            _tcpSocketServer = new TcpSocketServer(port) { RecLength = recLength };
        }

        #endregion

        #region 私有成员

        private TcpSocketServer _tcpSocketServer { get; set; }
        private ConcurrentDictionary<string, WebSocketConnection> _connectionList { get; } = new ConcurrentDictionary<string, WebSocketConnection>();
        private static bool IsHandshake(string requestStr)
        {
            return requestStr.Contains("Upgrade") && requestStr.Contains("Sec-WebSocket-Key");
        }
        private static Dictionary<string, string> GetHeaders(string requestStr)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            var rows = requestStr.Split("\r\n".ToCharArray());
            rows.ForEach(aRow =>
            {
                if (aRow.Contains(":"))
                {
                    var keyValue = aRow.Split(':');
                    string key = keyValue[0];
                    string value = keyValue[1];
                    value = value.Remove(0, 1);

                    headers.Add(key, value);
                }
            });

            return headers;
        }
        private static string GetWebSocketAccept(string key)
        {
            return $"{key}258EAFA5-E914-47DA-95CA-C5AB0DC85B11".ToSHA1Bytes().ToBase64String();
        }
        private static string GetWebSocketResponse(string requestStr)
        {
            var headers = GetHeaders(requestStr);
            string webSocketKey = headers["Sec-WebSocket-Key"];
            string accept = GetWebSocketAccept(webSocketKey);
            string res = $@"HTTP/1.1 101 Switching Protocols
Upgrade: websocket
Connection: Upgrade
Sec-WebSocket-Accept: {accept}

";

            return res;
        }
        private static string AnalyticData(byte[] recBytes)
        {
            int recByteLength = recBytes.Length;
            if (recByteLength < 2)
            {
                return string.Empty;
            }

            bool fin = (recBytes[0] & 0x80) == 0x80; // 1bit，1表示最后一帧
            if (!fin)
            {
                return string.Empty;// 超过一帧暂不处理
            }

            bool mask_flag = (recBytes[1] & 0x80) == 0x80; // 是否包含掩码
            if (!mask_flag)
            {
                return string.Empty;// 不包含掩码的暂不处理
            }

            int payload_len = recBytes[1] & 0x7F; // 数据长度

            byte[] masks = new byte[4];
            byte[] payload_data;

            if (payload_len == 126)
            {
                Array.Copy(recBytes, 4, masks, 0, 4);
                payload_len = (UInt16)(recBytes[2] << 8 | recBytes[3]);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 8, payload_data, 0, payload_len);

            }
            else if (payload_len == 127)
            {
                Array.Copy(recBytes, 10, masks, 0, 4);
                byte[] uInt64Bytes = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    uInt64Bytes[i] = recBytes[9 - i];
                }
                UInt64 len = BitConverter.ToUInt64(uInt64Bytes, 0);

                payload_data = new byte[len];
                for (UInt64 i = 0; i < len; i++)
                {
                    payload_data[i] = recBytes[i + 14];
                }
            }
            else
            {
                Array.Copy(recBytes, 2, masks, 0, 4);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 6, payload_data, 0, payload_len);

            }

            for (var i = 0; i < payload_len; i++)
            {
                payload_data[i] = (byte)(payload_data[i] ^ masks[i % 4]);
            }
            return Encoding.UTF8.GetString(payload_data);
        }
        private void InitServer()
        {
            _tcpSocketServer.HandleServerStarted = theServer =>
            {
                HandleServerStarted?.Invoke(this);
            };

            _tcpSocketServer.HandleException = ex =>
            {
                HandleException?.Invoke(ex);
            };

            _tcpSocketServer.HandleClientClose = (theServer, theCon) =>
            {
                var webCon = GetConnection(theCon.ConnectionId);
                CloseConnection(webCon);
                HandleClientClose?.Invoke(this, webCon);
            };

            _tcpSocketServer.HandleNewClientConnected = (theServer, theCon) =>
            {
                WebSocketConnection newCon = new WebSocketConnection(this, theCon)
                {
                    HandleClientClose = HandleClientClose == null ? null : new Action<WebSocketServer, WebSocketConnection>(HandleClientClose),
                    HandleSendMsg = HandleSendMsg == null ? null : new Action<WebSocketServer, WebSocketConnection, string>(HandleSendMsg)
                };

                AddConnection(newCon);

                HandleNewClientConnected?.Invoke(this, newCon);
            };

            _tcpSocketServer.HandleRecMsg = (thServer, theCon, bytes) =>
            {
                string recStr = bytes.ToString(Encoding.UTF8);
                if (IsHandshake(recStr))
                {
                    string res = GetWebSocketResponse(recStr);

                    theCon.Send(res);
                }
                else
                {
                    int opcode = new string(bytes[0].ToBinString().Copy(4, 4).ToArray()).ToInt_FromBinString();

                    //为关闭连接
                    if (opcode == 8)
                    {
                        GetConnection(theCon.ConnectionId).Close();
                    }
                    else
                    {
                        string recData = AnalyticData(bytes);
                        HandleRecMsg?.Invoke(this, GetConnection(theCon.ConnectionId), recData);
                    }
                }
            };
        }
        private void AddConnection(WebSocketConnection theConnection)
        {
            _connectionList[theConnection.ConnectionId] = theConnection;
        }

        #endregion

        #region 外部接口

        /// <summary>
        /// 开始运行
        /// </summary>
        public void Start()
        {
            InitServer();
            _tcpSocketServer.StartServer();
        }

        /// <summary>
        /// 停止运行
        /// </summary>
        public void Stop()
        {
            _tcpSocketServer.StopServer();
        }

        /// <summary>
        /// 设置连接标识Id
        /// </summary>
        /// <param name="tcpSocketConnection">需要设置的连接对象</param>
        /// <param name="oldConnectionId">老的标识</param>
        /// <param name="newConnectionId">新的标识</param>
        public void SetConnectionId(WebSocketConnection tcpSocketConnection, string oldConnectionId, string newConnectionId)
        {
            if (!string.IsNullOrEmpty(oldConnectionId) && !string.IsNullOrEmpty(newConnectionId))
            {
                _connectionList.TryRemove(oldConnectionId, out WebSocketConnection theValue);
                _connectionList[newConnectionId] = tcpSocketConnection;

                _tcpSocketServer.GetConnection(oldConnectionId).ConnectionId = newConnectionId;
            }
        }

        /// <summary>
        /// 关闭指定客户端连接
        /// </summary>
        /// <param name="theConnection">指定的客户端连接</param>
        public void CloseConnection(WebSocketConnection theConnection)
        {
            theConnection.Close();
        }

        /// <summary>
        /// 删除指定的客户端连接
        /// </summary>
        /// <param name="theConnection">指定的客户端连接</param>
        public void RemoveConnection(WebSocketConnection theConnection)
        {
            if (!string.IsNullOrEmpty(theConnection.ConnectionId))
            {
                _connectionList.TryRemove(theConnection.ConnectionId, out WebSocketConnection theOutValue);
            }
        }

        /// <summary>
        /// 获取客户端连接
        /// </summary>
        /// <param name="connectionId">连接标志Id</param>
        /// <returns></returns>
        public WebSocketConnection GetConnection(string connectionId)
        {
            return _connectionList[connectionId];
        }

        /// <summary>
        /// 获取所有连接标志Id
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllConnectionIds()
        {
            return _connectionList.Keys.ToList();
        }

        /// <summary>
        /// 获取所有连接
        /// </summary>
        /// <returns></returns>
        public List<WebSocketConnection> GetAllConnections()
        {
            return _connectionList.Values.ToList();
        }

        /// <summary>
        /// 获取客户端连接数
        /// </summary>
        /// <returns></returns>
        public int GetConnectionCount()
        {
            return _connectionList.Keys.Count;
        }

        #endregion

        #region 公共事件

        /// <summary>
        /// 异常处理程序
        /// </summary>
        public Action<Exception> HandleException { get; set; }

        #endregion

        #region 服务端事件

        /// <summary>
        /// 服务启动后执行
        /// </summary>
        public Action<WebSocketServer> HandleServerStarted { get; set; }

        /// <summary>
        /// 当新客户端连接后执行
        /// </summary>
        public Action<WebSocketServer, WebSocketConnection> HandleNewClientConnected { get; set; }

        #endregion

        #region 客户端连接事件

        /// <summary>
        /// 客户端连接接受新的消息后调用
        /// </summary>
        public Action<WebSocketServer, WebSocketConnection, string> HandleRecMsg { get; set; }

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
