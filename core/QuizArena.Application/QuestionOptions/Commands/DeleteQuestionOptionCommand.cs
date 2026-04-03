using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.QuestionOptions;

namespace QuizArena.Application.QuestionOptions.Commands;

public sealed record DeleteQuestionOptionCommand(Guid Id) : ICommand;

public sealed class DeleteQuestionOptionCommandValidator : AbstractValidator<DeleteQuestionOptionCommand>
{
    public DeleteQuestionOptionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");
    }
}

internal sealed class DeleteOptionCommandHandler(
    IQuestionOptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteQuestionOptionCommand>
{
    public async Task<Result> Handle(DeleteQuestionOptionCommand request, CancellationToken cancellationToken)
    {
        var questionOption = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (questionOption is null)
            return Result.NotFound("Option not found.");
        
        repository.Remove(questionOption);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}