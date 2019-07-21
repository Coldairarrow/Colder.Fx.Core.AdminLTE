using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class Base_DepartmentController : BaseMvcController
    {
        #region DI

        public Base_DepartmentController(IBase_DepartmentBusiness base_DepartmentBus)
        {
            _base_DepartmentBus = base_DepartmentBus;
        }
        IBase_DepartmentBusiness _base_DepartmentBus { get; }

        #endregion

        #region ��ͼ����

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_Department() : _base_DepartmentBus.GetTheData(id);

            return View(theData);
        }

        #endregion

        #region ��ȡ����

        public ActionResult GetDataList(Pagination pagination, string departmentName = null)
        {
            var dataList = _base_DepartmentBus.GetDataList(pagination, departmentName);

            return DataTable_Bootstrap(dataList, pagination);
        }

        public ActionResult GetDataList_ZTree()
        {
            var dataList = _base_DepartmentBus.GetDataList(new Pagination());
            var resList = dataList.Select(x => new
            {
                id = x.Id,
                name = x.Name,
                pId = x.ParentId,
                open = true
            });

            return JsonContent(resList.ToJson());
        }

        #endregion

        #region �ύ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="theData">���������</param>
        public ActionResult SaveData(Base_Department theData)
        {
            AjaxResult res;
            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();

                res = _base_DepartmentBus.AddData(theData);
            }
            else
            {
                res = _base_DepartmentBus.UpdateData(theData);
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public ActionResult DeleteData(string ids)
        {
            var res = _base_DepartmentBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        #endregion
    }
}