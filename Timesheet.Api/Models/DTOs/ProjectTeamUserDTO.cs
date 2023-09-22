namespace Timesheet.Api.Models.DTOs
{
    public class ProjectTeamUserDTO
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
