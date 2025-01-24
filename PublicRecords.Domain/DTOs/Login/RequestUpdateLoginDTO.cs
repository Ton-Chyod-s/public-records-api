using PublicRecords.Domain.Enums.User;

namespace PublicRecords.Domain.DTOs.Login
{
    public record RequestUpdateLoginDTO
        (
            string Name,
            UserEnum? Type
        );

}
