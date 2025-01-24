using PublicRecords.Domain.DTOs.OfficialStateDiary;
using PublicRecords.Domain.Enums.User;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Interface.UseCases.OfficialElectronicDiary;
using PublicRecords.Domain.Interface.UseCases.OfficialStateDiary;
using PublicRecords.Domain.Interface.UseCases.SaveAndNotify;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace PublicRecords.API.Endpoints
{
    public static class OfficialDiaryEndpoints
    {
        public static WebApplication MapOfficialDiaryEndpoints(this WebApplication app)
        {
            var root = app.MapGroup("/diary")
                .WithTags("Diary")
                .WithDescription("Endpoints related to the Official Diary!!!")
                .WithOpenApi();


           root.MapGet("/official-state-diary", async ([FromServices] IOfficialStateDiaryUseCase officialElectronicDiaryUseCase, [FromQuery] string name, [FromQuery] string year) =>
            {
                var result = await officialElectronicDiaryUseCase.GetOfficialStateDiary(name, year);

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
            .WithName("Official State Diary")
            .RequireAuthorization(policy => policy.RequireRole(UserEnum.User.ToString(), UserEnum.Admin.ToString()))
            .Produces<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>>(StatusCodes.Status200OK)
            .Produces<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>>(StatusCodes.Status404NotFound)
            .Produces<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>>(StatusCodes.Status500InternalServerError);
          
            root.MapGet("/official-municipal-diary", async ([FromServices] IOfficialMunicipalDiaryUseCase officialStateDiary, [FromQuery] string name, [FromQuery] string year) =>
            {
                var result = await officialStateDiary.GetOfficialMunicipalDiary(name, year);

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
            .WithName("Official Municipal Diary")
            .RequireAuthorization(policy => policy.RequireRole(UserEnum.User.ToString(), UserEnum.Admin.ToString()))
            .Produces<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>>(StatusCodes.Status200OK)
            .Produces<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>>(StatusCodes.Status404NotFound)
            .Produces<OneOf<List<ResponseOfficialDiaryDTO>, BaseError>>(StatusCodes.Status500InternalServerError);

            root.MapPost("/save-and-notify", async ([FromServices] ISaveAndNotifyUseCase saveAndNotifyUseCase) =>
            {
                var result = await saveAndNotifyUseCase.SaveAndNotify();

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
            .WithName("Save And Notify")
            .Produces<OneOf<bool, BaseError>>(StatusCodes.Status200OK)
            .Produces<OneOf<bool, BaseError>>(StatusCodes.Status404NotFound)
            .Produces<OneOf<bool, BaseError>>(StatusCodes.Status500InternalServerError);

            return app;
        }
    }
}
