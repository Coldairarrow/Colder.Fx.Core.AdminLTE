using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Coldairarrow.Util.Sockets
{
    /// <summary>
    /// UdpSocket客户端
    /// </summary>
    public class UdpSocketClient
    {
        #region 构造函数

        /// <summary>
        /// 构造函数，指定端口号
        /// </summary>
        /// <param name="port">监听的端口</param>
        public UdpSocketClient(int port)
        {
            _port = port;
        }

        /// <summary>
        /// 构造函数，自动分配端口号
        /// </summary>
        public UdpSocketClient()
        {
            _udpClient = new UdpClient(0);
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

                        HandleRecMsg?.BeginInvoke(this, iPEndPoint, bytes, null, null);
                    }
                    catch (Exception ex)
                    {
                        HandleException?.BeginInvoke(ex, null, null);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                HandleException?.BeginInvoke(ex, null, null);
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
            HandleStarted?.BeginInvoke(null, null);
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

                        HandleSendMsg?.BeginInvoke(this, iPEndPoint, bytes, null, null);
                    }
                    catch (Exception ex)
                    {
                        HandleException?.BeginInvoke(ex, null, null);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                HandleException?.BeginInvoke(ex, null, null);
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
        /// 处理异常
        /// </summary>
        public Action<Exception> HandleException { get; set; }

        /// <summary>
        /// 处理收到的消息
        /// </summary>
        public Action<UdpSocketClient, IPEndPoint, byte[]> HandleRecMsg { get; set; }

        /// <summary>
        /// 处理发送的消息
        /// </summary>
        public Action<UdpSocketClient, IPEndPoint, byte[]> HandleSendMsg { get; set; }

        /// <summary>
        /// 服务启动后调用
        /// </summary>
        public Action HandleStarted { get; set; }

        #endregion
    }
}
