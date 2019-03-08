using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Coldairarrow.DataRepository
{
    public interface IRepository<T> where T : class, new()
    {
        #region 事物提交

        void BeginTransaction();
        bool EndTransaction();

        #endregion

        #region 增加数据

        void Insert(T entity);
        void Insert(List<T> entities);
        void BulkInsert(List<T> entities);

        #endregion

        #region 删除数据

        void DeleteAll();
        void Delete(string key);
        void Delete(List<string> keys);
        void Delete(T entity);
        void Delete(List<T> entities);
        void Delete(Expression<Func<T, bool>> condition);

        #endregion

        #region 更新数据

        void Update(T entity);
        void Update(List<T> entities);
        void UpdateAny(T entity, List<string> properties);
        void UpdateAny(List<T> entities, List<string> properties);

        #endregion

        #region 查询数据

        T GetEntity(params object[] keyValue);
        List<T> GetList();
        IQueryable<T> GetIQueryable();
        DataTable GetDataTableWithSql(string sql);
        DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters);
        List<U> GetListBySql<U>(string sqlStr) where U : class, new();
        List<U> GetListBySql<U>(string sqlStr, List<DbParameter> param) where U : class, new();

        #endregion

        #region 执行Sql语句

        void ExecuteSql(string sql);
        void ExecuteSql(string sql, List<DbParameter> spList);

        #endregion
    }
}
