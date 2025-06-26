using Fantasy.Shared.Entities.Country;
using Fantasy.Shared.Entities.Team;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CountryModel> Countries { get; set; }
    public DbSet<TeamModel> Teams { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}