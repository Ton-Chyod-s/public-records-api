using PublicRecords.Domain.DTOs.SendEmail;
using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.Services.SendEmail
{
    public interface ISendEmailService
    {
        Task<OneOf<bool, BaseError>> SendAsyncEmail(RequestSendEmailDTO requestSendEmailDTO);
    }
}
