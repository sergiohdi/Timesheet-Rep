namespace Timesheet.Api.Business.Interfaces;

public interface IEmailBusiness
{
    void SendEmail(string toEmail, string subject, string message);
}
