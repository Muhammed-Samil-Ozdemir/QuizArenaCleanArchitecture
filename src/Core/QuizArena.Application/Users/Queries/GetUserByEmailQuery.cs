using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Queries;

public sealed record GetUserByEmailQuery(string Email) : IQuery<GetUserByEmailResponse>;

internal sealed class GetUserByEmailQueryHandler(
    IUserRepository repository) : IQueryHandler<GetUserByEmailQuery, GetUserByEmailResponse>
{
    public async Task<Result<GetUserByEmailResponse>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return Result<GetUserByEmailResponse>.NotFound("User not found.");
        
        return Result<GetUserByEmailResponse>.Success(new GetUserByEmailResponse(user.Id, user.Username, user.Email));
    }
}

public sealed record GetUserByEmailResponse(Guid Id, string Username, string Email);