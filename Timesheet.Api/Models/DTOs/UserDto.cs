using System;

namespace Timesheet.Api.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public bool? Disabled { get; set; }
        public bool? IsSampleUser { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? AccountExpiry { get; set; }
        public string Domain { get; set; }
        public string Email { get; set; }
        public string OfflineEmail { get; set; }
        public TimeSpan? CurrentHoursPerDay { get; set; }
        public double? BillingRate { get; set; }
        public string JobTitle { get; set; }
        public string ReportsTo { get; set; }
        public double? ConvertDaysToHours { get; set; }
        public double? PlpmarkUp { get; set; }
        public string PositionTitle { get; set; }
        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? SupervisorId { get; set; }
        public int? SubstituteUser { get; set; }
        public int? TimesheetTemplate { get; set; }
        public string Password { get; set; }
        public int? EmpTypeId { get; set; }
        public string Username 
        {
            get
            {
                return $"{this.LastName}, {this.FirstName}";
            }
        }
    }
}
