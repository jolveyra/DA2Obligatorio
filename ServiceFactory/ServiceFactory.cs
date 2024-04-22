using BusinessLogic;
using LogicInterfaces;
using RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
