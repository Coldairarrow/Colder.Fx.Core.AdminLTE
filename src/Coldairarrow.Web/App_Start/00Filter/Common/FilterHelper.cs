using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Web
{
    public static class FilterHelper
    {
        public static List<string> GetFilterList(ActionExecutingContext context)
        {
            return context.Filters.Select(x => x.GetType().FullName).ToList();
        }
    }
}
