using Business.Abstract;
using Core.Utilities.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IResult SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var host = smtpSettings["Host"];
                var port = int.Parse(smtpSettings["Port"]);
                var email = smtpSettings["Email"];
                var password = smtpSettings["Password"];

                using (var client = new SmtpClient(host, port))
                {
                    client.Credentials = new NetworkCredential(email, password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(email, "YalınNews"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(toEmail);

                    client.Send(mailMessage);
                }

                return new SuccessResult("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"E-posta gönderilirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<IResult> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("SmtpSettings");
                var host = smtpSettings["Host"];
                var port = int.Parse(smtpSettings["Port"]);
                var email = smtpSettings["Email"];
                var password = smtpSettings["Password"];

                using (var client = new SmtpClient(host, port))
                {
                    client.Credentials = new NetworkCredential(email, password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(email, "YalınNews"),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                }

                return new SuccessResult("E-posta başarıyla gönderildi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult($"E-posta gönderilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
