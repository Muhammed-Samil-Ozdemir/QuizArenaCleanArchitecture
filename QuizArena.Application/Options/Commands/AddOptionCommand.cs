using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Options;

namespace QuizArena.Application.Options.Commands;

public sealed record AddOptionCommand(Guid QuestionId, string Text, bool IsCorrect) : ICommand<AddOptionResponse>;

public sealed class AddOptionCommandValidator : AbstractValidator<AddOptionCommand>
{
    public AddOptionCommandValidator()
    {
        RuleFor(x => x.QuestionId)
            .NotEqual(Guid.Empty).WithMessage("Question Id cannot be empty.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Option text cannot be empty.")
            .MaximumLength(200).WithMessage("Option text cannot exceed 200 characters.");
    }
}

internal sealed class AddOptionCommandHandler(
    IOptionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddOptionCommand, AddOptionResponse>
{
    public async Task<Result<AddOptionResponse>> Handle(AddOptionCommand request, CancellationToken cancellationToken)
    {
        bool optionExist = await repository.AnyAsync(x => x.Text == request.Text, cancellationToken);
        if (optionExist)
            return Result<AddOptionResponse>.Conflict("An option with the same text already exists.");
        
        var option = new Option()
        {
            QuestionId = request.QuestionId,
            Text = request.Text,
            IsCorrect = request.IsCorrect
        };
        
        await repository.AddAsync(option, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<AddOptionResponse>.Success(new AddOptionResponse(option.Id));
    }
}

public sealed record AddOptionResponse(Guid Id);