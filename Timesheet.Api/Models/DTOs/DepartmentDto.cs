namespace Timesheet.Api.Models.DTOs;

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Comments { get; set; }
    public bool? DisabledSetting { get; set; }
    public string CostCenterGroup { get; set; }
    public int? ParentId { get; set; }
    
}
