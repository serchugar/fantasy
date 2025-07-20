using Fantasy.Backend.Services.Countries;
using Fantasy.Backend.Services.Teams;

namespace Fantasy.Backend.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<CountriesRepository>();
        services.AddScoped<TeamsRepository>();
        return services;
    }
}