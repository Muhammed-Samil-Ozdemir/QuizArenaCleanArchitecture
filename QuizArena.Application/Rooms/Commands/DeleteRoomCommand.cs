using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Rooms;

namespace QuizArena.Application.Rooms.Commands;

public sealed record DeleteRoomCommand(Guid Id) : ICommand;

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
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteRoomCommand>
{
    public async Task<Result> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (room is null)
            return Result.NotFound("Room not found.");
        
        repository.Remove(room);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}