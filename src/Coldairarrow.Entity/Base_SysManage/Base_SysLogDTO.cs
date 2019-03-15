using Nest;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_SysManage
{
    public class Base_SysLogDTO: Base_SysLog
    {
        public string RealName { get; set; }
    }
}