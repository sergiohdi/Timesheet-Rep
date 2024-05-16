namespace Timesheet.Api.Models.DTOs;

public class CostCenterDto
{
    public int Costcenterid { get; set; }
    public string Costcentername { get; set; }
    public bool? Disabled { get; set; }
}
