using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using System.Linq;

namespace Coldairarrow.Business.Cache
{
    public class Base_UserDTOCache : BaseCache<Base_UserDTO>, IBase_UserDTOCache, ICircleDependency
    {
        public IBase_UserBusiness SysUserBus { get; set; }
        protected override string _moduleKey => "Base_UserDTO";
        protected override Base_UserDTO GetDbData(string key)
        {
            return SysUserBus.GetDataList(new Pagination(), true, key).FirstOrDefault();
        }
    }
}
