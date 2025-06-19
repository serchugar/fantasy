using Fantasy.Backend.Configuration.AppSettings;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Fantasy.Backend.Configuration;

public static class Swagger
{
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        ServiceConfig config = services.BuildServiceProvider().GetRequiredService<IOptions<ServiceConfig>>().Value;
        
        services.AddSwaggerGen(opts =>
        {
            opts.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Fantasy App",
                Version = $"v{config.AppVersion}"
            });
        });
        return services;
    }

    public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opts =>
        {
            opts.SwaggerEndpoint("/swagger/v1/swagger.json", "Fantasy App API v1.0");
            opts.RoutePrefix = string.Empty;
        });
        return app;
    }
}