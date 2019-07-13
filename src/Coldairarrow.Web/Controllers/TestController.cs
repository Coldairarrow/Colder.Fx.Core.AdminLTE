using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web
{
    public class TestController : BaseController
    {
        public TestController(IBase_UserBusiness userBus)
        {
            _userBus = userBus;
        }
        private IBase_UserBusiness _userBus { get; }
        public ActionResult Index()
        {
            return View();
        }
    }
}