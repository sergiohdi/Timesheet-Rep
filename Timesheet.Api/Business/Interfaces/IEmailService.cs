using System.Threading.Tasks;

namespace Timesheet.Api.Business.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string toEmail, string subject, string message);
    }
}
