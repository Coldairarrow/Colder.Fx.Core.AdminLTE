using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data.Common;

namespace Coldairarrow.DataRepository
{
    public interface IRepositoryDbContext : IDisposable
    {
        DbContext GetDbContext();
        Action<string> HandleSqlLog { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet Set(Type entityType);
        EntityEntry Entry(object entity);
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        Database Database { get; }
        Type CheckEntityType(Type entityType);
        void UseTransaction(DbTransaction transaction);
        void RefreshDb();
    }
}
