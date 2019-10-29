using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
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

        /// <summary>
        /// 压力测试
        /// </summary>
        /// <returns></returns>
        public ActionResult PressTest()
        {
            var bus = AutofacHelper.GetScopeService<IBase_UserBusiness>();
            var db = DbFactory.GetRepository();
            Base_UnitTest data = new Base_UnitTest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Age = 10,
                UserName = Guid.NewGuid().ToString()
            };
            db.Insert(data);
            db.Update(data);
            db.GetIQueryable<Base_UnitTest>().FirstOrDefault();
            db.Delete(data);

            return Success("");
        }
    }
}