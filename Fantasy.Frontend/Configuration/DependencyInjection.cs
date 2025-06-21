using Fantasy.Frontend.Repositories.Countries;

namespace Fantasy.Frontend.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<CountriesRepository>();
        return services;
    }
}