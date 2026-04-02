using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Abstractions;
using QuizArena.Domain.Questions;

namespace QuizArena.Application.Questions.Commands;

public sealed record AddQuestionCommand(Guid RoomId, string Text) : ICommand<AddQuestionResponse>;

public sealed class AddQuestionCommandValidator : AbstractValidator<AddQuestionCommand>
{
    public AddQuestionCommandValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEqual(Guid.Empty).WithMessage("Room Id cannot be empty.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Question text cannot be empty.")
            .MaximumLength(500).WithMessage("Question text cannot exceed 500 characters.");
    }
}

internal sealed class AddQuestionCommandHandler(
    IQuestionRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<AddQuestionCommand, AddQuestionResponse>
{
    public async Task<Result<AddQuestionResponse>> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = new Question
        {
            RoomId = request.RoomId,
            Text = request.Text
        };
        
        await repository.AddAsync(question, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<AddQuestionResponse>.Success(new AddQuestionResponse(question.Id));
    }
}

public sealed record AddQuestionResponse(Guid Id);