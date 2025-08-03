using System.Text.Json.Serialization;
using Fantasy.Backend.Middleware;
using Fantasy.Backend.Configuration.AppSettings;
using Fantasy.Backend.Configuration;
using Fantasy.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Serchugar.Base.Backend;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>(optional:false, reloadOnChange:true);
#region Services

builder.Services.Configure<ServiceConfig>(builder.Configuration.GetSection(nameof(ServiceConfig)));
builder.Services.AddDependencyInjection();
builder.Services.AddDbContextPool<AppDbContext>(opts => opts.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection"),
    o => o.MigrationsHistoryTable("__EFMigrationHistory", "fantasy")));
builder.Services.AddCorsConfig();
builder.Services.AddControllers()
    .AddJsonOptions(opts => opts.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
if (builder.Environment.IsDevelopment()) builder.Services.AddSwaggerConfig();

#endregion

WebApplication app = builder.Build();
#region Middlewares

await app.UseDbSeederAsync();
if (app.Environment.IsDevelopment()) app.UseSwaggerConfig();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCustomExceptionHandler();
app.UseCorsConfig();

// Warmup EF so the real first query(the slow one) happens under the hood
using(var scope = app.Services.CreateScope())
{
    AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await context.Countries.AnyAsync();
}

#endregion

app.DiscoverControllerRoutes();
app.DiscoverKeyedEntities();

app.MapControllers();
app.Run();