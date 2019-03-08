using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Util.Sockets
{
    /// <summary>
    /// UdpSocket服务端
    /// </summary>
    public class UdpSocketServer
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="port">监听的端口</param>
        public UdpSocketServer(int port)
        {
            _port = port;
        }

        #endregion

        #region 私有成员

        private int _port { get; set; }
        private UdpClient _udpClient { get; set; }

        private void StartRecMsg()
        {
            try
            {
                _udpClient.BeginReceive(asyncCallback =>
                {
                    try
                    {
                        IPEndPoint iPEndPoint = null;
                        byte[] bytes = _udpClient.EndReceive(asyncCallback, ref iPEndPoint);
                        StartRecMsg();

                        HandleRecMsg?.Invoke(this, iPEndPoint, bytes);
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

        #endregion

        #region 外部接口

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            _udpClient = new UdpClient(_port);

            StartRecMsg();
            HandleStarted?.Invoke();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            _udpClient.Close();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes">数据</param>
        /// <param name="iPEndPoint">目标地址</param>
        public void Send(byte[] bytes, IPEndPoint iPEndPoint)
        {
            try
            {
                _udpClient.BeginSend(bytes, bytes.Length, iPEndPoint, asyncCallback =>
                {
                    try
                    {
                        int length = _udpClient.EndSend(asyncCallback);

                        HandleSendMsg?.Invoke(this, iPEndPoint, bytes);
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
        /// 发送数据
        /// </summary>
        /// <param name="msg">数据（默认UTF-8编码发送）</param>
        /// <param name="iPEndPoint">目标地址</param>
        public void Send(string msg, IPEndPoint iPEndPoint)
        {
            Send(Encoding.UTF8.GetBytes(msg), iPEndPoint);
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="msg">数据（指定的编码方式）</param>
        /// <param name="iPEndPoint">目标地址</param>
        /// <param name="encoding">指定编码</param>
        public void Send(string msg, IPEndPoint iPEndPoint, Encoding encoding)
        {
            Send(encoding.GetBytes(msg), iPEndPoint);
        }

        #endregion

        #region 事件处理

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

        /// <summary>
        /// 处理收到的消息
        /// </summary>
        public Action<UdpSocketServer, IPEndPoint, byte[]> HandleRecMsg { get; set; }

        /// <summary>
        /// 处理发送的消息
        /// </summary>
        public Action<UdpSocketServer, IPEndPoint, byte[]> HandleSendMsg { get; set; }

        /// <summary>
        /// 服务启动后调用
        /// </summary>
        public Action HandleStarted { get; set; }

        #endregion
    }
}
