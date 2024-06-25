using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Services;

namespace LibraryManagement.API.Extensions
{
    public static class ApiServicesExtension
    {
        public static IServiceCollection AddAuthIoc(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
