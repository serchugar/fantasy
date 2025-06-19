using Fantasy.Backend.Services.Countries;

namespace Fantasy.Backend.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<CountriesRepository>();
        return services;
    }
}