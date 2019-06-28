using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using NLog;
using NLog.Targets;

namespace Coldairarrow.Business
{
    public class BaseTarget : TargetWithLayout
    {
        public BaseTarget()
        {
            Name = "系统日志";
            Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss}|${level}|日志类型:${event-properties:item=LogType}|操作员:${event-properties:item=OpUserName}|内容:${message}|备份数据:${event-properties:item=Data} ";
        }

        protected Base_SysLog GetBase_SysLogInfo(LogEventInfo logEventInfo)
        {
            Base_SysLog newLog = new Base_SysLog
            {
                Id = IdHelper.GetId(),
                Data = logEventInfo.Properties["Data"] as string,
                Level = logEventInfo.Level.ToString(),
                LogContent = logEventInfo.Message,
                LogType = logEventInfo.Properties["LogType"] as string,
                OpTime = logEventInfo.TimeStamp,
                OpUserName = logEventInfo.Properties["OpUserName"] as string
            };

            return newLog;
        }
    }
}
