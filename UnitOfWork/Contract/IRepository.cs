namespace UnitOfWork.Contract;

public interface IRepository<TE, in TK> where TE : class, IKey<TK>
{
    void Create(TE entity);
    Task CreateAsync(TE entity);

    void CreateBatch(IEnumerable<TE> entity);
    Task CreateBatchAsync(IEnumerable<TE> entity);

    void Delete(TE entity);
    Task DeleteAsync(TE entity);

    void DeleteBatch(IEnumerable<TE> entity);
    Task DeleteBatchAsync(IEnumerable<TE> entity);

    TE? Get(TK id);
    Task<TE?>? GetAsync(TK id);
    IQueryable<TE> GetAllQueryable();

    void Update(TE entity);
    Task UpdateAsync(TE entity);

    void UpdateBatch(IEnumerable<TE> entity);
    Task UpdateBatchAsync(IEnumerable<TE> entity);
}