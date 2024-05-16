using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Timesheet.Api.Models.DTOs;
using Timesheet.Api.Repositories.Interfaces;

namespace Timesheet.Api.Repositories.EF_Implementations;

public class ProjectHasUserRepository : IProjectHasUserRepository
{
    private readonly TimesheetContext _db;

    public ProjectHasUserRepository(TimesheetContext db) => _db = db;

    public bool AddUsersToTeamProject(int projectId, IEnumerable<int> usersId)
    {
        StringBuilder query = new StringBuilder();
        query.Append("INSERT INTO ProjectHasUser (ProjectId, UserId) VALUES ");
        foreach (var userId in usersId)
        {
            query.Append($"({projectId}, {userId}),");
        }

        query.Remove(query.Length - 1, 1);

        _db.Database.ExecuteSqlRaw(query.ToString());
        return true;
    }

    public void DeleteTeamProjectUsers(int projectId) => 
        _db.ProjectHasUser.Where(x => x.ProjectId == projectId).ExecuteDelete();

    public IEnumerable<ProjectTeamUserDTO> GetUsersByProject(int projectId)
    {
        return _db.ProjectHasUser
            .Include(x => x.User)
            .Where(x => x.ProjectId == projectId)
            .Select(x => new ProjectTeamUserDTO 
            {
                ProjectId = x.ProjectId.Value,
                UserId = x.User.Id,
                UserName = x.User.LastName + ", " + x.User.FirstName
            })
            .ToList()
            .OrderBy(x => x.UserName);
    }
}
