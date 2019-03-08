using Coldairarrow.Util;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface IHomebusiness
    {
        AjaxResult SubmitLogin(string userName, string password);
    }
}