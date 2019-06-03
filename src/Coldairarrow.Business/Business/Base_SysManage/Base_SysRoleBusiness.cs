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

        #region 外部接口

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
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
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
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(Base_SysRole newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(Base_SysRole theData)
        {
            Update(theData);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public void DeleteData(List<string> ids)
        {
            //删除角色
            Delete(ids);
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="permissions">权限值</param>
        public void SavePermission(string roleId, List<string> permissions)
        {
            Service.Delete_Sql<Base_PermissionRole>(x => x.RoleId == roleId);
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
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}