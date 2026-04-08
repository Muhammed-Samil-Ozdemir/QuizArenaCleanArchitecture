using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Rooms;

namespace QuizArena.Application.Rooms.Commands;

public sealed record DeleteRoomCommand(Guid Id) : ICommand<DeleteRoomResponse>;

public sealed class DeleteRoomCommandValidator : AbstractValidator<DeleteRoomCommand>
{
    public DeleteRoomCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");
    }
}

internal sealed class DeleteRoomCommandHandler(
    IRoomRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteRoomCommand, DeleteRoomResponse>
{
    public async Task<Result<DeleteRoomResponse>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (room is null)
            return Result<DeleteRoomResponse>.NotFound("Room not found.");
        
        repository.Remove(room);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<DeleteRoomResponse>.Success(new DeleteRoomResponse());
    }
}

public sealed record DeleteRoomResponse();