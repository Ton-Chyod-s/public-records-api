using PublicRecords.Domain.Enums.OfficialStateDiaries;

namespace PublicRecords.Domain.DTOs.OfficialStateDiary
{
    public record ResponseOfficialDiaryDTO
        (
            string Number,
            string Day,
            string File,
            string Description,
            TypeDiaryEnum Type,
            long? PersonId,
            long? SessionId
        );
}
