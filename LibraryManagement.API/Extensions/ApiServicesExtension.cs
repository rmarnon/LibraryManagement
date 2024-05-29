using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Repositories;

namespace LibraryManagement.API.Extensions
{
    public static class ApiServicesExtension
    {
        public static IServiceCollection AddApiIoC(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
