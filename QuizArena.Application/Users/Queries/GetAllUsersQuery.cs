using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Queries;

public sealed record GetAllUsersQuery() : IQuery<List<GetAllUsersQueryResponse>>;

internal sealed class GetAllUsersQueryHandler(
    IUserRepository repository) : IQueryHandler<GetAllUsersQuery, List<GetAllUsersQueryResponse>>
{
    public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync(cancellationToken);
        var response = users
            .Select(u => new GetAllUsersQueryResponse(u.Id, u.Username, u.Email))
            .ToList();
        
        return Result<List<GetAllUsersQueryResponse>>.Success(response);
    }
}

public sealed record GetAllUsersQueryResponse(Guid Id, string Username, string Email);