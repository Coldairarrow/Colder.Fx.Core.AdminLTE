using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web
{
    //[CheckUrlPermission]
    public class TestController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RequestDemo()
        {
            return View();
        }

        public IActionResult Test(string name,int age)
        {
            return Content(new { name, age }.ToJson());
        }
    }
}