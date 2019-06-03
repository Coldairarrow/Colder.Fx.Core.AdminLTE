using Coldairarrow.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Common;
using static Coldairarrow.DataRepository.EFCoreSqlLogeerProvider;

namespace Coldairarrow.DataRepository
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DatabaseType dbType, DbConnection existingConnection, IModel model)
        {
            _dbType = dbType;
            _dbConnection = existingConnection;
            _model = model;
        }
        private DatabaseType _dbType { get; }
        private DbConnection _dbConnection { get; }
        private IModel _model { get; }
        public Action<string> HandleSqlLog { set => _loggerHandlerContainer.HandleSqlLog = value; }
        LoggerHandlerContainer _loggerHandlerContainer { get; } = new LoggerHandlerContainer();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (_dbType)
            {
                case DatabaseType.SqlServer: optionsBuilder.UseSqlServer(_dbConnection, x => x.UseRowNumberForPaging()); break;
                case DatabaseType.MySql: optionsBuilder.UseMySql(_dbConnection); break;
                case DatabaseType.PostgreSql: optionsBuilder.UseNpgsql(_dbConnection); break;
                default: throw new Exception("暂不支持该数据库！");
            }
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseModel(_model);
            optionsBuilder.UseLoggerFactory(new LoggerFactory(new ILoggerProvider[] { new EFCoreSqlLogeerProvider(_loggerHandlerContainer) }));
        }
    }
}
