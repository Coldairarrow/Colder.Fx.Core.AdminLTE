using System;
using System.Net.Sockets;
using System.Text;

namespace Coldairarrow.Util.Sockets
{
    /// <summary>
    /// Socket连接,双向通信
    /// </summary>
    public class TcpSocketConnection
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="socket">维护的Socket对象</param>
        /// <param name="server">维护此连接的服务对象</param>
        /// <param name="recLength">接受缓冲区大小</param>
        public TcpSocketConnection(Socket socket, TcpSocketServer server, int recLength)
        {
            _socket = socket;
            _server = server;
            _recLength = recLength;
        }

        #endregion

        #region 私有成员

        private Socket _socket { get; }
        private bool _isRec { get; set; } = true;
        private TcpSocketServer _server { get; set; } = null;
        private bool _isClosed { get; set; } = false;
        private string _connectionId { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 接收区大小,单位:字节
        /// </summary>
        private int _recLength { get; set; }

        #endregion

        #region 外部接口


        /// <summary>
        /// 开始接受客户端消息
        /// </summary>
        public void StartRecMsg()
        {
            try
            {
                byte[] container = new byte[_recLength];
                _socket.BeginReceive(container, 0, container.Length, SocketFlags.None, asyncResult =>
                {
                    try
                    {
                        int length = _socket.EndReceive(asyncResult);

                        //马上进行下一轮接受，增加吞吐量
                        if (length > 0 && _isRec && IsSocketConnected())
                            StartRecMsg();

                        if (length > 0)
                        {
                            byte[] recBytes = new byte[length];
                            Array.Copy(container, 0, recBytes, 0, length);
                            HandleRecMsg?.Invoke(_server, this, recBytes);
                        }
                        else
                            Close();
                    }
                    catch (ObjectDisposedException)
                    {

                    }
                    catch (Exception ex)
                    {
                        HandleException?.Invoke(ex);
                        Close();
                    }
                }, null);
            }
            catch (Exception ex)
            {
                HandleException?.Invoke(ex);
                Close();
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes">数据字节</param>
        public void Send(byte[] bytes)
        {
            try
            {
                _socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, asyncResult =>
                {
                    try
                    {
                        int length = _socket.EndSend(asyncResult);
                        HandleSendMsg?.Invoke(_server, this, bytes);
                    }
                    catch (Exception ex)
                    {
                        HandleException?.Invoke(ex);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                HandleException?.Invoke(ex);
            }
        }

        /// <summary>
        /// 发送字符串（默认使用UTF-8编码）
        /// </summary>
        /// <param name="msgStr">字符串</param>
        public void Send(string msgStr)
        {
            Send(Encoding.UTF8.GetBytes(msgStr));
        }

        /// <summary>
        /// 发送字符串（使用自定义编码）
        /// </summary>
        /// <param name="msgStr">字符串消息</param>
        /// <param name="encoding">使用的编码</param>
        public void Send(string msgStr, Encoding encoding)
        {
            Send(encoding.GetBytes(msgStr));
        }

        /// <summary>
        /// 连接标识Id
        /// 注:用于标识与客户端的连接
        /// </summary>
        public string ConnectionId
        {
            get
            {
                return _connectionId;
            }
            set
            {
                string oldConnectionId = _connectionId;
                _connectionId = value;
                _server?.SetConnectionId(this, oldConnectionId, value);
            }
        }

        /// <summary>
        /// 关闭当前连接
        /// </summary>
        public void Close()
        {
            if (_isClosed)
                return;
            try
            {
                _isClosed = true;
                _server.RemoveConnection(this);

                _isRec = false;
                _socket.BeginDisconnect(false, (asyncCallback) =>
                {
                    try
                    {
                        _socket.EndDisconnect(asyncCallback);
                    }
                    catch (Exception ex)
                    {
                        HandleException?.Invoke(ex);
                    }
                    finally
                    {
                        _socket.Dispose();
                    }
                }, null);
            }
            catch (Exception ex)
            {
                HandleException?.Invoke(ex);
            }
            finally
            {
                try
                {
                    HandleClientClose?.Invoke(_server, this);
                }
                catch (Exception ex)
                {
                    HandleException?.Invoke(ex);
                }
            }
        }

        /// <summary>
        /// 判断是否处于已连接状态
        /// </summary>
        /// <returns></returns>
        public bool IsSocketConnected()
        {
            return !((_socket.Poll(1000, SelectMode.SelectRead) && (_socket.Available == 0)) || !_socket.Connected);
        }

        #endregion

        #region 事件处理

        /// <summary>
        /// 客户端连接接受新的消息后调用
        /// </summary>
        public Action<TcpSocketServer, TcpSocketConnection, byte[]> HandleRecMsg { get; set; }

        /// <summary>
        /// 客户端连接发送消息后回调
        /// </summary>
        public Action<TcpSocketServer, TcpSocketConnection, byte[]> HandleSendMsg { get; set; }

        /// <summary>
        /// 客户端连接关闭后回调
        /// </summary>
        public Action<TcpSocketServer, TcpSocketConnection> HandleClientClose { get; set; }

        /// <summary>
        /// 异常处理程序
        /// </summary>
        public Action<Exception> HandleException { get; set; }

        #endregion
    }
}
