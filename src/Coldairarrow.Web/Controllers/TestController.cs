using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web
{
    public class TestController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}