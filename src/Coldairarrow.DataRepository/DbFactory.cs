using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Coldairarrow.DataRepository
{
    /// <summary>
    /// 数据库工厂
    /// </summary>
    public class DbFactory
    {
        #region 构造函数

        static DbFactory()
        {
            _dbrepositoryContainer = new IocHelper();
            _dbrepositoryContainer.RegisterType<IRepository, SqlServerRepository>(DatabaseType.SqlServer.ToString());
            _dbrepositoryContainer.RegisterType<IRepository, MySqlRepository>(DatabaseType.MySql.ToString());
            _dbrepositoryContainer.RegisterType<IRepository, PostgreSqlRepository>(DatabaseType.PostgreSql.ToString());
        }

        #endregion

        #region 内部成员

        private static IocHelper _dbrepositoryContainer { get; }

        #endregion

        #region 外部接口

        /// <summary>
        /// 根据配置文件获取数据库类型，并返回对应的工厂接口
        /// </summary>
        /// <param name="obj">初始化参数，可为连接字符串或者DbContext</param>
        /// <returns></returns>
        public static IRepository GetRepository(Object obj = null, DatabaseType? dbType = null, string entityNamespace = null)
        {
            IRepository res = null;
            DatabaseType _dbType = GetDbType(dbType);
            Type dbRepositoryType = Type.GetType("Coldairarrow.DataRepository." + DbProviderFactoryHelper.DbTypeToDbTypeStr(_dbType) + "Repository");
            List<object> paramters = new List<object>();
            void BuildParamters()
            {
                if (obj.IsNullOrEmpty())
                    return;

                if (obj is DbContext)
                {
                    paramters.Add(obj);
                    return;
                }
                else if (obj is string)
                {
                    paramters.Add(obj);
                    paramters.Add(entityNamespace);
                }
            }
            BuildParamters();
            res = _dbrepositoryContainer.Resolve<IRepository>(_dbType.ToString(), paramters.ToArray());
            return res;
        }

        /// <summary>
        /// 获取DbType
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        private static DatabaseType GetDbType(DatabaseType? dbType)
        {
            DatabaseType _dbType;
            if (dbType.IsNullOrEmpty())
            {
                _dbType = GlobalSwitch.DatabaseType;
            }
            else
                _dbType = dbType.Value;

            return _dbType;
        }

        /// <summary>
        /// 根据参数获取数据库的DbContext
        /// </summary>
        /// <param name="obj">初始化参数，可为连接字符串或者DbContext</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static DbContext GetDbContext(Object obj, DatabaseType dbType, string entityNamespace)
        {
            if (obj.IsNullOrEmpty())
            {
                return new BaseDbContext(null, dbType, entityNamespace);
            }
            else
            {
                //若参数为字符串
                if (obj is String)
                    return new BaseDbContext((string)obj, dbType, entityNamespace);
                //若参数为DbContext
                else if (obj is DbContext)
                    return (DbContext)Activator.CreateInstance(obj.GetType(), null);
                else
                    throw new Exception("请传入有效的参数！");
            }
        }

        #endregion
    }
}
