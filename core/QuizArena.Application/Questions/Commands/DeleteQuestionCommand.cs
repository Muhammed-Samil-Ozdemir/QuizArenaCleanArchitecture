using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Questions;

namespace QuizArena.Application.Questions.Commands;

public sealed record DeleteQuestionCommand(Guid Id) : ICommand;

public sealed class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
{
    public DeleteQuestionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");
    }
}

internal sealed class DeleteQuestionCommandHandler(
    IQuestionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteQuestionCommand>
{
    public async Task<Result> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (question is null)
            return Result.NotFound("Question not found.");
        
        repository.Remove(question);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}