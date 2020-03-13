using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using static Coldairarrow.Entity.Base_SysManage.EnumType;

namespace Coldairarrow.Business
{
    /// <summary>
    /// 操作者
    /// </summary>
    public class Operator : IOperator, ICircleDependency
    {
        public IBase_UserBusiness _sysUserBus { get; set; }
        private const string USERID = "UserId";

        /// <summary>
        /// 当前操作者UserId
        /// </summary>
        public string UserId
        {
            get
            {
                if (GlobalSwitch.RunModel == RunModel.LocalTest)
                    return "Admin";
                else
                    return HttpContextCore.Current.Session.GetString(USERID);
            }
        }

        public Base_UserDTO Property { get => _sysUserBus.GetTheInfo(UserId); }

        #region 操作方法

        /// <summary>
        /// 是否已登录
        /// </summary>
        /// <returns></returns>
        public bool Logged()
        {
            return !UserId.IsNullOrEmpty();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userId">用户逻辑主键Id</param>
        public void Login(string userId)
        {
            HttpContextCore.Current.Session.SetString(USERID, userId);
        }

        /// <summary>
        /// 注销
        /// </summary>
        public void Logout()
        {
            HttpContextCore.Current.Session.Remove(USERID);
        }

        /// <summary>
        /// 判断是否为超级管理员
        /// </summary>
        /// <returns></returns>
        public bool IsAdmin()
        {
            var role = Property.RoleType;
            if (UserId == "Admin" || role.HasFlag(RoleType.超级管理员))
                return true;
            else
                return false;
        }

        #endregion
    }
}
