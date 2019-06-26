using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Business
{
    public class BusHelper : IBusHelper, ICircleDependency
    {
        #region DI

        public IOperator Operator { private get; set; }

        #endregion

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logType">日志类型</param>
        public void WriteSysLog(string logContent, EnumType.LogType logType, string data = null)
        {
            string userName = null;
            try
            {
                userName = Operator.Property.RealName;
            }
            catch
            {

            }
            Base_SysLog newLog = new Base_SysLog
            {
                Id = IdHelper.GetId(),
                LogType = logType.ToString(),
                LogContent = logContent.Replace("\r\n", "<br />").Replace("  ", "&nbsp;&nbsp;"),
                OpTime = DateTime.Now,
                OpUserName = userName,
                Data = data
            };
            Task.Run(() =>
            {
                try
                {
                    LoggerFactory.GetLogger().WriteSysLog(newLog);
                }
                catch
                {

                }
            });
        }

        /// <summary>
        /// 处理系统异常
        /// </summary>
        /// <param name="ex">异常对象</param>
        public void HandleException(Exception ex)
        {
            string msg = ExceptionHelper.GetExceptionAllMsg(ex);
            msg += $@"
请求方法:{HttpContextCore.Current.Request.Method}
请求url:{HttpContextCore.Current.Request.GetDisplayUrl()}
请求body:{HttpContextCore.Current.Request.Body.ReadToString(Encoding.UTF8)}";

            WriteSysLog(msg, EnumType.LogType.系统异常);
        }
    }
}