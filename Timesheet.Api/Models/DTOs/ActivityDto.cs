namespace Timesheet.Api.Models.DTOs
{
    public class ActivityDto
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityCode { get; set; }
        public string Description { get; set; }
        
        public bool? Disabled { get; set; }
    }
}
