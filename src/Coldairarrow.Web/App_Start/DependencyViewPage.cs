using Coldairarrow.Business;
using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Microsoft.AspNetCore.Mvc
{
    public abstract class DependencyViewPage : RazorPage<dynamic>
    {
        public IPermissionManage PermissionManage { get => AutofacHelper.GetService<IPermissionManage>(); }
        public ISystemMenuManage SystemMenuManage { get => AutofacHelper.GetService<ISystemMenuManage>(); }
        public IBase_UserBusiness SysUserBus { get => AutofacHelper.GetService<IBase_UserBusiness>(); }
        public IOperator Operator { get => AutofacHelper.GetService<IOperator>(); }
    }
}