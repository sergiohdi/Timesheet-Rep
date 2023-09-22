using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models
{
    public class ClientLight
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MinLength(5, ErrorMessage = "Name must be at least 5 characters")]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
