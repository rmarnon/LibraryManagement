using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<List<UserViewModel>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Result<List<UserViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();
            if (users is null)
                return Result.Fail<List<UserViewModel>>("Empty user list");

            return Result.Ok(users.Select(u => new UserViewModel(u.Name, u.Email)).ToList());
        }
    }
}
