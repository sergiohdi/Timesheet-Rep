using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models;

public class ProjectLight
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Project Code is required")]
    public string ProjectCode { get; set; }
    [Required(ErrorMessage = "Client is required")]
    public int? ClientId { get; set; }
    [Required(ErrorMessage = "Billable is required")]
    public string TimeExpenseEntryType { get; set; }
}
