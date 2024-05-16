using Microsoft.Extensions.DependencyInjection;
using Timesheet.Api.Business.Implementations;
using Timesheet.Api.Business.Interfaces;
using Timesheet.Api.Repositories.EF_Implementations;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Extensions.APIConfiguration;

public static class DIContainerConfiguration
{
    public static IServiceCollection ConfigureDIContainer(this IServiceCollection services)
    {
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
        services.AddTransient<IEmailBusiness, EmailBusiness>();
        services.AddTransient<ITimesheetTypeBusiness, TimesheetTypeBusiness>();
        services.AddTransient<ITimesheetControlBusiness, TimesheetControlBusiness>();
        services.AddTransient<IApprovalHistoryBusiness, ApprovalHistoryBusiness>();
        services.AddTransient<ILoginBusiness, LoginBusiness>();

        return services;
    }

}
