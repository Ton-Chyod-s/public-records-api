using PublicRecords.Domain.Entities.OfficialStateDiary;
using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Infraestructure.Context;
using RestSharp;

namespace PublicRecords.Infraestructure.Repository
{
    internal class OfficialDiaryRepository(OfficialDiaryDbContext context) : BaseRepository<OfficialDiaries>(context), IOfficialDiariesRepository
    {
         
    }
}
