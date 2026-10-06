namespace TestTask5_2.Domain.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(CancellationToken ct = default);
    Task<T?> FindAsync(Guid id, CancellationToken ct = default);
    T Create(T entity);
    T Update(T entity);
    void Remove(T entity);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
