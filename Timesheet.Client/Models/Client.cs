using System.ComponentModel.DataAnnotations;

namespace Timesheet.Client.Models
{
    public class Client : ClientLight
    {
        public Client()
        {
            this.Disabled = false;
        }

        public string Comments { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string StateProvince { get; set; }

        public string ZipPostalCode { get; set; }

        public string Country { get; set; }

        public string Telephone { get; set; }

        public string Fax { get; set; }

        public bool? Disabled { get; set; }
        
        [Required]
        public string OriginalCode { get; set; }

        public string Group { get; set; }
        // public ICollection<Project> Projects { get; set; }
    }
}
