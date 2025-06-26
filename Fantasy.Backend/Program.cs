using Fantasy.Backend.Middleware;
using Fantasy.Backend.Configuration.AppSettings;
using Fantasy.Backend.Configuration;
using Fantasy.Backend.Data;
using Fantasy.Shared.Entities.Country;
using Microsoft.EntityFrameworkCore;
using Serchugar.Base.Backend;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>(optional:false, reloadOnChange:true);
#region Services

builder.Services.Configure<ServiceConfig>(builder.Configuration.GetSection(nameof(ServiceConfig)));
builder.Services.AddDependencyInjection();
builder.Services.AddDbContextPool<AppDbContext>(opts => opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCorsConfig();
builder.Services.AddControllers();
if (builder.Environment.IsDevelopment()) builder.Services.AddSwaggerConfig();

#endregion

WebApplication app = builder.Build();
#region Middlewares

if (app.Environment.IsDevelopment()) app.UseSwaggerConfig();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCustomExceptionHandler();
app.UseCorsConfig();

#endregion

app.DiscoverControllerRoutes();
app.DiscoverKeyedEntities(typeof(CountryModel).Assembly);

app.MapControllers();
app.Run();