using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.QuestionOptions;
using QuizArena.Domain.UnitOfWorks;

namespace QuizArena.Application.QuestionOptions.Commands;

public sealed record UpdateQuestionOptionCommand(Guid Id,
    Guid QuestionId,
    string Text,
    bool IsCorrect) : ICommand<UpdateQuestionOptionResponse>;

public sealed class UpdateQuestionOptionCommandValidator : AbstractValidator<UpdateQuestionOptionCommand>
{
    public UpdateQuestionOptionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Option text cannot be empty.")
            .MaximumLength(200).WithMessage("Option text cannot exceed 200 characters.");
    }
}

internal sealed class UpdateQuestionOptionCommandHandler(
    IQuestionOptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateQuestionOptionCommand, UpdateQuestionOptionResponse>
{
    public async Task<Result<UpdateQuestionOptionResponse>> Handle(UpdateQuestionOptionCommand request, CancellationToken cancellationToken)
    {
        var questionOption = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (questionOption is null)
            return Result<UpdateQuestionOptionResponse>.NotFound("Option not found.");

        bool optionExist = await repository.AnyAsync(x => 
            x.Id != request.Id && x.QuestionId == request.QuestionId && x.Text == request.Text, cancellationToken);
        if (optionExist)
            return Result<UpdateQuestionOptionResponse>.Conflict("An option with the same text already exists.");

        questionOption.Text = request.Text;
        questionOption.IsCorrect = request.IsCorrect;

        repository.Update(questionOption);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UpdateQuestionOptionResponse>.Success(new UpdateQuestionOptionResponse(questionOption.Id));
    }
}

public sealed record UpdateQuestionOptionResponse(Guid Id);