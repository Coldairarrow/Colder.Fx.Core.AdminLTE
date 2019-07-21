using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    /// <summary>
    /// ϵͳ���û���
    /// </summary>
    [Table("Base_User")]
    public class Base_User
    {

        /// <summary>
        /// �û�Id
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// �û���
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// ��ʵ����
        /// </summary>
        public String RealName { get; set; }

        /// <summary>
        /// �Ա�(1Ϊ�У�0ΪŮ)
        /// </summary>
        public Int32? Sex { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// ��������Id
        /// </summary>
        public string DepartmentId { get; set; }
    }
}