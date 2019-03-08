using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Business
{
    interface ILogger
    {
        void WriteSysLog(Base_SysLog log);
        List<Base_SysLog> GetLogList(
            string logContent,
            string logType,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime,
            Pagination pagination);
    }
}
