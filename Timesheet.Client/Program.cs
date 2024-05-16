using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Timesheet.Client.Extensions;
using Timesheet.Client.Utils.Auth;

namespace Timesheet.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.Services.AddSingleton(sp => 
        {
            var httpClient = new System.Net.Http.HttpClient
            {
                // Todo: move to appsettings file or env. variable
                BaseAddress = new Uri("http://localhost:5010")
            };

            return httpClient;
        });

        builder.Services.AddTelerikBlazor();
        builder.Services.AddServiceDependencyInjection(); // DI Container Configuration

        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

        await builder.Build().RunAsync();
    }
}

