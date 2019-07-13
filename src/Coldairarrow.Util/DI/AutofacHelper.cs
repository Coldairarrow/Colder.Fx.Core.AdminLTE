using Microsoft.AspNetCore.Http;
using System;

namespace Coldairarrow.Util
{
    public class AutofacHelper
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static T GetService<T>()
        {
            return (T)ServiceProvider.GetService(typeof(T));
        }

        public static T GetScopeService<T>()
        {
            return (T)GetService<IHttpContextAccessor>().HttpContext.RequestServices.GetService(typeof(T));
        }
    }
}