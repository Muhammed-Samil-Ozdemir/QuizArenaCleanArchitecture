using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Domain.UnitOfWorks;

namespace QuizArena.Application.QuestionOptions.Commands;

public sealed record DeleteQuestionOptionCommand(Guid Id) : ICommand<DeleteQuestionOptionResponse>;

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
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteQuestionOptionCommand, DeleteQuestionOptionResponse>
{
    public async Task<Result<DeleteQuestionOptionResponse>> Handle(DeleteQuestionOptionCommand request, CancellationToken cancellationToken)
    {
        var questionOption = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (questionOption is null)
            return Result<DeleteQuestionOptionResponse>.NotFound("Option not found.");
        
        repository.Remove(questionOption);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<DeleteQuestionOptionResponse>.Success(new DeleteQuestionOptionResponse());
    }
}

public sealed record DeleteQuestionOptionResponse();