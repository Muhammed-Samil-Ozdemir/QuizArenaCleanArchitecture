using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResponse>;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository repository) : IQueryHandler<GetUserByIdQuery, GetUserByIdResponse>
{
    public async Task<Result<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return Result<GetUserByIdResponse>.NotFound("User not found.");
        
        return Result<GetUserByIdResponse>.Success(new GetUserByIdResponse(user.Id, user.Username, user.Email));
    }
}

public sealed record GetUserByIdResponse(Guid Id, string Username, string Email);