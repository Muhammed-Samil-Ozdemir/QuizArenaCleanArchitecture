using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.RoomParticipants;

namespace QuizArena.Application.RoomParticipants.Commands;

public sealed record JoinRoomCommand(Guid RoomId, Guid UserId) : ICommand<JoinRoomResponse>;

public sealed class JoinRoomCommandValidator : AbstractValidator<JoinRoomCommand>
{
    public JoinRoomCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEqual(Guid.Empty).WithMessage("Room Id cannot be empty.");

        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("User Id cannot be empty.");
    }
}

internal sealed class JoinRoomCommandHandler(
    IRoomParticipantRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<JoinRoomCommand, JoinRoomResponse>
{
    public async Task<Result<JoinRoomResponse>> Handle(JoinRoomCommand request, CancellationToken cancellationToken)
    {
        bool alreadyJoined = await repository
            .AnyAsync(x => x.RoomId == request.RoomId && x.UserId == request.UserId, cancellationToken);
        if (alreadyJoined)
            return Result<JoinRoomResponse>.Conflict("User has already joined the room.");

        var participant = new RoomParticipant
        {
            RoomId = request.RoomId,
            UserId = request.UserId
        };
        
        await repository.AddAsync(participant, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<JoinRoomResponse>.Success(new JoinRoomResponse(participant.Id, participant.JoinedAt));
    }
}

public sealed record JoinRoomResponse(Guid Id, DateTime JoinedAt);