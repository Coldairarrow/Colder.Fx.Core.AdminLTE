using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business
{
    class RDBMSLogger : ILogger
    {
        private IRepository _db { get; } = DbFactory.GetRepository();
        public List<Base_SysLog> GetLogList(string logContent, string logType, string opUserName, DateTime? startTime, DateTime? endTime, Pagination pagination)
        {
            var whereExp = LinqHelper.True<Base_SysLog>();
            if (!logContent.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogContent.Contains(logContent));
            if (!logType.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.LogType == logType);
            if (!opUserName.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpUserName.Contains(opUserName));
            if (!startTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpTime >= startTime);
            if (!endTime.IsNullOrEmpty())
                whereExp = whereExp.And(x => x.OpTime <= endTime);

            return _db.GetIQueryable<Base_SysLog>().Where(whereExp).GetPagination(pagination).ToList();
        }

        public void WriteSysLog(Base_SysLog log)
        {
            _db.Insert(log);
        }
    }
}
