using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.RoomParticipants;
using QuizArena.Domain.UnitOfWorks;

namespace QuizArena.Application.RoomParticipants.Commands;

public sealed record LeaveRoomCommand(Guid RoomId, Guid UserId) : ICommand<LeaveRoomResponse>;

public sealed class LeaveRoomCommandValidator : AbstractValidator<LeaveRoomCommand>
{
    public LeaveRoomCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEqual(Guid.Empty).WithMessage("Room Id cannot be empty.");

        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("User Id cannot be empty.");
    }
}

internal sealed class LeaveRoomCommandHandler(
    IRoomParticipantRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<LeaveRoomCommand, LeaveRoomResponse>
{
    public async Task<Result<LeaveRoomResponse>> Handle(LeaveRoomCommand request, CancellationToken cancellationToken)
    {
        var participant = await repository.GetByRoomAndUserAsync(request.RoomId, request.UserId, cancellationToken);
        if (participant is null)
            return Result<LeaveRoomResponse>.NotFound("User is not in the room.");
        
        repository.Remove(participant);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<LeaveRoomResponse>.Success(new LeaveRoomResponse());
    }
}

public sealed record LeaveRoomResponse();