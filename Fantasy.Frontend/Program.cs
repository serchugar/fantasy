using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Fantasy.Frontend;
using Fantasy.Frontend.Configuration;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7114") });
builder.Services.AddDependencyInjection();
builder.Services.AddLocalization();
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();