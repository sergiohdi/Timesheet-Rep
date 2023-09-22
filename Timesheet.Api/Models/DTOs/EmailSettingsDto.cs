namespace Timesheet.Api.Models.DTOs
{
    public class EmailSettingsDto
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
        public string Username { get; set; }
    }
}
