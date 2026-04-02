using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Rooms;

namespace QuizArena.Application.Rooms.Commands;

public sealed record CreateRoomCommand(string Name, string Description, Guid OwnerId) : ICommand<CreateRoomResponse>;

public sealed class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(50).WithMessage("The name can be up to 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty.")
            .MaximumLength(500).WithMessage("Description can be up to 500 characters.");

        RuleFor(x => x.OwnerId)
            .NotEqual(Guid.Empty).WithMessage("OwnerId cannot be empty.");
    }
}

internal sealed class CreateRoomCommandHandler(
     IRoomRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateRoomCommand, CreateRoomResponse>
{
    public async Task<Result<CreateRoomResponse>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var room = new Room()
        {
            Name = request.Name,
            Description = request.Description,
            OwnerId = request.OwnerId
        };
        
        await repository.AddAsync(room, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<CreateRoomResponse>.Success(new CreateRoomResponse(room.Id));
    }
}

public sealed record CreateRoomResponse(Guid Id);