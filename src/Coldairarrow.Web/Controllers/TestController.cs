using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web
{
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

        [CheckParamNotEmpty("name","sex")]
        public IActionResult Test()
        {
            return Success();
        }
    }
}