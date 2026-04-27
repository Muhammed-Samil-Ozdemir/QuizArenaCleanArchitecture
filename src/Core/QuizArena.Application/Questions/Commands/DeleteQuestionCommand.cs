using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Questions;
using QuizArena.Domain.UnitOfWorks;

namespace QuizArena.Application.Questions.Commands;

public sealed record DeleteQuestionCommand(Guid Id) : ICommand<DeleteQuestionResponse>;

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
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteQuestionCommand, DeleteQuestionResponse>
{
    public async Task<Result<DeleteQuestionResponse>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (question is null)
            return Result<DeleteQuestionResponse>.NotFound("Question not found.");
        
        repository.Remove(question);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<DeleteQuestionResponse>.Success(new DeleteQuestionResponse());
    }
}

public sealed record DeleteQuestionResponse();