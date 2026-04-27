using FluentValidation;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Questions;
using QuizArena.Domain.UnitOfWorks;
using QuizArena.Domain.UserAnswers;

namespace QuizArena.Application.UserAnswers.Commands;

public sealed record SubmitAnswerCommand(
    Guid UserId,
    Guid QuestionId,
    Guid SelectedOptionId) : ICommand<SubmitAnswerResponse>;

public sealed class SubmitAnswerCommandValidator : AbstractValidator<SubmitAnswerCommand>
{
    public SubmitAnswerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEqual(Guid.Empty).WithMessage("User Id cannot be empty.");
        
        RuleFor(x => x.QuestionId)
            .NotEqual(Guid.Empty).WithMessage("Question Id cannot be empty.");
        
        RuleFor(x => x.SelectedOptionId)
            .NotEqual(Guid.Empty).WithMessage("Selected Option Id cannot be empty.");
    }
}

internal sealed class SubmitAnswerCommandHandler(
    IQuestionRepository questionRepository,
    IUserAnswerRepository answerRepository,
    IUnitOfWork unitOfWork): ICommandHandler<SubmitAnswerCommand, SubmitAnswerResponse>
{
    public async Task<Result<SubmitAnswerResponse>> Handle(SubmitAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await questionRepository.GetWithOptionsAsync(request.QuestionId, cancellationToken);
        if (question is null)
            return Result<SubmitAnswerResponse>.NotFound("Question not found.");

        var selectedOption = question.QuestionOptions.FirstOrDefault(x => x.Id == request.SelectedOptionId);
        if (selectedOption is null)
            return Result<SubmitAnswerResponse>.BadRequest("Selected option is invalid.");

        var answer = new UserAnswer()
        {
            UserId = request.UserId,
            QuestionId = request.QuestionId,
            SelectedOptionId = request.SelectedOptionId,
            IsCorrect = selectedOption.IsCorrect,
            AnsweredAt = DateTime.UtcNow
        };

        await answerRepository.AddAsync(answer, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<SubmitAnswerResponse>.Success(new SubmitAnswerResponse(answer.Id, answer.IsCorrect, answer.AnsweredAt));
    }
}

public sealed record SubmitAnswerResponse(Guid Id, bool IsCorrect, DateTime AnsweredAt);