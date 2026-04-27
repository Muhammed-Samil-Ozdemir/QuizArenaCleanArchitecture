using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Application.Security;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.UnitOfWorks;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Commands;

public sealed record UpdateUserCommand(
    Guid Id,
    string Username,
    string Email,
    string Password) : ICommand<UpdateUserResponse>;
    
public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username cannot be empty.")
            .MaximumLength(50).WithMessage("The username can be up to 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Please enter a valid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("The password must be at least 6 characters.");
    }
}

internal sealed class UpdateUserCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher) : ICommandHandler<UpdateUserCommand, UpdateUserResponse>
{
    public async Task<Result<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (user is null)
            return Result<UpdateUserResponse>.NotFound("User not found.");

        var conflict = await repository.AnyAsync(
            x => x.Id != request.Id && (x.Email == request.Email || x.Username == request.Username),
            cancellationToken);
        if (conflict)
            return Result<UpdateUserResponse>.Conflict("A user with the same username or email already exists.");

        user.Username = request.Username;
        user.Email = request.Email;
        user.PasswordHash = passwordHasher.Hash(request.Password);

        repository.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UpdateUserResponse>.Success(new UpdateUserResponse(user.Id));
    }
}

public sealed record UpdateUserResponse(Guid Id);