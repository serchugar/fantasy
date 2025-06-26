using Fantasy.Shared.Entities.Country;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fantasy.Backend.Data.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<CountryModel>
{
    public void Configure(EntityTypeBuilder<CountryModel> builder)
    {
        builder.HasIndex(c => c.Name).IsUnique();
        
        builder.HasMany(c => c.Teams)
            .WithOne(t => t.Country)
            .HasForeignKey(t => t.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(c => c.Teams).AutoInclude();
    }
}