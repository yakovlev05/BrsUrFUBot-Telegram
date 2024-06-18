using Microsoft.EntityFrameworkCore;

namespace UrfuService.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DataContext _dbContext;

    public Repository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T?> GetAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}