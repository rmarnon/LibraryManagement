using LibraryManagement.Application.Commands.Users;
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
                Email = "Email",
                Password = "P@ssword0123",
                Role = Core.Enums.Role.User
            };
        }

        internal static UpdateUserCommand UpdateUserCommandFake()
        {
            return new()
            {
                Name = "Name",
                Email = "Email",
                Password = "P@ssword0123",
                Role = Core.Enums.Role.User
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
            return new() { Email = "Email", Password = "P@assword0123" };
        }
    }
}
