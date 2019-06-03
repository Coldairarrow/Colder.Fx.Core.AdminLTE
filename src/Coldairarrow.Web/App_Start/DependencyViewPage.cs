using Coldairarrow.Business;
using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Microsoft.AspNetCore.Mvc
{
    public abstract class DependencyViewPage : RazorPage<dynamic>
    {
        public IPermissionManage PermissionManage { get; set; } = AutofacHelper.GetService<IPermissionManage>();
        public ISystemMenuManage SystemMenuManage { get; set; } = AutofacHelper.GetService<ISystemMenuManage>();
        public IBase_UserBusiness SysUserBus { get; set; } = AutofacHelper.GetService<IBase_UserBusiness>();
        public IOperator Operator { get; set; } = AutofacHelper.GetService<IOperator>();
    }
}