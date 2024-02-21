using AutoMapper;
using System;
using System.Collections.Generic;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Shared.Utils;

namespace Timesheet.Api.Repositories.Mappers
{
    public class MapperProfile: Profile
    {
        private Dictionary<int, string> approvalHistoryStatus = new Dictionary<int, string>
        {
            { (int)ApprovalStatusOption.Waiting, "Submitted" },
            { (int)ApprovalStatusOption.Approved, "Approved" },
            { (int)ApprovalStatusOption.Rejected, "Rejected" },
            { (int)ApprovalStatusOption.SupervisorApproval, "Approved by supervisor" }
        };

        public MapperProfile()
        {
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap()
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => src.Projects));
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<ParentDepartment, ParentDepartmentDto>().ReverseMap();
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
            CreateMap<Activity, ActivityDto>().ReverseMap();
            CreateMap<ApprovalStatus, ApprovalStatusDto>().ReverseMap();
            CreateMap<EmployeeType, EmployeeTypeDto>().ReverseMap();
            CreateMap<TimesheetType, TimesheetTypeDto>().ReverseMap();           
            CreateMap<TimeOff, TimeOffDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CostCenter, CostCenterDto>().ReverseMap();
            CreateMap<General, GeneralDto>().ReverseMap();
            CreateMap<UserActivityCode, UserActivityCodeDto>().ReverseMap();
            CreateMap<UserSelectAct, UserSelectActDto>().ReverseMap();
            CreateMap<UserActivityCode, UserActivityCodeDto>().ReverseMap();
            CreateMap<ProjectHasUser, ProjectTeamUserDTO>().ReverseMap();
            CreateMap<Substitute, SubstituteDto>().ReverseMap();
            CreateMap<TimesheetData, TimesheetDataDto>()
                .ForMember(dest => dest.TimesheetId, opt => opt.MapFrom( src => src.TimesheetId))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Clientid))
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Projectid))
                .ForMember(dest => dest.ActivityId, opt => opt.MapFrom(src => src.ActivityId))
                .ForMember(dest => dest.TimesheetId, opt => opt.MapFrom(src => src.Tasktimeoffid))
                .ForMember(dest => dest.EntryDate, opt => opt.MapFrom(src => src.Entrydate))
                .ForMember(dest => dest.TotalHours, opt => opt.MapFrom(src => src.TotalHours))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.ApprovalStatus, opt => opt.MapFrom(src => src.Approvalstatus))
                .ForMember(dest => dest.Billable, opt => opt.MapFrom(src => src.Billable))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Ttinfo2))
                .ForMember(dest => dest.PONumber, opt => opt.MapFrom(src => src.Ttinfo3));
            CreateMap<Approvals, ApprovalDto>().ReverseMap();
            CreateMap<TimesheetControl, TimesheetControlDto>().ReverseMap();
            CreateMap<TimesheetControl, TimesheetControlApprovalDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.ApprovalStatus.Appstatusname))
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.ApprovalStatusId == 2));
            CreateMap<ApprovalHistory, GetApprovalHistoryDto>()
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => src.ActionDate))
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => approvalHistoryStatus[src.ActionType.Value]))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.TimesheetType, opt => opt.MapFrom(src => src.TimesheetType));
            CreateMap<CreateApprovalRequestDto, ApprovalHistory>()
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ActionType, opt => opt.MapFrom(src => src.ActionType))
                .ForMember(dest => dest.IdUser, opt => opt.MapFrom(src => src.IdUser))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.IdTimesheetControl, opt => opt.MapFrom(src => src.IdTimesheetControl))
                .ForMember(dest => dest.TimesheetType, opt => opt.MapFrom(src => src.TimesheetType))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));
        }
    }
}
