using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IBase_AppSecretBusiness
    {
        List<Base_AppSecret> GetDataList(Pagination pagination, string keyword);
        Base_AppSecret GetTheData(string id);
        void AddData(Base_AppSecret newData);
        void UpdateData(Base_AppSecret theData);
        void DeleteData(List<string> ids);
        void SavePermission(string appId, List<string> permissions);
    }
}