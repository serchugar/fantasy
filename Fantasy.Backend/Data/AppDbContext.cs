using Fantasy.Shared.Entities.Country;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Fantasy.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CountryModel> Countries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Countries
        modelBuilder.Entity<CountryModel>().HasIndex(c => c.Name).IsUnique();
        #endregion
        
        DisableCascadingDelete(modelBuilder);
    }

    private void DisableCascadingDelete(ModelBuilder modelBuilder)
    {
        IEnumerable<IMutableForeignKey> relationships = modelBuilder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys());
        foreach (IMutableForeignKey relationship in relationships)
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}