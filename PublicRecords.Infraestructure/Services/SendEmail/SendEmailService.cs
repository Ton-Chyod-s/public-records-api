using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using OneOf;
using PublicRecords.Domain.DTOs.SendEmail;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.SendEmail;
using PublicRecords.Domain.Interface.Services.SendEmail;

namespace PublicRecords.Infraestructure.Services.SendEmail
{
    public class SendEmailService(IConfiguration configuration) : ISendEmailService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<OneOf<bool, BaseError>> SendAsyncEmail(RequestSendEmailDTO requestSendEmailDTO)
        {
            var SMTP_SERVER = _configuration["E-mail:SMTP_SERVER"] ?? string.Empty;
            var SMTP_PORT = _configuration["E-mail:SMTP_PORT"] ?? string.Empty;
            var EMAIL = _configuration["E-mail:EMAIL"] ?? string.Empty;
            var EMAIL_PASSWORD = _configuration["E-mail:EMAIL_PASSWORD"] ?? string.Empty;

            if (string.IsNullOrEmpty(requestSendEmailDTO.From))
                return new InvalidEmail();

            var body = CreateMailMessageBody.GenerateEmailHtmlTemplate(requestSendEmailDTO.Subject, requestSendEmailDTO.Body);

            var smtpClient = CreateSmtpClient(SMTP_SERVER, int.Parse(SMTP_PORT), EMAIL, EMAIL_PASSWORD);
           
            var mailMessage = CreateMailMessage(EMAIL, requestSendEmailDTO.Subject, body);

            mailMessage.To.Add(requestSendEmailDTO.From);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);

                return true;
            }
            catch (SmtpException ex)
            {
                return new SendEmailError(ex);
            }
            catch (Exception ex)
            {
                return new UnexpectedMailError(ex);
            }

        }

        internal SmtpClient CreateSmtpClient(string smtpServer, int smtpPort, string email, string password)
        {
            return new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true // Habilitar SSL 465 para SSL
            };
        }

        internal MailMessage CreateMailMessage(string email, string subject, string body)
        {
            return new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true // Altere para 'false' se o corpo não for HTML
            };
        }

    }
}
