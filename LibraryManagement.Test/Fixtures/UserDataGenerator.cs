using LibraryManagement.Application.Commands.Users;
using LibraryManagement.Application.Queries.Users;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Enums;

namespace LibraryManagement.Test.Fixtures
{
    internal static class UserDataGenerator
    {
        internal static UserViewModel GetUserViewModel()
        {
            return new("Name", "Email");
        }

        internal static LoginUserViewModel GetLoginUserViewModel()
        {
            return new("Email", "Token");
        }

        internal static CreateUserCommand CreateUserCommandFake()
        {
            return new()
            {
                Name = "Name",
                Email = "email@gmail.com",
                Password = "P@ssword0123",
                Role = Role.User
            };
        }

        internal static UpdateUserCommand UpdateUserCommandFake()
        {
            return new()
            {
                Name = "Name",
                Email = "email@yahoo.com.br",
                Password = "P@ssword0123",
                Role = Role.User
            };
        }

        internal static DeleteUserCommand DeleteUserCommandFake()
        {
            return new(Guid.NewGuid());
        }

        internal static User GetUserFake()
        {
            return new("Name", "Email", "P@assword0123", Role.Admin);
        }

        internal static LoginUserCommand LoginUserCommandFake()
        {
            return new() { Email = "email@hotmail.com", Password = "P@assword0123" };
        }

        internal static GetAllUsersQuery GetAllUsersQueryFake()
        {
            return new("query", new());
        }

        internal static GetUserQuery GetUserQueryFake()
        {
            return new(Guid.NewGuid());
        }
    }
}
