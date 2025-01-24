using PublicRecords.Domain.DTOs.Person;
using PublicRecords.Domain.Enums.User;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Interface.UseCases.Person;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace PublicRecords.API.Endpoints
{
    public static class PersonEndpoints
    {
        public static WebApplication MapPersonEndpoints(this WebApplication app)
        {
            var root = app.MapGroup("/person")
                .WithTags("Person")
                .WithDescription("This group contains endpoints for managing person-related operations, including creation, updates, and retrieval of person details.")
                .WithOpenApi();

            root.MapPost("", async ([FromServices] IPersonUseCase personUseCase, [FromBody] PersonDTO personEnum) =>
            {
                var result = await personUseCase.ExecuteAddOrUpdatePerson(personEnum);

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
           .WithName("Person")
           .RequireAuthorization(policy => policy.RequireRole(UserEnum.User.ToString(), UserEnum.Admin.ToString()))
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status200OK)
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status404NotFound)
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status500InternalServerError);

            root.MapDelete("/delete", async ([FromServices] IRemovePersonUseCase removePersonUseCase, [FromQuery] long personId) =>
            {
                var result = await removePersonUseCase.RemovePerson(personId);

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
           .WithName("RemovePerson")
           .RequireAuthorization(policy => policy.RequireRole(UserEnum.User.ToString()))
           .Produces<OneOf<long, BaseError>>(StatusCodes.Status200OK)
           .Produces<OneOf<long, BaseError>>(StatusCodes.Status404NotFound)
           .Produces<OneOf<long, BaseError>>(StatusCodes.Status500InternalServerError);

            root.MapPost("/unauthorized", async ([FromServices] IUpdateAuthPersonUseCase updateAuthPersonUseCase) =>
            {
                var result = await updateAuthPersonUseCase.UpdateAuthPerson();

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
           .WithName("AuthorizedPerson")
           .RequireAuthorization(policy => policy.RequireRole(UserEnum.User.ToString(), UserEnum.Admin.ToString()))
           .Produces<OneOf<long, BaseError>>(StatusCodes.Status200OK)
           .Produces<OneOf<long, BaseError>>(StatusCodes.Status404NotFound)
           .Produces<OneOf<long, BaseError>>(StatusCodes.Status500InternalServerError);

            return app;
        }
    }
}
