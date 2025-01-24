using Microsoft.AspNetCore.Http;

namespace PublicRecords.Domain.Errors.Person
{
    public record InvalidName() 
        : BaseError("O parâmetro 'name' não pode ser nulo ou vazio.", nameof(InvalidName), StatusCodes.Status400BadRequest);
    public record PersonNotSaved()
        : BaseError("Não foi possível salvar/atualizar o nome atual. Verifique os dados fornecidos.", nameof(PersonNotSaved), StatusCodes.Status400BadRequest);
    public record PersonNotSavedName()
        : BaseError("Não foi possível salvar o nome fornecido. Verifique os dados fornecidos. Nome/Sobrenome", nameof(PersonNotSavedName), StatusCodes.Status400BadRequest);
    public record PersonNotFound()
        : BaseError("Não foi possível encontrar a pessoa com o nome fornecido.", nameof(PersonNotFound), StatusCodes.Status404NotFound);
    public record PersonNotDeleted()
        : BaseError("Não foi possível encontrar/deletar a pessoa com o nome fornecido.", nameof(PersonNotDeleted), StatusCodes.Status400BadRequest);
}
