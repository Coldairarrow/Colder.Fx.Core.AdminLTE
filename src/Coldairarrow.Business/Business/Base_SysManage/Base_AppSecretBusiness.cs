using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Business.Base_SysManage
{
    public class Base_AppSecretBusiness : BaseBusiness<Base_AppSecret>, IBase_AppSecretBusiness, IDependency
    {
        #region 外部接口

        public List<Base_AppSecret> GetDataList(Pagination pagination, string keyword)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Base_AppSecret>();
            if (!keyword.IsNullOrEmpty())
            {
                where = where.And(x => 
                    x.AppId.Contains(keyword)
                    || x.AppSecret.Contains(keyword)
                    || x.AppName.Contains(keyword));
            }

            return q.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 获取指定的单条数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public Base_AppSecret GetTheData(string id)
        {
            return GetEntity(id);
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="newData">数据</param>
        public void AddData(Base_AppSecret newData)
        {
            Insert(newData);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        public void UpdateData(Base_AppSecret theData)
        {
            Update(theData);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="theData">删除的数据</param>
        public void DeleteData(List<string> ids)
        {
            Delete(ids);
        }

        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="appId">应用Id</param>
        /// <param name="permissions">权限值</param>
        public void SavePermission(string appId, List<string> permissions)
        {
            Service.Delete_Sql<Base_PermissionAppId>(x => x.AppId == appId);

            List<Base_PermissionAppId> insertList = new List<Base_PermissionAppId>();
            permissions.ForEach(newPermission =>
            {
                insertList.Add(new Base_PermissionAppId
                {
                    Id = IdHelper.GetId(),
                    AppId = appId,
                    PermissionValue = newPermission
                });
            });

            Service.Insert(insertList);
        }

        #endregion

        #region 私有成员

        #endregion

        #region 数据模型

        #endregion
    }
}