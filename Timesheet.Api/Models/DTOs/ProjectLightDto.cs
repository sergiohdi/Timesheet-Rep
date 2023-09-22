namespace Timesheet.Api.Models.DTOs
{
    public class ProjectLightDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProjectCode { get; set; }
        public int? ClientId { get; set; }
        public string TimeExpenseEntryType { get; set; }
    }
}
