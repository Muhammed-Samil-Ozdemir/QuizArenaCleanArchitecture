using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Questions;

namespace QuizArena.Application.Questions.Queries;

public sealed record GetQuestionWithOptionsQuery(Guid Id) : IQuery<GetQuestionWithOptionsResponse>;

internal sealed class GetQuestionWithOptionsQueryHandler(
    IQuestionRepository repository) : IQueryHandler<GetQuestionWithOptionsQuery, GetQuestionWithOptionsResponse>
{
    public async Task<Result<GetQuestionWithOptionsResponse>> Handle(GetQuestionWithOptionsQuery request, CancellationToken cancellationToken)
    {
        var question = await repository.GetWithOptionsAsync(request.Id, cancellationToken);
        if (question is null)
            return Result<GetQuestionWithOptionsResponse>.NotFound("Question not found.");

        var response = new GetQuestionWithOptionsResponse(
            question.Id,
            question.Text,
            question.QuestionOptions.Select(o => new QuestionOptionDto(o.Id, o.Text, o.IsCorrect)).ToList());

        return Result<GetQuestionWithOptionsResponse>.Success(response);
    }
}

public sealed record GetQuestionWithOptionsResponse(
    Guid Id,
    string Text,
    List<QuestionOptionDto> Options);

public sealed record QuestionOptionDto(
    Guid Id,
    string Text,
    bool IsCorrect);