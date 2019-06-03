using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IBase_SysLogBusiness
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
        List<Base_SysLog> GetLogList(
            Pagination pagination,
            string logContent,
            string logType,
            string opUserName,
            DateTime? startTime,
            DateTime? endTime
            );

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}