using System.Net.Mail;
using Microsoft.AspNetCore.Http;

namespace PublicRecords.Domain.Errors.SendEmail
{
    public record class UnexpectedMailError(Exception ExceptionDetails)
    : BaseError($"Ocorreu um erro inesperado. Tente novamente mais tarde.\nerror:{ExceptionDetails}", nameof(UnexpectedMailError), StatusCodes.Status500InternalServerError);
    public record class SendEmailError(SmtpException ExceptionDetails)
        : BaseError($"Erro ao enviar o e-mail!\nerror:{ExceptionDetails}", nameof(SendEmailError), StatusCodes.Status500InternalServerError);
    public record class InvalidEmail()
        : BaseError("O e-mail informado é inválido!", nameof(InvalidEmail), StatusCodes.Status400BadRequest);
    public record class MissingParameters()
        : BaseError("Algumas variáveis obrigatórias estão ausentes no arquivo .env", nameof(MissingParameters), StatusCodes.Status400BadRequest);
}
