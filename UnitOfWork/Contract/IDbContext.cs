using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UnitOfWork.Contract;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    int SaveChanges();

    DatabaseFacade Database { get; }

    void Dispose();

    // DbSet<User>? Users { get; set; }
}