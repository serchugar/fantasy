using Fantasy.Backend.Middleware;
using Fantasy.Backend.Configuration.AppSettings;
using Fantasy.Backend.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>(optional:false, reloadOnChange:true);
#region Services

builder.Services.AddControllers();
builder.Services.Configure<ServiceConfig>(builder.Configuration.GetSection(nameof(ServiceConfig)));
builder.Services.AddDependencyInjection();
builder.Services.AddControllers();
if (builder.Environment.IsDevelopment()) builder.Services.AddSwaggerConfig();

#endregion

WebApplication app = builder.Build();
#region Middlewares

if (builder.Environment.IsDevelopment()) app.UseSwaggerConfig();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCustomExceptionHandler();

#endregion

app.MapControllers();
app.Run();