using CatQuiz.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatQuiz.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Question> Questions { get; set; }

    public override int SaveChanges()
    {
        SaveChangesInternal();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SaveChangesInternal();
        return await base.SaveChangesAsync();
    }

    private void SaveChangesInternal()
    {
        var now = DateTime.UtcNow;

        foreach (var changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is BaseEntity entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = now;
                        entity.LastModifiedDate = now;
                        break;
                    case EntityState.Modified:
                        Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        entity.LastModifiedDate = now;
                        break;
                }
            }
        }
    }
}
