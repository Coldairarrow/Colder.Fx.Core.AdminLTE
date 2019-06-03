using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IBase_DatabaseLinkBusiness
    {
        List<Base_DatabaseLink> GetDataList(Pagination pagination);
        Base_DatabaseLink GetTheData(string id);
        void AddData(Base_DatabaseLink newData);
        void UpdateData(Base_DatabaseLink theData);
        void DeleteData(List<string> ids);
    }
}