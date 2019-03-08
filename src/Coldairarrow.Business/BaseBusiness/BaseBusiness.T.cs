using Coldairarrow.Business.Common;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_SysManage;
using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Coldairarrow.Business
{
    /// <summary>
    /// 描述：业务处理基类
    /// 作者：Coldairarrow
    /// </summary>
    /// <typeparam name="T">泛型约束（数据库实体）</typeparam>
    public class BaseBusiness<T> : IRepository<T> where T : class, new()
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseBusiness()
        {
            SetService(null, null, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">连接名或连接字符串</param>
        public BaseBusiness(string conStr)
        {
            SetService(conStr, null, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">连接名或连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        public BaseBusiness(string conStr, DatabaseType dbType)
        {
            SetService(conStr, dbType, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">数据库连接名或连接字符串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="entityNamespace">实体命名空间</param>
        public BaseBusiness(string conStr, DatabaseType dbType, string entityNamespace)
        {
            SetService(conStr, dbType, entityNamespace);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">上下文连接的DbContext</param>
        public BaseBusiness(DbContext dbContext)
        {
            SetService(dbContext, null, null);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">上下文连接的DbContext</param>
        /// <param name="dbType">数据库类型</param>
        public BaseBusiness(DbContext dbContext, DatabaseType dbType)
        {
            SetService(dbContext, dbType, null);
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 设置仓储服务
        /// </summary>
        /// <param name="param">参数</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="entityNamespace">命名空间</param>
        private void SetService(object param, DatabaseType? dbType, string entityNamespace)
        {
            Service = DbFactory.GetRepository(param, dbType, entityNamespace);
        }

        #endregion

        #region 外部属性

        public IRepository Service { get; set; }

        #endregion

        #region 事物提交

        /// <summary>
        /// 开始事物提交
        /// </summary>
        public void BeginTransaction()
        {
            Service.BeginTransaction();
        }

        /// <summary>
        /// 结束事物提交
        /// </summary>
        public bool EndTransaction()
        {
            return Service.EndTransaction();
        }

        #endregion

        #region 增加数据

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        public void Insert(T entity)
        {
            Service.Insert<T>(entity);
        }

        /// <summary>
        /// 插入数据列表
        /// 注：若数据量较大，请使用BulkInsert
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">实体列表</param>
        public void Insert(List<T> entities)
        {
            Service.Insert(entities);
        }

        /// <summary>
        /// 使用Bulk批量插入数据
        /// 注：适合大数据量，速度非常快,不支持事务提交（本身是一个事务）
        /// </summary>
        /// <param name="entities">数据</param>
        public void BulkInsert(List<T> entities)
        {
            Service.BulkInsert(entities);
        }

        #endregion

        #region 删除数据

        /// <summary>
        /// 删除表中所有数据
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        public void DeleteAll()
        {
            Service.DeleteAll<T>();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="key">该记录主键</param>
        public void Delete(string key)
        {
            Service.Delete<T>(key);
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="keys">主键列表</param>
        public void Delete(List<string> keys)
        {
            Service.Delete<T>(keys);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Delete(T entity)
        {
            Service.Delete(entity);
        }

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据列表</param>
        public void Delete(List<T> entities)
        {
            Service.Delete(entities);
        }

        /// <summary>
        /// 通过条件删除数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="condition">条件</param>
        public void Delete(Expression<Func<T, bool>> condition)
        {
            Service.Delete(condition);
        }

        #endregion

        #region 更新数据

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        public void Update(T entity)
        {
            Service.Update(entity);
        }

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据列表</param>
        public void Update(List<T> entities)
        {
            Service.Update(entities);
        }

        /// <summary>
        /// 更新一条数据,某些属性
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="properties">需要更新的字段</param>
        public void UpdateAny(T entity, List<string> properties)
        {
            Service.UpdateAny(entity, properties);
        }

        /// <summary>
        /// 更新多条数据,某些属性
        /// </summary>
        /// <param name="entities">数据列表</param>
        /// <param name="properties">需要更新的字段</param>
        public void UpdateAny(List<T> entities, List<string> properties)
        {
            Service.UpdateAny(entities, properties);
        }

        /// <summary>
        /// 指定条件更新
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="whereExpre">筛选表达式</param>
        /// <param name="set">更改属性回调</param>
        public void UpdateWhere(Expression<Func<T, bool>> whereExpre, Action<T> set)
        {
            Service.UpdateWhere(whereExpre, set);
        }

        #endregion

        #region 查询数据

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public T GetEntity(params object[] keyValue)
        {
            return Service.GetEntity<T>(keyValue);
        }

        /// <summary>
        /// 获取表的所有数据，当数据量很大时不要使用！
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public List<T> GetList()
        {
            return Service.GetList<T>();
        }

        /// <summary>
        /// 获取实体对应的表，延迟加载，主要用于支持Linq查询操作
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns></returns>
        public virtual IQueryable<T> GetIQueryable()
        {
            return Service.GetIQueryable<T>();
        }

        /// <summary>
        /// 获取分页后的数据
        /// </summary>
        /// <typeparam name="U">实体类型</typeparam>
        /// <param name="query">数据源IQueryable</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public List<U> GetPagination<U>(IQueryable<U> query, Pagination pagination)
        {
            return query.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 获取分页后的数据
        /// </summary>
        /// <typeparam name="U">实体参数</typeparam>
        /// <param name="query">IQueryable数据源</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageRows">每页行数</param>
        /// <param name="orderColumn">排序列</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="count">总记录数</param>
        /// <param name="pages">总页数</param>
        /// <returns></returns>
        public List<U> GetPagination<U>(IQueryable<U> query, int pageIndex, int pageRows, string orderColumn, string orderType, ref int count, ref int pages)
        {
            Pagination pagination = new Pagination
            {
                page = pageIndex,
                rows = pageRows,
                sord = orderType,
                sidx = orderColumn
            };
            count = pagination.records = query.Count();
            pages = pagination.total;

            return query.GetPagination(pagination).ToList();
        }

        /// <summary>
        /// 通过Sql查询返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql)
        {
            return Service.GetDataTableWithSql(sql);
        }

        /// <summary>
        /// 通过Sql参数查询返回DataTable
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns></returns>
        public DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters)
        {
            return Service.GetDataTableWithSql(sql, parameters);
        }

        /// <summary>
        /// 通过sql返回List
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <returns></returns>
        public List<U> GetListBySql<U>(string sqlStr) where U : class, new()
        {
            return Service.GetListBySql<U>(sqlStr);
        }

        /// <summary>
        /// 通过sql返回list
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sqlStr">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public List<U> GetListBySql<U>(string sqlStr, List<DbParameter> param) where U : class, new()
        {
            return Service.GetListBySql<U>(sqlStr, param);
        }

        #endregion

        #region 执行Sql语句

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void ExecuteSql(string sql)
        {
            Service.ExecuteSql(sql);
        }

        /// <summary>
        /// 通过参数执行Sql语句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void ExecuteSql(string sql, List<DbParameter> parameters)
        {
            Service.ExecuteSql(sql, parameters);
        }

        #endregion

        #region 业务返回

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public AjaxResult Success()
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = "请求成功！",
                Data = null
            };

            return res;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public AjaxResult Success(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = msg,
                Data = null
            };

            return res;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public AjaxResult Success(object data)
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = "请求成功！",
                Data = data
            };

            return res;
        }

        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="msg">返回的消息</param>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public AjaxResult Success(string msg, object data)
        {
            AjaxResult res = new AjaxResult
            {
                Success = true,
                Msg = msg,
                Data = data
            };

            return res;
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        public AjaxResult Error()
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                Msg = "请求失败！",
                Data = null
            };

            return res;
        }

        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <returns></returns>
        public AjaxResult Error(string msg)
        {
            AjaxResult res = new AjaxResult
            {
                Success = false,
                Msg = msg,
                Data = null
            };

            return res;
        }

        #endregion

        #region 其它操作

        public virtual EnumType.LogType LogType { get => throw new Exception("请在子类重写"); }

        public void WriteSysLog(string logContent)
        {
            WriteSysLog(logContent, LogType);
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logContent">日志内容</param>
        /// <param name="logType">日志类型</param>
        public static void WriteSysLog(string logContent, EnumType.LogType logType)
        {
            BusHelper.WriteSysLog(logContent, logType);
        }

        /// <summary>
        /// 处理系统异常
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static void HandleException(Exception ex)
        {
            BusHelper.HandleException(ex);
        }

        /// <summary>
        /// 校验重复的数据字段
        /// </summary>
        /// <param name="data">校验的数据</param>
        public void CheckRepeatProperty(T data)
        {
            CheckRepeatProperty(data, CheckRepeatPropertyConfig);
        }

        /// <summary>
        /// 校验重复的数据字段
        /// </summary>
        /// <param name="data">校验的数据</param>
        /// <param name="properties">校验的属性，Key为字段名，Value为重复后的提示信息</param>
        public void CheckRepeatProperty(T data, Dictionary<string, string> properties)
        {
            foreach (var aProperty in properties)
            {
                if (!data.GetPropertyValue(aProperty.Key).IsNullOrEmpty())
                {
                    int count = GetIQueryable().Where($"Id!=@0&&{aProperty.Key}==@1", data.GetPropertyValue("Id"), data.GetPropertyValue(aProperty.Key)).Count();
                    if (count > 0)
                        throw new Exception(aProperty.Value);
                }
            }
        }

        public virtual Dictionary<string, string> CheckRepeatPropertyConfig { get; } = new Dictionary<string, string>();

        #endregion
    }
}
