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
            services.AddScoped<IAuthorizationLogic, AuthorizationLogic>();
            services.AddScoped<IAdministratorLogic, AuthorizationLogic>();
            services.AddScoped<IMaintenanceEmployeeLogic, AuthorizationLogic>();
            //services.AddScoped<IInvitationRepository, InvitationRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
