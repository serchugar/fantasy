using Fantasy.Shared.Entities.Team;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fantasy.Backend.Data.Configurations;

public class TeamConfiguration : IEntityTypeConfiguration<TeamModel>
{
    public void Configure(EntityTypeBuilder<TeamModel> builder)
    {
        builder.HasIndex(t => new {t.Name, t.CountryId}).IsUnique();
        
        builder.Navigation(t => t.Country).AutoInclude();
    }
}