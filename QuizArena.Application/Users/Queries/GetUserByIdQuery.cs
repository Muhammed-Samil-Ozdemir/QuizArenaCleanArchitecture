using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdQueryResponse>;

internal sealed class GetUserByIdQueryHandler(
    IUserRepository repository) : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return Result<GetUserByIdQueryResponse>.NotFound("User not found.");
        
        return Result<GetUserByIdQueryResponse>.Success(new GetUserByIdQueryResponse(user.Id, user.Username, user.Email));
    }
}

public sealed record GetUserByIdQueryResponse(Guid Id, string Username, string Email);