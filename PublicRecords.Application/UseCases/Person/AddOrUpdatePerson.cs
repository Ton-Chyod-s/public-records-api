using PublicRecords.Domain.DTOs.Person;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Errors.Common;
using PublicRecords.Domain.Errors.Login;
using PublicRecords.Domain.Errors.Person;
using PublicRecords.Domain.Errors.SendEmail;
using PublicRecords.Domain.Extensions;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Domain.Interface.UseCases.Person;
using Microsoft.AspNetCore.Http;
using OneOf;

namespace PublicRecords.Application.UseCases.Person
{
    internal class AddOrUpdatePerson : IPersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddOrUpdatePerson(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<OneOf<bool, BaseError>> ExecuteAddOrUpdatePerson(PersonDTO personDto)
        {
            var validatePersonDto = ValidatePersonDto(personDto);

            if (validatePersonDto.IsError()) return validatePersonDto.GetError();

            var validateAndProcessPersonAsync = await ValidateAndProcessPersonAsync(personDto);

            if (validateAndProcessPersonAsync.IsError()) return validateAndProcessPersonAsync.GetError();    

            return true;
        }

        public async Task<OneOf<bool, BaseError>> ValidateAndProcessPersonAsync(PersonDTO personDto)
        {
            var roleName = _httpContextAccessor.HttpContext?.GetTokenIssuerFromHttpContext();

            if (roleName == "Admin")
            {
                var resultValidate = await ValidateAndAddOrUpdatePerson(personDto);

                if (resultValidate.IsError()) return resultValidate.GetError();

            }
            else
            {
                var userId = _httpContextAccessor.HttpContext?.GetUserIdByContext();

                if (string.IsNullOrEmpty(userId)) return new UnauthorizedAccess();

                if (int.TryParse(userId, out int parsedUserId))
                {
                    var user = await _unitOfWork.PersonRepository.GetPersonById(parsedUserId);

                    var userValue = user.GetValue();

                    if (userValue is not null) return new UnauthorizedAccess();

                    var resultValidate = await ValidateAndAddOrUpdatePerson(personDto);

                    if (resultValidate.IsError()) return resultValidate.GetError();
                }
            }

            return true;
        }

        private async Task<OneOf<bool, BaseError>> ValidateAndAddOrUpdatePerson(PersonDTO personDto)
        {
            var Name = personDto.validatedPersonName.EnsureValidName();
            var validatedName = Name.GetValue();

            var userName = _httpContextAccessor.HttpContext?.GetSystemIdentifierIdByContext();

            if (string.IsNullOrWhiteSpace(userName))
                return new UserNotFound();

            var userResult = await _unitOfWork.UserRepository.GetUserByName(userName);
            if (userResult == null)
                return new UserNotFound();

            var personResult = await _unitOfWork.PersonRepository.AddPerson(
                validatedName.TextToTitleCase(),
                personDto.Email.ToLowerInvariant(),
                userResult.Id
            );

            if (personResult.IsError())
                return personResult.GetError();

            return true;
        }

        private OneOf<bool, BaseError> ValidatePersonDto(PersonDTO personDto)
        {
            if (string.IsNullOrWhiteSpace(personDto.validatedPersonName) || personDto.validatedPersonName.Length < 3)
                return new InvalidName();

            if (string.IsNullOrWhiteSpace(personDto.Email) || !personDto.Email.IsValidEmail())
                return new InvalidEmail();

            return true;
        }

    }
}
