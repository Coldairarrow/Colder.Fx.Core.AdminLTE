using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web
{
    public class CommonController : Controller
    {
        public ActionResult ShowBigImg(string url)
        {
            ViewData["url"] = url;
            return View();
        }
    }
}