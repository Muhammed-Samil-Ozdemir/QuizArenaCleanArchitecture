using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Domain.UnitOfWorks;

namespace QuizArena.Application.QuestionOptions.Commands;

public sealed record AddQuestionOptionCommand(Guid QuestionId, string Text, bool IsCorrect) : ICommand<AddQuestionOptionResponse>;

public sealed class AddQuestionOptionCommandValidator : AbstractValidator<AddQuestionOptionCommand>
{
    public AddQuestionOptionCommandValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEqual(Guid.Empty).WithMessage("Question Id cannot be empty.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Option text cannot be empty.")
            .MaximumLength(200).WithMessage("Option text cannot exceed 200 characters.");
    }
}

internal sealed class AddQuestionOptionCommandHandler(
    IQuestionOptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddQuestionOptionCommand, AddQuestionOptionResponse>
{
    public async Task<Result<AddQuestionOptionResponse>> Handle(AddQuestionOptionCommand request, CancellationToken cancellationToken)
    {
        bool optionExist = await repository.AnyAsync(x =>
            x.QuestionId == request.QuestionId && x.Text == request.Text, cancellationToken);
        if (optionExist)
            return Result<AddQuestionOptionResponse>.Conflict("An option with the same text already exists.");
        
        var questionOption = new QuestionOption()
        {
            QuestionId = request.QuestionId,
            Text = request.Text,
            IsCorrect = request.IsCorrect
        };
        
        await repository.AddAsync(questionOption, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<AddQuestionOptionResponse>.Success(new AddQuestionOptionResponse(questionOption.Id));
    }
}

public sealed record AddQuestionOptionResponse(Guid Id);