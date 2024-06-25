using LibraryManagement.Core.Interfaces;
using LibraryManagement.Infrastructure.Services;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagement.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApiServicesExtension
    {
        public static IServiceCollection AddAuthIoc(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
