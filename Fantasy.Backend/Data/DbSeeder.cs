using Fantasy.Shared.Entities.Team;
using Microsoft.EntityFrameworkCore;

namespace Fantasy.Backend.Data;

public class DbSeeder(AppDbContext context)
{
    public async Task SeedAsync()
    {
        await context.Database.EnsureCreatedAsync();
        await PopulateCountriesAsync();
        await PopulateTeamsAsync();
    }

    private async Task PopulateCountriesAsync()
    {
        if (context.Countries.Any()) return;
        
        string countriesSqlScript = await File.ReadAllTextAsync("Data/SqlScripts/Countries.sql");
        await context.Database.ExecuteSqlRawAsync(countriesSqlScript);
    }

    private async Task PopulateTeamsAsync()
    {
        if (context.Teams.Any()) return;

        foreach (var country in context.Countries)
        {
            context.Teams.Add(new TeamModel {Name = country.Name, Country = country});           
            
            if (country.Name == "Spain")
            {
                context.Teams.Add(new TeamModel {Name = "Real Madrid", Country = country});
                context.Teams.Add(new TeamModel {Name = "Barcelona", Country = country});
                context.Teams.Add(new TeamModel {Name = "Valencia", Country = country});
                context.Teams.Add(new TeamModel {Name = "Atlético de Madrid", Country = country});
                context.Teams.Add(new TeamModel {Name = "Levante", Country = country});
                context.Teams.Add(new TeamModel {Name = "Betis", Country = country});
            }
            else if (country.Name == "England")
            {
                context.Teams.Add(new TeamModel {Name = "Manchester United", Country = country});
                context.Teams.Add(new TeamModel {Name = "Liverpool", Country = country});
            }
            else if (country.Name == "Germany")
            {
                context.Teams.Add(new TeamModel {Name = "Bayern Munich", Country = country});
                context.Teams.Add(new TeamModel {Name = "Borussia Dortmund", Country = country});
            }
            else if (country.Name == "Argentina")
            {
                context.Teams.Add(new TeamModel {Name = "Boca Juniors", Country = country});
                context.Teams.Add(new TeamModel {Name = "River Plate", Country = country});
            }
        }
        
        await context.SaveChangesAsync();
    }
}

public static class DbSeederExtensions
{
    public static async Task<IApplicationBuilder> UseDbSeederAsync(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        DbSeeder dbSeeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
        await dbSeeder.SeedAsync();

        return app;
    }
}