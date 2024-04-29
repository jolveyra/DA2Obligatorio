using BusinessLogic;
using LogicInterfaces;
using RepositoryInterfaces;
using Microsoft.Extensions.DependencyInjection;
using DataAccess.Context;
using DataAccess.Repositories;
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
            services.AddScoped<IUserSettingsLogic, UserLogic>();
            services.AddScoped<IManagerRequestLogic, RequestLogic>();
            services.AddScoped<IEmployeeRequestLogic, RequestLogic>();
            services.AddScoped<IBuildingLogic, BuildingLogic>();
            services.AddScoped<ICategoryLogic, CategoryLogic>();
            services.AddScoped<IReportLogic, ReportLogic>();

            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public static void AddConnectionString(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<DbContext, BuildingBossContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
