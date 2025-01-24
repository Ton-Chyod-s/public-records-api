using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Infraestructure.Context;
using PublicRecords.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkClass = PublicRecords.Infraestructure.UnitOfWork.UnitOfWork;

namespace PublicRecords.Infraestructure.Extensions
{
    public static class ConfigureRepositoriesExtensions
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            #region [Database Context Setup OfficialDiaryDbContext]
            var connectionString = Environment.GetEnvironmentVariable("CONTEXT_DATA_SOURCE");

            if (!string.IsNullOrEmpty(connectionString))
                services.AddDbContext<OfficialDiaryDbContext>(options => options.UseNpgsql(connectionString));
            else
                services.AddDbContext<OfficialDiaryDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("OfficialDiaryDb")));
            #endregion

            #region [Dependency Injection Setup]
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWorkClass>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
            services.AddScoped<IAddOrUpdateLoginRepository, AddOrUpdateLoginRepository>();

            #endregion

            return services;
        }
    }
}
