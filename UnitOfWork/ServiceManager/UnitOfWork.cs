using Microsoft.EntityFrameworkCore.Storage;
using UnitOfWork.Contract;
using UnitOfWork.Repository;
using static System.GC;

namespace UnitOfWork.ServiceManager;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private IDbContextTransaction? _transaction;
    private readonly IDbContext _context;
    private bool _disposed;
    private readonly Dictionary<string, object> _repository = new();

    public UnitOfWork(IDbContext context)
    {
        _context = context;
    }

    public void BeginTransaction()
    {
        _transaction = _context.Database.BeginTransaction();
    }

    public void Commit() => _transaction?.Commit();


    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this);
    }


    public void RollBack()
    {
        _transaction!.Rollback();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public IRepository<TE, TK> GetRepository<TE, TK>() where TE : class, IKey<TK>
    {
        var type = typeof(TE).Name;
        if (!_repository.ContainsKey(type))
        {
            try
            {
                var repositoryType = typeof(Repository<TE, TK>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                if (repositoryInstance == null) throw new Exception($"Message exception");
                _repository.Add(type, repositoryInstance);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        return (IRepository<TE, TK>) _repository[type];
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();
        _disposed = true;
    }
}