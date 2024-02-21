using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Timesheet.Api.Business.Implementations;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.EF_Implementations;
using Timesheet.Api.Repositories.Interfaces;
using Timesheet.Api.Repositories.Repositories.EF_Implementations;

namespace Timesheet.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<EmailSettingsDto>(Configuration.GetSection("EmailSettings"));

            services.AddDbContext<TimesheetContext>(conf => conf.UseSqlServer(Configuration.GetConnectionString("TimeSheetConn")));

            // services.AddAuthentication(IISDefaults.AuthenticationScheme);

            services.AddCors(options => options.AddPolicy("local", conf =>
                conf.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            ));

            services.AddControllers()
                .AddFluentValidation(conf =>
                {
                    conf.RegisterValidatorsFromAssemblyContaining<Startup>();
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "TimeSheet v1", Version = "v1" });
            });

            // Automapper setup
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Allow to get access to httpcontext from other layers
            services.AddHttpContextAccessor();

            // Repository DI Container 
            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<IApprovalStatusRepository, ApprovalStatusRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<ICostCenterRepository, CostCenterRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IParentDepartmentRepository, ParentDepartmentRepository>();
            services.AddTransient<IEmployeeTypeRepository, EmployeeTypeRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();          
            services.AddTransient<ITimeOffRepository, TimeOffRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserActivityCodeRepository, UserActivityCodeRepository>();
            services.AddTransient<IGeneralRepository, GeneralRepository>();
            services.AddTransient<IUserSelectActRepository, UserSelectActRepository>();
            services.AddTransient<ITimesheetDataRepository, TimesheetDataRepository>();
            services.AddTransient<IProjectHasUserRepository, ProjectHasUserRepository>();
            services.AddTransient<IApprovalRepository, ApprovalRepository>();
            services.AddTransient<ISubstituteRepository, SubstituteRepository>();
            services.AddTransient<ITimesheetTypeRepository, TimesheetTypeRepository>();
            services.AddTransient<ITimesheetControlRepository, TimesheetControlRepository>();
            services.AddTransient<IApprovalHistoryRepository, ApprovalHistoryRepository>();

            // Business DI Container 
            services.AddTransient<IActivityBusiness, ActivityBusiness>();
            services.AddTransient<IApprovalStatusBusiness, ApprovalStatusBusiness>();
            services.AddTransient<IClientBusiness, ClientBusiness>();
            services.AddTransient<ICostCenterBusiness, CostCenterBusiness>();
            services.AddTransient<IDepartmentBusiness, DepartmentBusiness>();
            services.AddTransient<IParentDepartmentBusiness, ParentDepartmentBusiness>();
            services.AddTransient<IEmployeeTypeBusiness, EmployeeTypeBusiness>();
            services.AddTransient<IProjectBusiness, ProjectBusiness>();           
            services.AddTransient<ITimeOffBusiness, TimeOffBusiness>();
            services.AddTransient<IUserBusiness, UserBusiness>();
            services.AddTransient<IUserActivityCodeBusiness, UserActivityCodeBusiness>();
            services.AddTransient<IGeneralBusiness, GeneralBusiness>();
            services.AddTransient<IUserSelectActBusiness, UserSelectActBusiness>();
            services.AddTransient<ITimesheetDataBusiness, TimesheetDataBusiness>();
            services.AddTransient<IProjectHasUserBusiness, ProjectHasUserBusiness>();
            services.AddTransient<IApprovalBusiness, ApprovalBusiness>();
            services.AddTransient<ISubstituteBusiness, SubstituteBusiness>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ITimesheetTypeBusiness, TimesheetTypeBusiness>();
            services.AddTransient<ITimesheetControlBusiness, TimesheetControlBusiness>();
            services.AddTransient<IApprovalHistoryBusiness, ApprovalHistoryBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            app.UseCors("local");

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "TimeSheet v1");
            });

            // app.UseAuthentication();

            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
