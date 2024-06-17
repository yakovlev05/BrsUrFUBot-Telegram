namespace UserService.Data.Repositories;

public interface IRepository<T> : IDisposable where T : class
{
    Task<T?> GetAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}