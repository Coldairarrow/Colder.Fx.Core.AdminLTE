using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IBase_DepartmentBusiness
    {
        List<Base_Department> GetDataList(Pagination pagination, string departmentName = null);
        Base_Department GetTheData(string id);
        List<string> GetChildrenIds(string departmentId);
        void AddData(Base_Department newData);
        void UpdateData(Base_Department theData);
        void DeleteData(List<string> ids);
    }
}