using PublicRecords.Domain.DTOs.Login;
using PublicRecords.Domain.DTOs.Token;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Interface.UseCases.Login;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace PublicRecords.API.Endpoints
{
    public static class SessionEndpoints
    {
        public static WebApplication MapSessionEndpoints(this WebApplication app)
        {
            var root = app.MapGroup("/login")
                .WithTags("Login")
                .WithDescription("This endpoint allows users to authenticate themselves by providing valid credentials.")
                .WithOpenApi();

            root.MapPost("/", async ([FromServices] ILoginUseCase addOrUpdateLogin, [FromBody] ResquestAddOrLoginDTO resquestAddOrUpdateLoginDTO) =>
            {
                var result = await addOrUpdateLogin.LoginWithApp(resquestAddOrUpdateLoginDTO);

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
           .WithName("Login With App")
           .Produces<OneOf<ResponseTokenDTO, BaseError>>(StatusCodes.Status200OK)
           .Produces<OneOf<ResponseTokenDTO, BaseError>>(StatusCodes.Status404NotFound)
           .Produces<OneOf<ResponseTokenDTO, BaseError>>(StatusCodes.Status500InternalServerError);

            root.MapPost("/add", async ([FromServices] ICreateLoginUseCase createLoginUseCase, [FromBody] ResquestAddOrLoginDTO resquestAddOrUpdateLoginDTO) =>
            {
                var result = await createLoginUseCase.CreateLogin(resquestAddOrUpdateLoginDTO);

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
           .WithName("Create Login With App")
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status200OK)
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status500InternalServerError);

            root.MapPut("/update", async ([FromServices] IUpdateLoginUseCase updateLoginUseCase, [FromBody] RequestUpdateLoginDTO requestUpdateLoginDTO) =>
            {
                var result = await updateLoginUseCase.UpdateLogin(requestUpdateLoginDTO);

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
           .WithName("Update Login With App")
           .Produces<OneOf<ResponseTokenDTO, BaseError>>(StatusCodes.Status200OK)
           .Produces<OneOf<ResponseTokenDTO, BaseError>>(StatusCodes.Status500InternalServerError);

            root.MapDelete("/delete", async ([FromServices] IDeleteLoginUseCase deleteLoginUseCase) =>
            {
                var result = await deleteLoginUseCase.DeleteUser();

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
           .WithName("Delete Login With App")
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status200OK)
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status500InternalServerError);

            return app;
        }
    }
}
