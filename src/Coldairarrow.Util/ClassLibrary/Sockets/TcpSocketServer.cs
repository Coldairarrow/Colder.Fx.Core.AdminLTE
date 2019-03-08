using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Coldairarrow.Util.Sockets
{
    /// <summary>
    /// TcpSocket服务端
    /// </summary>
    public class TcpSocketServer
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ip">监听的IP地址</param>
        /// <param name="port">监听的端口</param>
        public TcpSocketServer(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        /// <summary>
        /// 构造函数,监听IP地址默认为本机0.0.0.0
        /// </summary>
        /// <param name="port">监听的端口</param>
        public TcpSocketServer(int port)
        {
            _ip = "0.0.0.0";
            _port = port;
        }

        #endregion

        #region 内部成员

        private Socket _socket { get; set; } = null;
        private string _ip { get; set; } = "";
        private int _port { get; set; } = 0;
        private bool _isListen { get; set; } = true;
        private void StartListen()
        {
            try
            {
                _socket.BeginAccept(asyncResult =>
                {
                    try
                    {
                        Socket newSocket = _socket.EndAccept(asyncResult);

                        //马上进行下一轮监听,增加吞吐量
                        if (_isListen)
                            StartListen();

                        TcpSocketConnection newConnection = new TcpSocketConnection(newSocket, this, RecLength)
                        {
                            HandleRecMsg = HandleRecMsg == null ? null : new Action<TcpSocketServer, TcpSocketConnection, byte[]>(HandleRecMsg),
                            HandleClientClose = HandleClientClose == null ? null : new Action<TcpSocketServer, TcpSocketConnection>(HandleClientClose),
                            HandleSendMsg = HandleSendMsg == null ? null : new Action<TcpSocketServer, TcpSocketConnection, byte[]>(HandleSendMsg),
                            HandleException = HandleException == null ? null : new Action<Exception>(HandleException)
                        };
                        AddConnection(newConnection);
                        newConnection.StartRecMsg();
                        HandleNewClientConnected?.Invoke(this, newConnection);
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
        private bool PortInUse(int port)
        {
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            return ipProperties.GetActiveTcpListeners().ToList().Exists(x => x.Port == port);
        }
        private ConcurrentDictionary<string, TcpSocketConnection> _connectionList { get; } = new ConcurrentDictionary<string, TcpSocketConnection>();

        #endregion

        #region 外部接口

        /// <summary>
        /// 接收区大小,单位:字节
        /// </summary>
        public int RecLength { get; set; } = 1024;

        /// <summary>
        /// 开始服务，监听客户端
        /// </summary>
        public void StartServer()
        {
            try
            {
                //实例化套接字（ip4寻址协议，流式传输，TCP协议）
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //创建ip对象
                IPAddress address = IPAddress.Parse(_ip);
                //创建网络节点对象包含ip和port
                IPEndPoint endpoint = new IPEndPoint(address, _port);
                //将 监听套接字绑定到 对应的IP和端口
                _socket.Bind(endpoint);
                //设置监听队列长度为Int32最大值(同时能够处理连接请求数量)
                _socket.Listen(int.MaxValue);
                //开始监听客户端
                StartListen();
                HandleServerStarted?.Invoke(this);
            }
            catch (Exception ex)
            {
                HandleException?.Invoke(ex);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void StopServer()
        {
            _isListen = false;
            _socket.Close();
            _socket.Dispose();
            GetAllConnections().ForEach(aCon =>
            {
                aCon.Close();
            });
        }

        /// <summary>
        /// 设置连接标识Id
        /// </summary>
        /// <param name="tcpSocketConnection">需要设置的连接对象</param>
        /// <param name="oldConnectionId">老的标识</param>
        /// <param name="newConnectionId">新的标识</param>
        public void SetConnectionId(TcpSocketConnection tcpSocketConnection, string oldConnectionId, string newConnectionId)
        {
            if (!string.IsNullOrEmpty(oldConnectionId))
                _connectionList.TryRemove(oldConnectionId, out TcpSocketConnection theValue);

            _connectionList[newConnectionId] = tcpSocketConnection;
        }

        /// <summary>
        /// 关闭指定客户端连接
        /// </summary>
        /// <param name="theConnection">指定的客户端连接</param>
        public void CloseConnection(TcpSocketConnection theConnection)
        {
            theConnection.Close();
        }

        /// <summary>
        /// 添加客户端连接
        /// </summary>
        /// <param name="theConnection">需要添加的客户端连接</param>
        private void AddConnection(TcpSocketConnection theConnection)
        {
            _connectionList[theConnection.ConnectionId] = theConnection;
        }

        /// <summary>
        /// 删除指定的客户端连接
        /// </summary>
        /// <param name="theConnection">指定的客户端连接</param>
        public void RemoveConnection(TcpSocketConnection theConnection)
        {
            if (!string.IsNullOrEmpty(theConnection.ConnectionId))
            {
                var theCon = _connectionList[theConnection.ConnectionId];
                if (theCon == theConnection)
                    _connectionList.TryRemove(theConnection.ConnectionId, out TcpSocketConnection theOutValue);
            }
        }

        /// <summary>
        /// 获取客户端连接
        /// </summary>
        /// <param name="connectionId">连接标志Id</param>
        /// <returns></returns>
        public TcpSocketConnection GetConnection(string connectionId)
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
        public List<TcpSocketConnection> GetAllConnections()
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
        public Action<Exception> HandleException
        {
            get
            {
                return _handleException;
            }
            set
            {
                _handleException = x =>
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            value?.Invoke(x);
                        }
                        catch
                        {

                        }
                    });
                };
            }
        }
        private Action<Exception> _handleException;

        #endregion

        #region 服务端事件

        /// <summary>
        /// 服务启动后执行
        /// </summary>
        public Action<TcpSocketServer> HandleServerStarted { get; set; }

        /// <summary>
        /// 当新客户端连接后执行
        /// </summary>
        public Action<TcpSocketServer, TcpSocketConnection> HandleNewClientConnected { get; set; }

        #endregion

        #region 客户端连接事件

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

        #endregion
    }
}
