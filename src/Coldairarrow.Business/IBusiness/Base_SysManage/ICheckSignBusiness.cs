using Microsoft.AspNetCore.Http;

namespace Coldairarrow.Business.Base_SysManage
{
    public interface ICheckSignBusiness
    {
        bool IsSecurity(HttpContext context);
        string GetAppSecret(string appId);
    }
}