
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Timesheet.Client.Services.Implementations;
using Timesheet.Client.Services.Interfaces;

namespace Timesheet.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSingleton(sp => new System.Net.Http.HttpClient { BaseAddress = new Uri("http://localhost:5010") });

            builder.Services.AddTelerikBlazor();

            // DI Container
            builder.Services.AddSingleton<IComponentComunicationService, ComponentComunicationService>();
            builder.Services.AddTransient<BaseService>();
            builder.Services.AddTransient<IActivityService, ActivityService>();
            builder.Services.AddTransient<IClientService, ClientService>();
            builder.Services.AddTransient<IProjectService, ProjectService>();
            builder.Services.AddTransient<IGeneralService, GeneralService>();
            builder.Services.AddTransient<ITimeOffService, TimeOffService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<ITimesheetDataService, TimesheetDataService>();
            builder.Services.AddTransient<IUserActivityCodeService, UserActivityCodeService>();
            builder.Services.AddTransient<IDepartmentService, DepartmentService>();
            builder.Services.AddTransient<IEmployeeTypeService, EmployeeTypeService>();
            builder.Services.AddTransient<IApprovalService, ApprovalService>();
            builder.Services.AddTransient<IProjectTeamUserService, ProjectTeamUserService>();
            builder.Services.AddTransient<IParentDepartmentService, ParentDepartmentService>();
            builder.Services.AddTransient<ICostCenterService, CostCenterService>();
            builder.Services.AddTransient<ITimesheetTypeService, TimesheetTypeService>();
            builder.Services.AddTransient<ITimesheetControlService, TimesheetControlService>();

            await builder.Build().RunAsync();
        }
    }
}

