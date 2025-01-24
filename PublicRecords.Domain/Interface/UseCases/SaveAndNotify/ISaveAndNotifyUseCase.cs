using PublicRecords.Domain.Errors;
using OneOf;

namespace PublicRecords.Domain.Interface.UseCases.SaveAndNotify
{
    public interface ISaveAndNotifyUseCase
    {
        Task<OneOf<bool, BaseError>> SaveAndNotify();
    }
}
