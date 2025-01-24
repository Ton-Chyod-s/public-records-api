using Microsoft.AspNetCore.Http;

namespace PublicRecords.Domain.Errors.Login
{
    public record UserNotSaved() : BaseError("Usuário não foi salvo.", nameof(UserNotSaved), StatusCodes.Status400BadRequest);
    public record TokenNotSaved() : BaseError("Token não foi salvo.", nameof(TokenNotSaved), StatusCodes.Status400BadRequest);
    public record UserNotFound() : BaseError("Usuário não foi encontrado.", nameof(UserNotFound), StatusCodes.Status404NotFound);
    public record UserNotUpdate() : BaseError("Usuário não foi atualizado.", nameof(UserNotUpdate), StatusCodes.Status501NotImplemented);
    public record UserNotDeleted() : BaseError("Usuário não foi deletado.", nameof(UserNotDeleted), StatusCodes.Status400BadRequest);

}
