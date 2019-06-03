using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface ICheckSignBusiness
    {
        AjaxResult IsSecurity(HttpContext context);
        string GetAppSecret(string appId);
    }
}