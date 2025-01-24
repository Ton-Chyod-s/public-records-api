using PublicRecords.Domain.Entities.User;
using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Infraestructure.Context;

namespace PublicRecords.Infraestructure.Repository
{
    internal class AddOrUpdateLoginRepository(OfficialDiaryDbContext context) : BaseRepository<User>(context), IAddOrUpdateLoginRepository 
    {
        
    }

}
