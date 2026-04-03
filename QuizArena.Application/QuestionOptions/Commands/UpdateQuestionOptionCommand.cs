using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.QuestionOptions;

namespace QuizArena.Application.QuestionOptions.Commands;

public sealed record UpdateQuesitonOptionCommand(Guid Id,
    Guid QuestionId,
    string Text,
    bool IsCorrect) : ICommand<UpdateQuestionOptionResponse>;

public sealed class UpdateQuesitonOptionCommandValidator : AbstractValidator<UpdateQuesitonOptionCommand>
{
    public UpdateQuesitonOptionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("Id cannot be empty.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Option text cannot be empty.")
            .MaximumLength(200).WithMessage("Option text cannot exceed 200 characters.");
    }
}

internal sealed class UpdateQuesitonOptionCommandHandler(
    IQuestionOptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateQuesitonOptionCommand, UpdateQuestionOptionResponse>
{
    public async Task<Result<UpdateQuestionOptionResponse>> Handle(UpdateQuesitonOptionCommand request, CancellationToken cancellationToken)
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