using Fantasy.Shared.Entities.Country;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CountryModel> Countries { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}