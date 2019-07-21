using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    /// <summary>
    /// ���ű�
    /// </summary>
    [Table("Base_Department")]
    public class Base_Department
    {

        /// <summary>
        /// ��Ȼ����
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// �ϼ�����Id
        /// </summary>
        public String ParentId { get; set; }

    }
}