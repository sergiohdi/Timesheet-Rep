using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models
{
    public class User
    {
        public User()
        {
            this.Disabled = false;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "Employee ID is required")]
        [MinLength(6, ErrorMessage = "Employee ID must be at least 3 characters")]
        public string ExternalId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        [MinLength(3, ErrorMessage = "First Name must be at least 3 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [MinLength(3, ErrorMessage = "Last Name must be at least 3 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Login is required")]
        public string LoginName { get; set; }       
        public bool? Disabled { get; set; }
        public bool? IsSampleUser { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime? StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public DateTime? AccountExpiry { get; set; }
        public string Domain { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        public string OfflineEmail { get; set; }
        public TimeSpan? CurrentHoursPerDay { get; set; }
        public double? BillingRate { get; set; }
        [Required(ErrorMessage = "Job Title is required")]
        [MinLength(3, ErrorMessage = "Job Title must be at least 2 characters")]
        public string JobTitle { get; set; }
        public string ReportsTo { get; set; }
        public double? ConvertDaysToHours { get; set; }
        public double? PlpmarkUp { get; set; }
        public string PositionTitle { get; set; }
        [Required(ErrorMessage = "Select a Role")]
        public int? RoleId { get; set; }
        [Required(ErrorMessage = "Select a Department")]
        public int? DepartmentId { get; set; }
        [Required(ErrorMessage = "Select a Supervisor")]
        public int? SupervisorId { get; set; }

        public int? SubstituteUser { get; set; }

        [Required(ErrorMessage = "Select a Timesheet Template")]
        public int? TimesheetTemplate { get; set; }

        [Required(ErrorMessage = "Select an Employee Type")]
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
