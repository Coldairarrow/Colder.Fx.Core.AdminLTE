using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Coldairarrow.DataRepository
{
    public class SqlServerRepository : DbRepository, IRepository
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlServerRepository()
            : base(null, DatabaseType.SqlServer, null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">数据库连接名</param>
        public SqlServerRepository(string conStr)
            : base(conStr, DatabaseType.SqlServer, null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="conStr">数据库连接名</param>
        /// <param name="entityNamespace">实体命名空间</param>
        public SqlServerRepository(string conStr, string entityNamespace)
            : base(conStr, DatabaseType.SqlServer, entityNamespace)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbContext">数据库连接上下文</param>
        public SqlServerRepository(DbContext dbContext)
            : base(dbContext, DatabaseType.SqlServer, null)
        {
        }

        #endregion

        #region 插入数据

        /// <summary>
        /// 使用Bulk批量插入数据（适合大数据量，速度非常快）
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entities">数据</param>
        public override void BulkInsert<T>(List<T> entities)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connectionString;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string tableName = string.Empty;
                var tableAttribute = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault();
                if (tableAttribute != null)
                    tableName = ((TableAttribute)tableAttribute).Name;
                else
                    tableName = typeof(T).Name;

                SqlBulkCopy sqlBC = new SqlBulkCopy(conn)
                {
                    BatchSize = 100000,
                    BulkCopyTimeout = 0,
                    DestinationTableName = tableName
                };
                using (sqlBC)
                {
                    sqlBC.WriteToServer(entities.ToDataTable());
                }
            }
        }

        #endregion
    }
}
