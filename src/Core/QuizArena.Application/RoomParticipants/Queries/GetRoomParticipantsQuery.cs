using FluentValidation;
using Microsoft.EntityFrameworkCore;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.RoomParticipants;

namespace QuizArena.Application.RoomParticipants.Queries;

public sealed record GetRoomParticipantsQuery(Guid RoomId) : IQuery<List<GetRoomParticipantsResponse>>;

public sealed class GetRoomParticipantsQueryValidator : AbstractValidator<GetRoomParticipantsQuery>
{
    public GetRoomParticipantsQueryValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEqual(Guid.Empty).WithMessage("Room Id cannot be empty.");
    }
}

internal sealed class GetRoomParticipantsQueryHandler(
    IRoomParticipantRepository repository) : IQueryHandler<GetRoomParticipantsQuery, List<GetRoomParticipantsResponse>>
{
    public async Task<Result<List<GetRoomParticipantsResponse>>> Handle(GetRoomParticipantsQuery request, CancellationToken cancellationToken)
    {
        var participants = await repository
            .GetWhere(x => x.RoomId == request.RoomId)
            .Select(x => new GetRoomParticipantsResponse(x.UserId, x.User.Username, x.JoinedAt))
            .ToListAsync(cancellationToken);
        
        return Result<List<GetRoomParticipantsResponse>>.Success(participants);
    }
}

public sealed record GetRoomParticipantsResponse(Guid UserId, string Username, DateTime JoinedAt);