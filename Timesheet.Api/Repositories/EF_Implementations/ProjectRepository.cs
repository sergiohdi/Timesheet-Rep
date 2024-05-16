using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Timesheet.Api.Models;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Extensions;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class ProjectRepository : IProjectRepository
{
    private readonly TimesheetContext _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public ProjectRepository(TimesheetContext db, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public IEnumerable<ProjectDto> GetProjects(bool? isOpen)
    {
        IQueryable<Project> query = _db.Project.AsNoTracking()
                                                       .Include(x => x.Client)
                                                       .OrderBy(x => x.Name);

        if (isOpen != null)
        {
            query = query.Where(x => x.ClosedStatus == isOpen);
        }

        return _mapper.Map<IEnumerable<ProjectDto>>(query.ToList());
    }

    public IEnumerable<ProjectLightDto> GetProjectsForDropDown()
    {
        return _db.Project.AsNoTracking()
                          .OrderBy(x => x.Name)
                          .Where(x => x.ClosedStatus == false)
                          .Select(x => new ProjectLightDto
                          {
                              Id = x.Id,
                              Name = x.Name,
                              ProjectCode = x.ProjectCode,
                              ClientId = x.ClientId,
                              TimeExpenseEntryType = x.TimeExpenseEntryType
                          })
                          .ToList();
    }

    public ProjectDto GetProjectById(int projectId)
    {
        Project project = _db.Project.AsNoTracking().FirstOrDefault(x => x.Id == projectId);
        return _mapper.Map<ProjectDto>(project);
    }

    public bool CreateProject(ProjectDto project)
    {
        Project projectDb = _mapper.Map<Project>(project);
        projectDb.Client = null;
        projectDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

        _db.Add(projectDb);
        return _db.SaveChanges() > 0;
    }

    public bool UpdateProject(ProjectDto project)
    {
        Project projectDb = _mapper.Map<Project>(project);
        projectDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

        _db.Entry(projectDb).State = EntityState.Modified;
        return _db.SaveChanges() > 0;
    }

    public bool UpdateProjectState(ProjectDto project)
    {
        Project projectDb = _mapper.Map<Project>(project);
        projectDb.SetAudit(_httpContextAccessor.HttpContext, true, 1);

        _db.Entry(projectDb).State = EntityState.Modified;
        return _db.SaveChanges() > 0;
    }

    public bool ValidateProjectsByClientId(int clientId) => _db.Project.Any(x => x.ClientId == clientId);

    public bool DeleteProject(ProjectDto project)
    {
        Project ProjectDb = _mapper.Map<Project>(project);
        ProjectDb.Client = null;

        _db.Remove(ProjectDb);
        return _db.SaveChanges() > 0;
    }
}
