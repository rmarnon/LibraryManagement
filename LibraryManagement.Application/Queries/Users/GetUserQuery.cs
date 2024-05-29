﻿using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Queries.Users
{
    public record GetUserQuery(Guid Id) : IRequest<UserViewModel>
    {
        public Guid Id { get; private set; } = Id;
    }
}
