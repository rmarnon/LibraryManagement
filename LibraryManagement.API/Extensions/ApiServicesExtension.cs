using LibraryManagement.Core.Interfaces;
using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Infrastructure.Services;

namespace LibraryManagement.API.Extensions
{
    public static class ApiServicesExtension
    {
        public static IServiceCollection AddApiIoC(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}
