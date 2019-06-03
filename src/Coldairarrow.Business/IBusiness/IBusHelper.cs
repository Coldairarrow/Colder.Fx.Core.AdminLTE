using Coldairarrow.Entity.Base_SysManage;
using System;

namespace Coldairarrow.Business
{
    public interface IBusHelper
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logType">日志类型</param>
        void WriteSysLog(string logContent, EnumType.LogType logType);

        /// <summary>
        /// 处理系统异常
        /// </summary>
        /// <param name="ex">异常对象</param>
        void HandleException(Exception ex);
    }
}