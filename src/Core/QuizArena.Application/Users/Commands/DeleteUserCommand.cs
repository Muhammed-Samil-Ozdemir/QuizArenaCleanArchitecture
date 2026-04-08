using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Commands;

public sealed record DeleteUserCommand(Guid Id) : ICommand<DeleteUserResponse>;

public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");
    }
}

internal sealed class DeleteUserCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, DeleteUserResponse>
{
    public async Task<Result<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return Result<DeleteUserResponse>.NotFound("User not found.");
        
        repository.Remove(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<DeleteUserResponse>.Success(new DeleteUserResponse());
    }
}

public sealed record DeleteUserResponse();