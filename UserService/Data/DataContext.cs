using Microsoft.EntityFrameworkCore;
using UserService.Data.Entities;

namespace UserService.Data;

public class DataContext : DbContext
{
    public DbSet<UserEntity> Users { get; set; }
    
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