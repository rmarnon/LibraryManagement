﻿using LibraryManagement.Core.Enums;
using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
