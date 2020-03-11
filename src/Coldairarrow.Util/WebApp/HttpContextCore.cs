using Microsoft.AspNetCore.Http;

namespace Coldairarrow.Util
{
    public static class HttpContextCore
    {
        public static IHttpContextAccessor Accessor;
        public static HttpContext Current { get => Accessor.HttpContext; }
    }
}
