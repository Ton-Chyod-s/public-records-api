using Microsoft.AspNetCore.Http;

namespace PublicRecords.Domain.Errors.Common
{
    public record InvalidPayload(string Message)
            : BaseError(Message, nameof(InvalidPayload), StatusCodes.Status400BadRequest);

    public record InvalidEnteredInformations(Dictionary<string, string> ValidationErros)
            : BaseError("Alguns campos foram preenchidos de forma inconsistente!", nameof(InvalidEnteredInformations), StatusCodes.Status400BadRequest, ValidationErros);

    public record UnexpectedError(string Message)
            : BaseError(Message, nameof(UnexpectedError), StatusCodes.Status500InternalServerError);

    public record DatabaseError()
            : BaseError("Erro ao atualizar o banco de dados!", nameof(DatabaseError), StatusCodes.Status500InternalServerError);

    public record NotFoundError(string Message)
            : BaseError(Message, nameof(NotFoundError), StatusCodes.Status404NotFound);

    public record InvalitYear()
            : BaseError("Ano inválido!", nameof(InvalitYear), StatusCodes.Status400BadRequest);

    public record UnauthorizedAccess()
            : BaseError("Acesso não autorizado!", nameof(UnauthorizedAccess), StatusCodes.Status401Unauthorized);
}
