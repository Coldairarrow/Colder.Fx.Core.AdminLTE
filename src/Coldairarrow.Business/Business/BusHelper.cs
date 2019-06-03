using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System;
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
        public void WriteSysLog(string logContent, EnumType.LogType logType)
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
                OpUserName = userName
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
            WriteSysLog(msg, EnumType.LogType.系统异常);
        }
    }
}