using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Options;

namespace QuizArena.Application.Options.Commands;

public sealed record DeleteOptionCommand(Guid Id) : ICommand;

public sealed class DeleteOptionCommandValidator : AbstractValidator<DeleteOptionCommand>
{
    public DeleteOptionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");
    }
}

internal sealed class DeleteOptionCommandHandler(
    IOptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteOptionCommand>
{
    public async Task<Result> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
    {
        var option = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (option is null)
            return Result.NotFound("Option not found.");
        
        repository.Remove(option);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}