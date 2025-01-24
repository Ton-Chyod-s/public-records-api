using PublicRecords.Domain.DTOs.SendEmail;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Interface.Services.SendEmail;
using PublicRecords.Domain.Interface.UseCases.SendEmail;
using OneOf;

namespace PublicRecords.Application.UseCases.SendEmail
{
    internal class SendEmailUseCase
        (
            ISendEmailService sendEmailService
        ) : ISendEmailUseCase
    {
        private readonly ISendEmailService _sendEmailService = sendEmailService;   

        public async Task<OneOf<bool, BaseError>> SendAsyncEmail(RequestSendEmailDTO requestSendEmailDTO) => 
            await _sendEmailService.SendAsyncEmail(requestSendEmailDTO);
    }
}
