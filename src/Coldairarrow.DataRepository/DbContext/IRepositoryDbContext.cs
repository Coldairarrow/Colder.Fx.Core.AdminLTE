using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;

namespace Coldairarrow.DataRepository
{
    public interface IRepositoryDbContext : IDisposable
    {
        DbContext GetDbContext();
        Action<string> HandleSqlLog { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet<dynamic> Set(Type entityType);
        EntityEntry Entry(object entity);
        int SaveChanges();
        DatabaseFacade Database { get; }
        Type CheckEntityType(Type entityType);
        void UseTransaction(DbTransaction transaction);
        void RefreshDb();
    }
}
