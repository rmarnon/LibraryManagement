﻿using MediatR;

namespace LibraryManagement.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<Unit>
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
