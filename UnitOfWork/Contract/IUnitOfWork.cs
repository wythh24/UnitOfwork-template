namespace UnitOfWork.Contract;

public interface IUnitOfWork
{
    void BeginTransaction();
    void Commit();
    void Dispose();
    void RollBack();
    void Save();

    IRepository<TE, TK> GetRepository<TE, TK>() where TE : class, IKey<TK>;
}