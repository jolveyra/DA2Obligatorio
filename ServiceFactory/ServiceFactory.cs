using BusinessLogic;
using LogicInterfaces;
using RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography.X509Certificates;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace ServiceFactory
{
    public static class ServiceFactory
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IInvitationLogic, InvitationLogic>();
            services.AddScoped<IAuthorizationLogic, UserLogic>();
            services.AddScoped<IAdministratorLogic, UserLogic>();
            services.AddScoped<IMaintenanceEmployeeLogic, UserLogic>();

            //services.AddScoped<IInvitationRepository, InvitationRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();

        }

        public static void AddConnectionString(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<DbContext, BuildingsContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
