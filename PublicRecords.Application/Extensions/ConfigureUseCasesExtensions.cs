using PublicRecords.Application.UseCases.Login;
using PublicRecords.Application.UseCases.OfficialElectronicDiary;
using PublicRecords.Application.UseCases.OfficialStateDiary;
using PublicRecords.Application.UseCases.Person;
using PublicRecords.Application.UseCases.SaveAndNotify;
using PublicRecords.Application.UseCases.SendEmail;
using PublicRecords.Domain.Interface.UseCases.Login;
using PublicRecords.Domain.Interface.UseCases.OfficialElectronicDiary;
using PublicRecords.Domain.Interface.UseCases.OfficialStateDiary;
using PublicRecords.Domain.Interface.UseCases.Person;
using PublicRecords.Domain.Interface.UseCases.SaveAndNotify;
using PublicRecords.Domain.Interface.UseCases.SendEmail;
using Microsoft.Extensions.DependencyInjection;

namespace PublicRecords.Application.Extensions
{
    public static class ConfigureUseCasesExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<IOfficialMunicipalDiaryUseCase, OfficialMunicipalDiaryUseCase>();
            services.AddScoped<IOfficialStateDiaryUseCase, OfficialStateDiaryUseCase>();
            services.AddScoped<ISendEmailUseCase, SendEmailUseCase>();
            services.AddScoped<IPersonUseCase, AddOrUpdatePerson>();
            services.AddScoped<IRemovePersonUseCase, RemovePersonUseCase>();
            services.AddScoped<ISaveAndNotifyUseCase, SaveAndNotifyUseCase>();
            services.AddScoped<ILoginUseCase, LoginUseCase>();
            services.AddScoped<ICreateLoginUseCase, CreateLoginUseCase>();
            services.AddScoped<IUpdateLoginUseCase, UpdateLoginUseCase>();
            services.AddScoped<IDeleteLoginUseCase, DeleteLoginUseCase>();
            services.AddScoped<IUpdateAuthPersonUseCase, UpdateAuthPersonUseCase>();

            return services;
        }
    }
}
