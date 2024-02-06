using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        [Required(ErrorMessage = "Activity Name is required")]
        public string ActivityName { get; set; }
        [Required(ErrorMessage = "Activity Code is required")]
        public string ActivityCode { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public bool? Disabled { get; set; }=false;
    }
}
