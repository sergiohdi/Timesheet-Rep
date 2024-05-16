using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Timesheet.Api.Extensions.APIConfiguration;

namespace Timesheet.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseConfiguration(Configuration);
        services.AddOptionsConfigurations(Configuration);
        services.SetJWTConfigurations(Configuration);
        services.AddSwaggerConfiguration();
        services.AddCorsConfiguration(Configuration);
        services.AddAutoMapper(typeof(Startup));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.ConfigureDIContainer();
        services.AddHttpContextAccessor();
        services.AddControllers(opt =>
        {
            opt.Filters.Add(new AuthorizeFilter());
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // app.UseHttpsRedirection();

        app.UseCors(Configuration["CorsPolicy"]);

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "TimeSheet v1");
        });

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
