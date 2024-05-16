using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models;

public class Department
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    public string Code { get; set; }
    public string Comments { get; set; }
    public bool? DisabledSetting { get; set; } = false;
    public string CostCenterGroup { get; set; }
    public int? ParentId { get; set; }

}
