using Microsoft.Extensions.DependencyInjection;
using Timesheet.Client.Services.Implementations;
using Timesheet.Client.Services.Interfaces;

namespace Timesheet.Client.Extensions;

public static class ClientConfigurationExtensions
{
    public static IServiceCollection AddServiceDependencyInjection(this IServiceCollection services)
    {
        // Data services
        services.AddTransient<BaseDataService>();
        services.AddTransient<IActivityDataService, ActivityDataService>();
        services.AddTransient<IClientDataService, ClientDataService>();
        services.AddTransient<IProjectDataService, ProjectDataService>();
        services.AddTransient<IGeneralDataService, GeneralDataService>();
        services.AddTransient<ITimeOffDataService, TimeOffDataService>();
        services.AddTransient<IUserDataService, UserDataService>();
        services.AddTransient<ITimesheetDataService, TimesheetDataService>();
        services.AddTransient<IUserActivityCodeDataService, UserActivityCodeDataService>();
        services.AddTransient<IDepartmentDataService, DepartmentDataService>();
        services.AddTransient<IEmployeeTypeDataService, EmployeeTypeDataService>();
        services.AddTransient<IApprovalDataService, ApprovalDataService>();
        services.AddTransient<IProjectTeamUserDataService, ProjectTeamUserDataService>();
        services.AddTransient<IParentDepartmentDataService, ParentDepartmentDataService>();
        services.AddTransient<ICostCenterDataService, CostCenterDataService>();
        services.AddTransient<ITimesheetTypeDataService, TimesheetTypeDataService>();
        services.AddTransient<ITimesheetControlDataService, TimesheetControlDataService>();
        services.AddTransient<IApprovalHistoryService, ApprovalHistoryService>();

        // Business Services
        services.AddSingleton<ILocalStorageService, LocalStorageService>();
        services.AddScoped<IComponentComunicationService, ComponentComunicationService>();
        services.AddScoped<ITimesheetService, TimesheetService>();
        services.AddScoped<ITimesheetValidationsService, TimesheetValidationsService>();

        return services;
    }
}
