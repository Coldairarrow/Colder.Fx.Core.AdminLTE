using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 描述：数据库仓储基类类
    /// 作者：Coldairarrow
    /// </summary>
    public class DbRepository : IRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="param">构造参数，可以为数据库连接字符串或者DbContext</param>
        /// <param name="dbType">数据库类型</param>
        public DbRepository(Object param, DatabaseType dbType, string entityNamespace)
        {
            BuildParam = param;
            _dbType = dbType;
            _entityNamespace = entityNamespace;
            Handle_BuildDbContext = new Func<DbContext>(() =>
            {
                return DbFactory.GetDbContext(BuildParam, _dbType, _entityNamespace);
            });
            _db = Handle_BuildDbContext?.Invoke();
            _connectionString = _db.Database.GetDbConnection().ConnectionString;
            IsDisposed = false;
        }

        #endregion

        #region 拥有成员

        /// <summary>
        /// 连接字符串
        /// </summary>
        protected string _connectionString { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        private DatabaseType _dbType { get; set; }

        /// <summary>
        /// 连接上下文DbContext
        /// </summary>
        private DbContext _db { get; set; }

        /// <summary>
        /// 建造DbConText所需参数
        /// </summary>
        private Object BuildParam { get; set; }

        /// <summary>
        /// 实体命名空间
        /// </summary>
        private string _entityNamespace { get; set; }

        /// <summary>
        /// 标记DbContext是否已经释放
        /// </summary>
        protected bool IsDisposed { get; set; }

        /// <summary>
        /// 判断是否开始事物
        /// </summary>
        protected IDbContextTransaction Transaction { get; set; }

        protected DbContext Db
        {
            get
            {
                if (IsDisposed)
                {
                    _db = Handle_BuildDbContext?.Invoke();
                    IsDisposed = false;
                }

                return _db;
            }
            set
            {
                _db = value;
            }
        }

        protected static PropertyInfo GetKeyProperty<T>()
        {
            return GetKeyPropertys<T>().FirstOrDefault();
        }

        protected static List<PropertyInfo> GetKeyPropertys<T>()
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(x => x.GetCustomAttributes(true).Select(o => o.GetType().FullName).Contains(typeof(KeyAttribute).FullName))
                .ToList();

            return properties;
        }

        protected static string GetDbTableName<T>()
        {
            string tableName = string.Empty;
            var tableAttribute = typeof(T).GetCustomAttribute<TableAttribute>();
            if (tableAttribute != null)
                tableName = tableAttribute.Name;
            else
                tableName = typeof(T).Name;

            return tableName;
        }

        #endregion

        #region 事件处理

        Func<DbContext> Handle_BuildDbContext { get; set; }

        #endregion

        #region 事物相关

        /// <summary>
        /// 是否开启事务,单库事务
        /// </summary>
        protected bool _openedTransaction { get; set; } = false;

        /// <summary>
        /// 需要执行的Sql事务
        /// </summary>
        protected Action _sqlTransaction { get; set; }
        public Action<string> HandleSqlLog { set => EFCoreSqlLogeerProvider.HandleSqlLog = value; }

        /// <summary>
        /// 提交到数据库
        /// </summary>
        protected void Commit()
        {
            //若未开启事物则直接提交到数据库
            if (!_openedTransaction)
            {
                Db.SaveChanges();
                Db.Dispose();
                IsDisposed = true;
            }
        }

        /// <summary>
        /// 释放数据,初始化状态
        /// </summary>
        protected void Dispose()
        {
            Transaction?.Dispose();
            Db?.Dispose();
            IsDisposed = true;
            _openedTransaction = false;
            _sqlTransaction = null;
        }

        /// <summary>
        /// 开始单库事物
        /// 注意:若要使用跨库事务,请使用DistributedTransaction
        /// </summary>
        public void BeginTransaction()
        {
            Transaction = Db.Database.BeginTransaction();
            _openedTransaction = true;
        }

        /// <summary>
        /// 结束事物提交
        /// </summary>
        public bool EndTransaction()
        {
            bool isOK = true;
            try
            {
                _sqlTransaction?.Invoke();
                Db.SaveChanges();
                Transaction.Commit();
            }
            catch
            {
                Transaction.Rollback();
                isOK = false;
            }
            finally
            {
                Dispose();
            }
            return isOK;
        }

        #endregion

        #region 数据库连接相关方法

        /// <summary>
        /// 获取DbContext
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext()
        {
            return Db;
        }

        #endregion

        #region 增加数据

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void Insert<T>(T entity) where T : class, new()
        {
            Db.Add(entity);
            Commit();
        }

        /// <summary>
        /// 插入数据列表
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        public void Insert<T>(List<T> entities) where T : class, new()
        {
            Db.Set<T>().AddRange(entities);
            Commit();
        }

        /// <summary>
        /// 使用Bulk批量插入数据（适合大数据量，速度非常快）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据</param>
        public virtual void BulkInsert<T>(List<T> entities) where T : class, new()
        {

        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除表中所有数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        public virtual void DeleteAll<T>() where T : class, new()
        {
            TableAttribute tableAttribute = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
            string tableName = tableAttribute.Name;
            string sql = $"DELETE {tableName}";
            ExecuteSql(sql);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="key">主键值</param>
        public void Delete<T>(string key) where T : class, new()
        {
            T newData = new T();
            var theProperty = GetKeyProperty<T>();
            if (theProperty == null)
                throw new Exception("该实体没有主键标识！请使用[Key]标识主键！");
            var value = Convert.ChangeType(key, theProperty.PropertyType);
            theProperty.SetValue(newData, value);
            Delete(newData);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="keys">主键列表</param>
        public void Delete<T>(List<string> keys) where T : class, new()
        {
            var theProperty = GetKeyProperty<T>();
            if (theProperty == null)
                throw new Exception("该实体没有主键标识！请使用[Key]标识主键！");

            List<T> deleteList = new List<T>();
            keys.ForEach(aKey =>
            {
                T newData = new T();
                var value = Convert.ChangeType(aKey, theProperty.PropertyType);
                theProperty.SetValue(newData, value);
                deleteList.Add(newData);
            });

            Delete(deleteList);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Delete<T>(T entity) where T : class, new()
        {
            Db.Set<T>().Attach(entity);
            Db.Set<T>().Remove(entity);
            Commit();
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据列表</param>
        public void Delete<T>(List<T> entities) where T : class, new()
        {
            foreach (var entity in entities)
            {
                Db.Set<T>().Attach(entity);
                Db.Set<T>().Remove(entity);
            }
            Commit();
        }

        /// <summary>
        /// 通过条件删除数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="condition">条件</param>
        public virtual void Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            var deleteList = GetIQueryable<T>().Where(condition).ToList();
            Delete(deleteList);
        }

        #endregion

        #region 更新数据

        /// <summary>
        /// 默认更新一个实体，所有字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void Update<T>(T entity) where T : class, new()
        {
            Db.Entry(entity).State = EntityState.Modified;

            Commit();
        }

        /// <summary>
        /// 默认更新实体列表，所有字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public void Update<T>(List<T> entities) where T : class, new()
        {
            entities.ForEach(aEntity =>
            {
                Db.Entry(aEntity).State = EntityState.Modified;
            });

            Commit();
        }

        /// <summary>
        /// 更新一条数据,某些属性
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="properties">需要更新的字段</param>
        public void UpdateAny<T>(T entity, List<string> properties) where T : class, new()
        {
            Db.Set<T>().Attach(entity);
            properties.ForEach(aProperty =>
            {
                Db.Entry(entity).Property(aProperty).IsModified = true;
            });
            Commit();
        }

        /// <summary>
        /// 更新多条数据,某些属性
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据列表</param>
        /// <param name="properties">需要更新的字段</param>
        public void UpdateAny<T>(List<T> entities, List<string> properties) where T : class, new()
        {
            entities.ForEach(aEntity =>
            {
                Db.Set<T>().Attach(aEntity);
                properties.ForEach(aProperty =>
                {
                    Db.Entry(aEntity).Property(aProperty).IsModified = true;
                });
            });

            Commit();
        }

        /// <summary>
        /// 指定条件更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="whereExpre">筛选表达式</param>
        /// <param name="set">更改属性回调</param>
        public void UpdateWhere<T>(Expression<Func<T, bool>> whereExpre, Action<T> set) where T : class, new()
        {
            var list = GetIQueryable<T>().Where(whereExpre).ToList();
            list.ForEach(aData => set(aData));
            Update(list);
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public T GetEntity<T>(params object[] keyValue) where T : class, new()
        {
            return Db.Set<T>().Find(keyValue);
        }

        /// <summary>
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public List<T> GetList<T>() where T : class, new()
        {
            return Db.Set<T>().AsNoTracking().ToList();
        }

        /// <summary>
        /// 获取实体对应的表，延迟加载，主要用于支持Linq查询操作
        /// 注意：无缓存
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public IQueryable<T> GetIQueryable<T>() where T : class, new()
        {
            return Db.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// 通过Sql语句获取DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql)
        {
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(_dbType);
            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;

                    DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet table = new DataSet();
                    adapter.Fill(table);

                    return table.Tables[0];
                }
            }
        }

        /// <summary>
        /// 通过Sql参数查询返回DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters)
        {
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(_dbType);
            using (DbConnection conn = dbProviderFactory.CreateConnection())
            {
                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (DbCommand cmd = conn.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;

                    if (parameters != null && parameters.Count > 0)
                    {
                        foreach (var item in parameters)
                        {
                            cmd.Parameters.Add(item);
                        }
                    }

                    DbDataAdapter adapter = dbProviderFactory.CreateDataAdapter();
                    adapter.SelectCommand = cmd;
                    DataSet table = new DataSet();
                    adapter.Fill(table);
                    cmd.Parameters.Clear();

                    return table.Tables[0];
                }
            }
        }

        /// <summary>
        /// 通过sql返回List
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public List<T> GetListBySql<T>(string sqlStr) where T : class, new()
        {
            return GetDataTableWithSql(sqlStr).ToList<T>();
        }

        /// <summary>
        /// 通过sql返回list
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public List<T> GetListBySql<T>(string sqlStr, List<DbParameter> parameters) where T : class, new()
        {
            return GetDataTableWithSql(sqlStr, parameters).ToList<T>();
        }

        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void ExecuteSql(string sql)
        {
            if (!_openedTransaction)
            {
                Db.Database.ExecuteSqlCommand(sql);
                Dispose();
            }
            else
            {
                _sqlTransaction += new Action(() =>
                {
                    Db.Database.ExecuteSqlCommand(sql);
                });
            }
        }

        /// <summary>
        /// 通过参数执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void ExecuteSql(string sql, List<DbParameter> parameters)
        {
            if (!_openedTransaction)
            {
                Db.Database.ExecuteSqlCommand(sql, parameters.ToArray());
                Dispose();
            }
            else
            {
                _sqlTransaction += new Action(() =>
                {
                    Db.Database.ExecuteSqlCommand(sql, parameters.ToArray());
                });
            }
        }

        #endregion
    }
}
