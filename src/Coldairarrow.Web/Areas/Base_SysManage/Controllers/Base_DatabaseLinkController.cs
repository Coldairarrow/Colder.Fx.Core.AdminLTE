using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class Base_DatabaseLinkController : BaseMvcController
    {
        #region DI

        public Base_DatabaseLinkController(IBase_DatabaseLinkBusiness dbLinkBus)
        {
            _dbLinkBus = dbLinkBus;
        }
        IBase_DatabaseLinkBusiness _dbLinkBus { get; }

        #endregion

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_DatabaseLink() : _dbLinkBus.GetTheData(id);

            return View(theData);
        }

        #endregion

        #region 获取数据

        public ActionResult GetDataList()
        {
            Pagination pagination = new Pagination();
            var dataList = _dbLinkBus.GetDataList(pagination);

            return DataTable_Bootstrap(dataList, pagination);
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Base_DatabaseLink theData)
        {
            if(theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();

                _dbLinkBus.AddData(theData);
            }
            else
            {
                _dbLinkBus.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _dbLinkBus.DeleteData(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}