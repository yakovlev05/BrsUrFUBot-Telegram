using Microsoft.EntityFrameworkCore;
using UrfuService.Data.Entities;

namespace UrfuService.Data;

public class DataContext : DbContext
{
    public DbSet<BrsEntity> Brs { get; set; }

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