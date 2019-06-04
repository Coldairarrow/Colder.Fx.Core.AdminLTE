using Coldairarrow.Business.ProjectManage;
using Coldairarrow.Entity.ProjectManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Web.Areas.ProjectManage.Controllers
{
    [Area("ProjectManage")]
    public class Dev_ProjectController : BaseMvcController
    {
        #region DI

        public Dev_ProjectController(IDev_ProjectBusiness dev_ProjectBus)
        {
            _dev_ProjectBus = dev_ProjectBus;
        }
        IDev_ProjectBusiness _dev_ProjectBus { get; }

        #endregion

        #region 视图功能

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Dev_Project() : _dev_ProjectBus.GetTheData(id);

            return View(theData);
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="condition">查询类型</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ActionResult GetDataList(Pagination pagination, string condition, string keyword)
        {
            var dataList = _dev_ProjectBus.GetDataList(pagination, condition, keyword);

            return DataTable_Bootstrap(dataList, pagination);
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="theData">保存的数据</param>
        public ActionResult SaveData(Dev_Project theData)
        {
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();

                _dev_ProjectBus.AddData(theData);
            }
            else
            {
                _dev_ProjectBus.UpdateData(theData);
            }

            return Success();
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public ActionResult DeleteData(string ids)
        {
            _dev_ProjectBus.DeleteData(ids.ToList<string>());

            return Success("删除成功！");
        }

        #endregion
    }
}