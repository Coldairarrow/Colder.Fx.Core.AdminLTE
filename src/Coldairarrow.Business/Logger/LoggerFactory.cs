using Coldairarrow.Util;
using System;

namespace Coldairarrow.Business
{
    static class LoggerFactory
    {
        public static ILogger GetLogger()
        {
            return GetLogger(GlobalSwitch.LoggerType);
        }

        public static ILogger GetLogger(LoggerType LoggerType)
        {
            switch (LoggerType)
            {
                case LoggerType.RDBMS: return new RDBMSLogger();
                case LoggerType.ElasticSearch: return new ElasticSearchLogger();
                default: throw new Exception("请配置记录日志的类型");
            }
        }
    }
}
