using Coldairarrow.Business.Base_SysManage;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Coldairarrow.Web.Areas.Base_SysManage.Controllers
{
    [Area("Base_SysManage")]
    public class Base_UserController : BaseMvcController
    {
        #region DI

        public Base_UserController(IBase_UserBusiness sysUserBus, IPermissionManage permissionManage)
        {
            _sysUserBus = sysUserBus;
            _permissionManage = permissionManage;
        }
        IBase_UserBusiness _sysUserBus { get; }
        IPermissionManage _permissionManage { get; set; }

        #endregion

        #region ��ͼ����

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Form(string id)
        {
            var theData = id.IsNullOrEmpty() ? new Base_User() : _sysUserBus.GetTheData(id);

            return View(theData);
        }

        public ActionResult ChangePwdForm()
        {
            return View();
        }

        public ActionResult PermissionForm(string userId)
        {
            ViewData["userId"] = userId;

            return View();
        }

        #endregion

        #region ��ȡ����

        public ActionResult GetDataList(Pagination pagination, string keyword)
        {
            var dataList = _sysUserBus.GetDataList(pagination, false, null, keyword);

            return DataTable_Bootstrap(dataList, pagination);
        }

        public ActionResult GetDataList_NoPagin(string values, string q)
        {
            var resList = _sysUserBus.BuildSelectResult(values, q, "RealName", "Id");

            return Content(resList.ToJson());
        }

        #endregion

        #region �ύ����

        public ActionResult SaveData(Base_User theData, string Pwd, string RoleIdList)
        {
            AjaxResult res;
            if (!Pwd.IsNullOrEmpty())
                theData.Password = Pwd.ToMD5String();

            if (theData.Id.IsNullOrEmpty())
            {
                theData.Id = IdHelper.GetId();

                res = _sysUserBus.AddData(theData);
            }
            else
            {
                res = _sysUserBus.UpdateData(theData);
            }

            //��ɫ����
            if (!RoleIdList.IsNullOrEmpty() && res.Success)
            {
                _sysUserBus.SetUserRole(theData.Id, RoleIdList.ToList<string>());
            }

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        public ActionResult DeleteData(string ids)
        {
            var res = _sysUserBus.DeleteData(ids.ToList<string>());

            return JsonContent(res.ToJson());
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="oldPwd">������</param>
        /// <param name="newPwd">������</param>
        public ActionResult ChangePwd(string oldPwd, string newPwd)
        {
            var res = _sysUserBus.ChangePwd(oldPwd, newPwd);

            return Content(res.ToJson());
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="permissions">Ȩ��</param>
        /// <returns></returns>
        public ActionResult SavePermission(string userId, string permissions)
        {
            _permissionManage.SetUserPermission(userId, permissions.ToList<string>());

            return Success();
        }

        #endregion
    }
}