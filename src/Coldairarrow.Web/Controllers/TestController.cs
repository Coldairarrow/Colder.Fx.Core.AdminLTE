using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        //[CheckSign]
        public ActionResult RequestTest()
        {
            //var bus = AutofacHelper.GetService<IBase_UserBusiness>();
            var db = DbFactory.GetRepository();
            Base_User data = new Base_User
            {
                Id = IdHelper.GetId()
            };
            db.Insert(data);
            db.Update(data);
            db.GetIQueryable<Base_User>().FirstOrDefault();
            db.Delete(data);
            db.Dispose();

            return Success("");
        }
    }
}