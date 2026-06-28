using Core.Utilities.Results;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmailService
    {
        IResult SendEmail(string toEmail, string subject, string body);
        Task<IResult> SendEmailAsync(string toEmail, string subject, string body);
    }
}
