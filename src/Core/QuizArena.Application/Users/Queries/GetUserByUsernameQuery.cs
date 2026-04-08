using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Queries;

public sealed record GetUserByUsernameQuery(string Username) : IQuery<GetUserByUsernameResponse>;

internal sealed class GetUserByUsernameQueryHandler(
    IUserRepository repository) : IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameResponse>
{
    public async Task<Result<GetUserByUsernameResponse>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByUsernameAsync(request.Username, cancellationToken);
        if (user is null)
            return Result<GetUserByUsernameResponse>.NotFound("User not found.");
        
        return Result<GetUserByUsernameResponse>.Success(new GetUserByUsernameResponse(user.Id, user.Username, user.Email));
    }
}

public sealed record GetUserByUsernameResponse(Guid Id, string Username, string Email);