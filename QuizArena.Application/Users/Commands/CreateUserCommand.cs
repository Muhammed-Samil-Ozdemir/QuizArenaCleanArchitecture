using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Users;

namespace QuizArena.Application.Users.Commands;

public sealed record CreateUserCommand(
    string Username,
    string Email,
    string Password) : ICommand<CreateUserResponse>;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
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

internal sealed class CreateUserCommandHandler(
    IUserRepository repository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher) : ICommandHandler<CreateUserCommand, CreateUserResponse>
{
    public async Task<Result<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        bool userExist = await repository.AnyAsync(x => x.Username == request.Username || x.Email == request.Email, cancellationToken);
        if (userExist)
            return Result<CreateUserResponse>.Conflict("A user with the same username or email already exists.");

        var user = new User()
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHasher.Hash(request.Password)
        };
        
        await repository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<CreateUserResponse>.Success(new CreateUserResponse(user.Id));
    }
}

public sealed record CreateUserResponse(Guid Id);