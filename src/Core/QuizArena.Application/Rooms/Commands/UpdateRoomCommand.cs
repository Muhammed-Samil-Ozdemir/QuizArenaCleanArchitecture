using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Rooms;
using QuizArena.Domain.UnitOfWorks;

namespace QuizArena.Application.Rooms.Commands;

public sealed record UpdateRoomCommand(
    Guid Id,
    string Name,
    string Description) : ICommand<UpdateRoomResponse>;

public sealed class UpdateRoomCommandValidator : AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(50).WithMessage("The name can be up to 50 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty.")
            .MaximumLength(500).WithMessage("Description can be up to 500 characters.");
    }
}

internal sealed class UpdateRoomCommandHandler(
    IRoomRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateRoomCommand, UpdateRoomResponse>
{
    public async Task<Result<UpdateRoomResponse>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (room is null)
            return Result<UpdateRoomResponse>.NotFound("Room not found.");

        room.Name = request.Name;
        room.Description = request.Description;

        repository.Update(room);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UpdateRoomResponse>.Success(new UpdateRoomResponse(room.Id));
    }
}

public sealed record UpdateRoomResponse(Guid Id);