using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using System.Linq;

namespace Coldairarrow.Business.Cache
{
    class Base_SysRoleCache : BaseCache<Base_SysRole>
    {
        public Base_SysRoleCache()
            : base("UserRoleCache", roleId =>
            {
                return DbFactory.GetRepository().GetIQueryable<Base_SysRole>().Where(x => x.RoleId == roleId).FirstOrDefault();
            })
        {

        }
    }
}
