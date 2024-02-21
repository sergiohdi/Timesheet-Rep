
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
            builder.Services.AddSingleton(sp => 
                new System.Net.Http.HttpClient 
                {
                    BaseAddress = new Uri("http://localhost:5010") 
                }
            );

            builder.Services.AddTelerikBlazor();

            // DI Container
            // Data services
            builder.Services.AddTransient<BaseDataService>();
            builder.Services.AddTransient<IActivityDataService, ActivityDataService>();
            builder.Services.AddTransient<IClientDataService, ClientDataService>();
            builder.Services.AddTransient<IProjectDataService, ProjectDataService>();
            builder.Services.AddTransient<IGeneralDataService, GeneralDataService>();
            builder.Services.AddTransient<ITimeOffDataService, TimeOffDataService>();
            builder.Services.AddTransient<IUserDataService, UserDataService>();
            builder.Services.AddTransient<ITimesheetDataService, TimesheetDataService>();
            builder.Services.AddTransient<IUserActivityCodeDataService, UserActivityCodeDataService>();
            builder.Services.AddTransient<IDepartmentDataService, DepartmentDataService>();
            builder.Services.AddTransient<IEmployeeTypeDataService, EmployeeTypeDataService>();
            builder.Services.AddTransient<IApprovalDataService, ApprovalDataService>();
            builder.Services.AddTransient<IProjectTeamUserDataService, ProjectTeamUserDataService>();
            builder.Services.AddTransient<IParentDepartmentDataService, ParentDepartmentDataService>();
            builder.Services.AddTransient<ICostCenterDataService, CostCenterDataService>();
            builder.Services.AddTransient<ITimesheetTypeDataService, TimesheetTypeDataService>();
            builder.Services.AddTransient<ITimesheetControlDataService, TimesheetControlDataService>();
            builder.Services.AddTransient<IApprovalHistoryService, ApprovalHistoryService>();

            // Business Services
            builder.Services.AddScoped<IComponentComunicationService, ComponentComunicationService>();
            builder.Services.AddScoped<ITimesheetService, TimesheetService>();
            builder.Services.AddScoped<ITimesheetValidationsService, TimesheetValidationsService>();

            await builder.Build().RunAsync();
        }
    }
}

