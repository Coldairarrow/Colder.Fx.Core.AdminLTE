using AutoMapper;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_SysManage
{
    public class Base_SysRoleBusiness : BaseBusiness<Base_SysRole>, IBase_SysRoleBusiness, IDependency
    {
        #region DI

        public Base_SysRoleBusiness(IPermissionManage permissionManage)
        {
            _permissionManage = permissionManage;
        }
        IPermissionManage _permissionManage { get; }

        #endregion

        #region �ⲿ�ӿ�

        public List<Base_SysRoleDTO> GetDataList(Pagination pagination, string roldId = null, string roleName = null)
        {
            var where = LinqHelper.True<Base_SysRole>();
            if (!roldId.IsNullOrEmpty())
                where = where.And(x => x.Id == roldId);
            if (!roleName.IsNullOrEmpty())
                where = where.And(x => x.RoleName.Contains(roleName));

            var list = GetIQueryable()
                .Where(where)
                .GetPagination(pagination)
                .ToList()
                .Select(x => Mapper.Map<Base_SysRoleDTO>(x))
                .ToList();

            return list;
        }

        /// <summary>
        /// ��ȡָ���ĵ�������
        /// </summary>
        /// <param name="id">����</param>
        /// <returns></returns>
        public Base_SysRole GetTheData(string id)
        {
            return GetEntity(id);
        }

        public Base_SysRoleDTO GetTheInfo(string id)
        {
            return GetDataList(new Pagination(), id).FirstOrDefault();
        }

        /// <summary>
        /// �������
        /// </summary>
        /// <param name="newData">����</param>
        [DataAddLog(LogType.ϵͳ��ɫ����, "RoleName", "��ɫ")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "��ɫ��" })]
        public AjaxResult AddData(Base_SysRole newData)
        {
            Insert(newData);

            return Success();
        }

        /// <summary>
        /// ��������
        /// </summary>
        [DataEditLog(LogType.ϵͳ��ɫ����, "RoleName", "��ɫ")]
        [DataRepeatValidate(new string[] { "RoleName" }, new string[] { "��ɫ��" })]
        public AjaxResult UpdateData(Base_SysRole theData)
        {
            Update(theData);

            return Success();
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="theData">ɾ��������</param>
        [DataDeleteLog(LogType.ϵͳ��ɫ����, "RoleName", "��ɫ")]
        public AjaxResult DeleteData(List<string> ids)
        {
            Delete(ids);

            return Success();
        }

        /// <summary>
        /// ����Ȩ��
        /// </summary>
        /// <param name="roleId">��ɫId</param>
        /// <param name="permissions">Ȩ��ֵ</param>
        public AjaxResult SavePermission(string roleId, List<string> permissions)
        {
            Service.Delete<Base_PermissionRole>(x => x.RoleId == roleId);
            List<Base_PermissionRole> insertList = new List<Base_PermissionRole>();
            permissions.ForEach(newPermission =>
            {
                insertList.Add(new Base_PermissionRole
                {
                    Id = IdHelper.GetId(),
                    RoleId = roleId,
                    PermissionValue = newPermission
                });
            });

            Service.Insert(insertList);
            _permissionManage.ClearUserPermissionCache();

            return Success();
        }

        #endregion

        #region ˽�г�Ա

        #endregion

        #region ����ģ��

        #endregion
    }
}