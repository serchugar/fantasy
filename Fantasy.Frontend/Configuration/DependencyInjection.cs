using Fantasy.Frontend.Repositories.Countries;
using Fantasy.Frontend.Repositories.Teams;

namespace Fantasy.Frontend.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<CountriesRepository>();
        services.AddScoped<TeamsRepository>();
        return services;
    }
}