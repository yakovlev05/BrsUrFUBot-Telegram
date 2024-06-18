using Microsoft.EntityFrameworkCore;

namespace UrfuService.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        Database.Migrate();
    }

    public new async Task<int> SaveChanges()
    {
        return await base.SaveChangesAsync();
    }

    public DbSet<T> DbSet<T>() where T : class
    {
        return Set<T>();
    }
}