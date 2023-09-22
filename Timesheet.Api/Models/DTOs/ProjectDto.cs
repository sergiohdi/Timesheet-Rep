using System;

namespace Timesheet.Api.Models.DTOs
{
    public class ProjectDto : ProjectLightDto
    {
        public string Description { get; set; }
        public bool? TimeEntryAllowed { get; set; }
        public DateTime? EntryStartDate { get; set; }
        public DateTime? EntryEndDate { get; set; }
        public bool? ClosedStatus { get; set; }
        public string UdfGroups { get; set; }
        public string UdfPlpCostCenter { get; set; }
        public string UdfProjectType { get; set; }
        public string UdfAudience { get; set; }
        public int? ProjectLeaderId { get; set; }
        public string ClientName { get; set; }
    }
}
