using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Coldairarrow.DataRepository
{
    public interface IRepository
    {
        #region 数据库连接相关方法

        DbContext GetDbContext();

        Action<string> HandleSqlLog { set; }

        #endregion

        #region 事物提交

        /// <summary>
        /// 开始单库事物
        /// 注意:若要使用跨库事务,请使用DistributedTransaction
        /// </summary>
        void BeginTransaction();
        bool EndTransaction();

        #endregion

        #region 增加数据

        void Insert<T>(T entity) where T : class, new();
        void Insert<T>(List<T> entities) where T : class, new();
        void BulkInsert<T>(List<T> entities) where T : class, new();

        #endregion

        #region 删除数据

        void DeleteAll<T>() where T : class, new();
        void Delete<T>(string key) where T : class, new();
        void Delete<T>(List<string> keys) where T : class, new();
        void Delete<T>(T entity) where T : class, new();
        void Delete<T>(List<T> entities) where T : class, new();
        void Delete<T>(Expression<Func<T, bool>> condition) where T : class, new();

        #endregion

        #region 更新数据

        void Update<T>(T entity) where T : class, new();
        void Update<T>(List<T> entities) where T : class, new();
        void UpdateAny<T>(T entity, List<string> properties) where T : class, new();
        void UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new();
        void UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new();

        #endregion

        #region 查询数据

        T GetEntity<T>(params object[] keyValue) where T : class, new();
        List<T> GetList<T>() where T : class, new();
        IQueryable<T> GetIQueryable<T>() where T : class, new();
        DataTable GetDataTableWithSql(string sql);
        DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters);
        List<T> GetListBySql<T>(string sqlStr) where T : class, new();
        List<T> GetListBySql<T>(string sqlStr, List<DbParameter> parameters) where T : class, new();

        #endregion

        #region 执行Sql语句

        void ExecuteSql(string sql);
        void ExecuteSql(string sql, List<DbParameter> parameters);

        #endregion
    }
}
