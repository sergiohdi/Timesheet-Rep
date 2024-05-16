using System;
using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models;

public class Project : ProjectLight
{
    public string Description { get; set; }
    public bool? TimeEntryAllowed { get; set; }
    [Required(ErrorMessage = "Start Date is required")]
    public DateTime? EntryStartDate { get; set; } = DateTime.Now;
    public DateTime? EntryEndDate { get; set; }
    public bool? ClosedStatus { get; set; }=false;
    public string UdfGroups { get; set; }
    public string UdfPlpCostCenter { get; set; }
    public string UdfProjectType { get; set; }
    public string UdfAudience { get; set; }
    public int? ProjectLeaderId { get; set; }
    public string ClientName { get; set; }
}
