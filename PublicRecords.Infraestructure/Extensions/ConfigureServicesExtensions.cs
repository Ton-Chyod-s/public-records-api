using PublicRecords.Domain.Interface.DatabaseAccessor.Base;
using PublicRecords.Domain.Interface.Services.OfficialElectronicDiary;
using PublicRecords.Domain.Interface.Services.OfficialStateDiary;
using PublicRecords.Domain.Interface.Services.SendEmail;
using PublicRecords.Domain.Interface.Services.Token;
using PublicRecords.Infraestructure.DatabaseAccessor.Base;
using PublicRecords.Infraestructure.Services.OfficialElectronicDiary;
using PublicRecords.Infraestructure.Services.OfficialStateDiary;
using PublicRecords.Infraestructure.Services.SendEmail;
using PublicRecords.Infraestructure.Services.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PublicRecords.Infraestructure.Extensions
{
    public static class ConfigureServicesExtensions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region [Services]
            services.AddTransient<IOfficialMunicipalDiaryService, OfficialMunicipalDiaryService>();
            services.AddTransient<IOfficialStateDiaryService, OfficialStateDiaryService>();
            services.AddTransient<ISendEmailService, SendEmailService>();
            services.AddTransient<ITokenService, TokenService>();
            #endregion

            services.AddScoped<INpgsqlService, NpgsqlService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("OfficialDiaryDb");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'OfficialDiaryDb' not found.");
                }

                return new NpgsqlService(connectionString);
            });

            return services;
        }
    }
}
