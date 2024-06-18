﻿using FluentResults;
using LibraryManagement.Application.ViewModels;
using MediatR;

namespace LibraryManagement.Application.Queries.Users
{
    public record GetAllUsersQuery : IRequest<Result<List<UserViewModel>>>
    {
        public string Query { get; private set; }

        public GetAllUsersQuery(string query) => Query = query;
    }
}
