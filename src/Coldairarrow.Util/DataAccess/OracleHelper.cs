using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Coldairarrow.Util
{
    /// <summary>
    /// SqlServer数据库操作帮助类
    /// </summary>
    public class OracleHelper : DbHelper
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nameOrConStr">数据库连接名或连接字符串</param>
        public OracleHelper(string nameOrConStr)
            : base(DatabaseType.Oracle, nameOrConStr)
        {
        }

        #endregion

        #region 私有成员

        protected override Dictionary<string, Type> DbTypeDic { get; } = new Dictionary<string, Type>()
        {
            { "BFILE", typeof(byte[]) },
            { "BLOB", typeof(byte[]) },
            { "CHAR", typeof(string) },
            { "CLOB", typeof(string) },
            { "DATE", typeof(DateTime) },
            { "FLOAT", typeof(decimal) },
            { "INTEGER", typeof(decimal) },
            { "INTERVAL YEAR TO MONTH", typeof(Int32) },
            { "INTERVAL DAY TO SECOND", typeof(TimeSpan) },
            { "LONG", typeof(string) },
            { "LONG RAW", typeof(string[]) },
            { "NCHAR", typeof(string) },
            { "NCLOB", typeof(string) },
            { "NUMBER", typeof(decimal) },
            { "NVARCHAR2", typeof(string) },
            { "RAW", typeof(byte[]) },
            { "ROWID", typeof(string) },
            { "TIMESTAMP", typeof(DateTime) },
            { "TIMESTAMP WITH LOCAL TIME ZONE", typeof(DateTime) },
            { "TIMESTAMP WITH TIME ZONE", typeof(DateTime) },
            { "UNSIGNED INTEGER", typeof(decimal) },
            { "VARCHAR2", typeof(string) }
        };

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取数据库中的所有表
        /// </summary>
        /// <param name="schemaName">模式（架构）</param>
        /// <returns></returns>
        public override List<DbTableInfo> GetDbAllTables(string schemaName = null)
        {
            DbProviderFactory dbProviderFactory = DbProviderFactoryHelper.GetDbProviderFactory(_dbType);
            string dbName = string.Empty;
            OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder(_conStr);
            dbName = builder.UserID;
            if (schemaName.IsNullOrEmpty())
                schemaName = dbName;

            string sql = @"select 
	TABLE_NAME as ""TableName"",
	COMMENTS as ""Description""
from all_tab_comments 
where owner =:schemaName";
            return GetListBySql<DbTableInfo>(sql, new List<DbParameter> { new OracleParameter("schemaName", schemaName) });
        }

        /// <summary>
        /// 通过连接字符串和表名获取数据库表的信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public override List<TableInfo> GetDbTableInfo(string tableName)
        {
            string sql = @"select
       a.COLUMN_NAME as ""Name"",
       a.DATA_TYPE as ""Type"",
       case when a.nullable = 'N' then  0 else 1 end as ""IsNullable"",
       b.comments as ""Description"", 
       case
          when (select count(*) from all_cons_columns c 
            where c.table_name=a.TABLE_NAME and c.column_name=a.COLUMN_NAME and c.constraint_name=
                (select d.constraint_name from all_constraints d where d.table_name=c.table_name and d.constraint_type   ='P')
                 )>0 then 1 else 0 end as ""IsKey""
            
  from All_TAB_COLS a,
       all_col_comments b
 where a.table_name = b.table_name      
       and b.COLUMN_NAME = a.COLUMN_NAME   and a.table_name = :tableName
       order by a.TABLE_NAME, a.COLUMN_ID";
            return GetListBySql<TableInfo>(sql, new List<DbParameter> { new OracleParameter("table_name", tableName) });
        }

        /// <summary>
        /// 生成实体文件
        /// </summary>
        /// <param name="infos">表字段信息</param>
        /// <param name="tableName">表名</param>
        /// <param name="tableDescription">表描述信息</param>
        /// <param name="filePath">文件路径（包含文件名）</param>
        /// <param name="nameSpace">实体命名空间</param>
        /// <param name="schemaName">架构（模式）名</param>
        public override void SaveEntityToFile(List<TableInfo> infos, string tableName, string tableDescription, string filePath, string nameSpace, string schemaName = null)
        {
            base.SaveEntityToFile(infos, tableName, tableDescription, filePath, nameSpace, schemaName);
        }

        #endregion
    }
}
