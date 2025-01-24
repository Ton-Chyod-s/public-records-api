using PublicRecords.Domain.Entities.Session;
using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Infraestructure.Context;

namespace PublicRecords.Infraestructure.Repository
{
    internal class SessionRepository(OfficialDiaryDbContext context) : BaseRepository<Session>(context), ISessionRepository
    {

    }
}
