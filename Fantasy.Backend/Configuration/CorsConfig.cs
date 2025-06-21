namespace Fantasy.Backend.Configuration;

public static class CorsConfig
{
    
    public static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(opts =>
        {
            opts.AddPolicy("AllowBlazorClient", policy =>
                policy.WithOrigins("https://localhost:7061")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
        return services;
    }


    public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app)
    {
        app.UseCors("AllowBlazorClient");
        return app;
    }
}