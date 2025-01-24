using PublicRecords.Domain.DTOs.SendEmail;
using PublicRecords.Domain.Enums.User;
using PublicRecords.Domain.Errors;
using PublicRecords.Domain.Interface.UseCases.SendEmail;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace PublicRecords.API.Endpoints
{
    public static class MailEndpoints
    {
        public static WebApplication MapMailEndpoints(this WebApplication app)
        {
            var root = app.MapGroup("/mail")
                .WithTags("Mail")
                .WithDescription("This group contains endpoints for managing email-related operations.")
                .WithOpenApi();

            root.MapPost("/send", async ([FromServices] ISendEmailUseCase sendEmailUseCase, [FromBody] RequestSendEmailDTO requestSendEmailDTO) =>
            {
                var result = await sendEmailUseCase.SendAsyncEmail(requestSendEmailDTO);

                return result.Match(
                    response => Results.Ok(response),
                    error => Results.Json(error, statusCode: error.HttpErrorCode));
            })
           .WithName("Send e-mail")
           .RequireAuthorization(policy => policy.RequireRole(UserEnum.Admin.ToString()))
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status200OK)
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status404NotFound)
           .Produces<OneOf<bool, BaseError>>(StatusCodes.Status500InternalServerError);


            return app;
        }
    }
}
