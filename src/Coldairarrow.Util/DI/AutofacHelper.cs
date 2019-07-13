using Autofac;
using Microsoft.AspNetCore.Http;

namespace Coldairarrow.Util
{
    public class AutofacHelper
    {
        public static IContainer Container { get; set; }

        public static T GetService<T>()
        {
            return (T)Container.Resolve(typeof(T));
        }

        public static T GetScopeService<T>()
        {
            return (T)GetService<IHttpContextAccessor>().HttpContext.RequestServices.GetService(typeof(T));
        }
    }
}