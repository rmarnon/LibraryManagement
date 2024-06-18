using FluentResults;
using LibraryManagement.Application.ViewModels;
using LibraryManagement.Core.Repositories;
using MediatR;

namespace LibraryManagement.Application.Queries.Users
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<UserViewModel>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Result<UserViewModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetOneAsync(request.Id);

            if (user is null)
                return Result.Fail<UserViewModel>("User not found");

            return Result.Ok(new UserViewModel(user.Name, user.Email));
        }
    }
}
