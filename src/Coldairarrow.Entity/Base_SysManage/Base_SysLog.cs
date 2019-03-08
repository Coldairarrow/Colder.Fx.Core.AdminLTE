using Nest;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    [Table("Base_SysLog")]
    public class Base_SysLog
    {

        /// <summary>
        /// 代理主键
        /// </summary>
        [Key]
        [Keyword]
        public String Id { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [Keyword]
        public String LogType { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        [Keyword]
        public String LogContent { get; set; }

        /// <summary>
        /// 操作员用户名
        /// </summary>
        [Keyword]
        public String OpUserName { get; set; }

        /// <summary>
        /// 日志记录时间
        /// </summary>
        [Keyword]
        public DateTime? OpTime { get; set; }

    }
}