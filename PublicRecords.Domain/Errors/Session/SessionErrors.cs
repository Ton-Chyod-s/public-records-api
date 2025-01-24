using Microsoft.AspNetCore.Http;

namespace PublicRecords.Domain.Errors.Session
{
    public record class SessionErrors() 
        : BaseError("Erro ao adicionar a sessão!", nameof(SessionErrors), StatusCodes.Status500InternalServerError);

}
