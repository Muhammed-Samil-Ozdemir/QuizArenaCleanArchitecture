using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Queries;

public sealed record GetUserByUsernameQuery(string Username) : IQuery<GetUserByUsernameQueryResponse>;

internal sealed class GetUserByUsernameQueryHandler(
    IUserRepository repository) : IQueryHandler<GetUserByUsernameQuery, GetUserByUsernameQueryResponse>
{
    public async Task<Result<GetUserByUsernameQueryResponse>> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByUsernameAsync(request.Username, cancellationToken);
        if (user is null)
            return Result<GetUserByUsernameQueryResponse>.NotFound("User not found.");
        
        return Result<GetUserByUsernameQueryResponse>.Success(new GetUserByUsernameQueryResponse(user.Id, user.Username, user.Email));
    }
}

public sealed record GetUserByUsernameQueryResponse(Guid Id, string Username, string Email);