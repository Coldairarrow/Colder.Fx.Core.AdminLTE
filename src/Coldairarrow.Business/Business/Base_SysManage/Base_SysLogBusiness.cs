using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    public class Base_SysLogBusiness : BaseBusiness<Base_SysLog>, IBase_SysLogBusiness, IDependency
    {
        #region 外部接口

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logType">日志类型</param>
        /// <param name="opUserName">操作人用户名</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public List<Base_SysLog> GetLogList(
            Pagination pagination,
            string logContent,
            string logType,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime)
        {
            return LoggerFactory.GetLogger().GetLogList(pagination, logContent, logType, opUserName, startTime, endTime);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}