using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Queries;

public sealed record GetAllUsersQuery() : IQuery<List<GetAllUsersResponse>>;

internal sealed class GetAllUsersQueryHandler(
    IUserRepository repository) : IQueryHandler<GetAllUsersQuery, List<GetAllUsersResponse>>
{
    public async Task<Result<List<GetAllUsersResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(cancellationToken);
        var response = users
            .Select(u => new GetAllUsersResponse(u.Id, u.Username, u.Email))
            .ToList();
        
        return Result<List<GetAllUsersResponse>>.Success(response);
    }
}

public sealed record GetAllUsersResponse(Guid Id, string Username, string Email);