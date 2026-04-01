using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Queries;

public sealed record GetUserByEmailQuery(string Email) : IQuery<GetUserByEmailQueryResponse>;

internal sealed class GetUserByEmailQueryHandler(
    IUserRepository repository) : IQueryHandler<GetUserByEmailQuery, GetUserByEmailQueryResponse>
{
    public async Task<Result<GetUserByEmailQueryResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByEmail(request.Email, cancellationToken);
        if (user is null)
            return Result<GetUserByEmailQueryResponse>.NotFound("User not found.");
        
        return Result<GetUserByEmailQueryResponse>.Success(new GetUserByEmailQueryResponse(user.Id, user.Username, user.Email));
    }
}

public sealed record GetUserByEmailQueryResponse(Guid Id, string Username, string Email);