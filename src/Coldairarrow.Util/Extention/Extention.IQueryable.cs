using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace Coldairarrow.Util
{
    /// <summary>
    /// IQueryable<T>的拓展操作
    /// 作者：Coldairarrow
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// 获取分页后的数据
        /// </summary>
        /// <typeparam name="T">实体参数</typeparam>
        /// <param name="source">IQueryable数据源</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageRows">每页行数</param>
        /// <param name="orderColumn">排序列</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="count">总记录数</param>
        /// <param name="pages">总页数</param>
        /// <returns></returns>
        public static IQueryable<T> GetPagination<T>(this IQueryable<T> source, int pageIndex, int pageRows, string orderColumn, string orderType, ref int count, ref int pages)
        {
            Pagination pagination = new Pagination
            {
                page = pageIndex,
                rows = pageRows,
                sord = orderType,
                sidx = orderColumn
            };

            return source.GetPagination(pagination);
        }

        /// <summary>
        /// 获取分页后的数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源IQueryable</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public static IQueryable<T> GetPagination<T>(this IQueryable<T> source, Pagination pagination)
        {
            pagination.records = source.Count();
            source = source.OrderBy(pagination.SortField, pagination.SortType);
            return source.Skip((pagination.page - 1) * pagination.rows).Take(pagination.rows);
        }

        /// <summary>
        /// 动态排序法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">IQueryable数据源</param>
        /// <param name="sortColumn">排序的列</param>
        /// <param name="sortType">排序的方法</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortColumn, string sortType)
        {
            return source.OrderBy(new KeyValuePair<string, string>(sortColumn, sortType));
        }

        /// <summary>
        /// 动态排序法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="sort">排序规则，Key为排序列，Value为排序类型</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, params KeyValuePair<string, string>[] sort)
        {
            var parameter = Expression.Parameter(typeof(T), "o");

            sort.ForEach((aSort, index) =>
            {
                //根据属性名获取属性
                var property = GetTheProperty(typeof(T), aSort.Key);
                //创建一个访问属性的表达式
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);

                string OrderName = "";
                if (index > 0)
                {
                    OrderName = aSort.Value.ToLower() == "desc" ? "ThenByDescending" : "ThenBy";
                }
                else
                    OrderName = aSort.Value.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";

                MethodCallExpression resultExp = Expression.Call(
                    typeof(Queryable), OrderName,
                    new Type[] { typeof(T), property.PropertyType },
                    source.Expression,
                    Expression.Quote(orderByExp));

                source = source.Provider.CreateQuery<T>(resultExp);
            });

            return (IOrderedQueryable<T>)source;

            //必须追溯到最基类属性
            PropertyInfo GetTheProperty(Type type, string propertyName)
            {
                if (type.BaseType.GetProperties().Any(x => x.Name == propertyName))
                    return GetTheProperty(type.BaseType, propertyName);
                else
                    return type.GetProperty(propertyName);
            }
        }

        /// <summary>
        /// 拓展IQueryable<T>方法操作
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static IQueryable<T> AsExpandable<T>(this IQueryable<T> source)
        {
            return LinqKit.Extensions.AsExpandable(source);
        }

        /// <summary>
        /// 转换为对应的Sql语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="query">数据源</param>
        /// <returns></returns>
        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();
            FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");
            FieldInfo QueryModelGeneratorField = QueryCompilerTypeInfo.DeclaredFields.First(x => x.Name == "_queryModelGenerator");
            FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");
            PropertyInfo DatabaseDependenciesField = typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");
            var queryCompiler = (QueryCompiler)QueryCompilerField.GetValue(query.Provider);
            var modelGenerator = (QueryModelGenerator)QueryModelGeneratorField.GetValue(queryCompiler);
            var queryModel = modelGenerator.ParseQuery(query.Expression);
            var database = (IDatabase)DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        }
    }
}
