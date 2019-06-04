using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ProjectManage
{
    /// <summary>
    /// 项目表
    /// </summary>
    [Table("Dev_Project")]
    public class Dev_Project
    {

        /// <summary>
        /// 自然主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public String ProjectId { get; set; }

        /// <summary>
        /// 项目名
        /// </summary>
        public String ProjectName { get; set; }

        /// <summary>
        /// 项目类型Id
        /// </summary>
        public String ProjectTypeId { get; set; }

        /// <summary>
        /// 项目经理Id
        /// </summary>
        public String ProjectManagerId { get; set; }

    }
}