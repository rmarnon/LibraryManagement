using LibraryManagement.Core.Repositories;
using LibraryManagement.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace LibraryManagement.Infrastructure.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryIoc(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(BaseRepository<>));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();

            return services;
        }
    }
}
