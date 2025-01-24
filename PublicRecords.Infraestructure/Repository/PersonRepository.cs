using PublicRecords.Domain.DTOs.Person;
using PublicRecords.Domain.Entities.OfficialStateDiary;
using PublicRecords.Domain.Entities.Person;
using PublicRecords.Domain.Entities.Session;
using PublicRecords.Domain.Enums.OfficialStateDiaries;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.OfficialStateDiary;
using PublicRecords.Domain.Errors.Person;
using PublicRecords.Domain.Errors.Session;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace PublicRecords.Infraestructure.Repository
{
    internal class PersonRepository(OfficialDiaryDbContext context) : BaseRepository<Person>(context), IPersonRepository
    {
        public async Task<OneOf<bool, BaseError>> AddPerson(string name, string email, long userId)
        {
            var person = new Person(name, email, userId);
            await _context.Person.AddAsync(person);

            return await _context.SaveChangesAsync() < 0 ? new PersonNotSaved() : true;
        }

        public async Task<OneOf<Person?, BaseError>> GetPersonById(long userId)
        {
            return await _context.Person
                .FirstOrDefaultAsync(p => p.UserId.Equals(userId));
        }

        public async Task<OneOf<bool, BaseError>> UpdatePerson(string name, string email, long userId)
        {
            var person = await _context.Person
                .FirstOrDefaultAsync(p => p.Name.Contains(name));

            if (person == null)
                return new PersonNotFound();
            
                person.UpdatePerson(name, email);
                _context.Person.Update(person);

            return await _context.SaveChangesAsync() < 0 ? true : new PersonNotSaved();
        }

        public async Task<OneOf<long, BaseError>> AddSession(long personId, string year)
        {
            var session = await _context.Sessions
                .FirstOrDefaultAsync(s => s.PersonID == personId && s.Year == year);

            if (session is null)
            {
                session = new Session(personId, year);
                await _context.Sessions.AddAsync(session);

                if (await _context.SaveChangesAsync() > 0)
                    return session.Id;
            }

            if (await _context.SaveChangesAsync() < 0)
                return new SessionErrors();

            return session.Id;
        }

        public async Task<OneOf<bool, BaseError>> addOfficialDiary(List<Dictionary<string, string>> responseOfficialMunicipalDiaryDTO)
        {
            foreach (var item in responseOfficialMunicipalDiaryDTO)
            {
                var newOfficialDiary = new OfficialDiaries(
                    item["Number"],
                    item["Day"],
                    item["File"],
                    item["Description"],
                    int.Parse(item["SessionId"]),
                    int.Parse(item["PersonId"]),
                    Enum.Parse<TypeDiaryEnum>(item["Type"])
                    );
                await _context.OfficialDiaries.AddAsync(newOfficialDiary);
            }

            if (await _context.SaveChangesAsync() < 0)
                return new OfficialDiaryNotSaved();

            return true;
        }

        public async Task<OneOf<bool?, BaseError>> RemovePerson(long personId)
        {
            var person = await _context.Person
                .FirstOrDefaultAsync(p => p.Id == personId);

            if (person is null)
            {
                return new PersonNotDeleted();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ResponsePersonDTO> GetPersonDTOAsync(string name)
        {
            var person = await _context.Person
                .FirstOrDefaultAsync(p => p.Name.Contains(name.FirstCharToUpper()));

            if (person is null)
            {
                return new ResponsePersonDTO(0, string.Empty, string.Empty);
            }

            return new ResponsePersonDTO(person.Id, person.Name, person.Email);
        }

        public async Task<bool> UpdateAuthorized(long userId)
        {
            var person = await _context.Person
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (person is not null)
            {
                person.AuthorizePerson(false);
                _context.Person.Update(person);
            }

            if (await _context.SaveChangesAsync() < 0)
                return false;

            return true;
        }

    }
}
