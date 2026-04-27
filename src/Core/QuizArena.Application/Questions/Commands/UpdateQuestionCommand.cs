using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Questions;
using QuizArena.Domain.UnitOfWorks;

namespace QuizArena.Application.Questions.Commands;

public sealed record UpdateQuestionCommand(Guid Id, string Text) : ICommand<UpdateQuestionResponse>;

public sealed class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Question text cannot be empty.")
            .MaximumLength(500).WithMessage("Question text cannot exceed 500 characters.");
    }
}

internal sealed class UpdateQuestionCommandHandler(
    IQuestionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateQuestionCommand, UpdateQuestionResponse>
{
    public async Task<Result<UpdateQuestionResponse>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (question is null)
            return Result<UpdateQuestionResponse>.NotFound("Question not found.");

        question.Text = request.Text;
        
        repository.Update(question);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<UpdateQuestionResponse>.Success(new UpdateQuestionResponse(question.Id));
    }
}

public sealed record UpdateQuestionResponse(Guid Id);