using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.QuestionOptions;

namespace QuizArena.Application.QuestionOptions.Queries;

public sealed record GetQuestionOptionsByQuestionQuery(Guid QuestionId) : IQuery<List<GetQuestionOptionsByQuestionResponse>>;

internal sealed class GetQuestionOptionsByQuestionQueryHandler(
    IQuestionOptionRepository repository) : IQueryHandler<GetQuestionOptionsByQuestionQuery, List<GetQuestionOptionsByQuestionResponse>>
{
    public async Task<Result<List<GetQuestionOptionsByQuestionResponse>>> Handle(GetQuestionOptionsByQuestionQuery request, CancellationToken cancellationToken)
    {
        var questionOptions = await repository.GetByQuestionIdAsync(request.QuestionId, cancellationToken);
        var response = questionOptions
            .Select(o => new GetQuestionOptionsByQuestionResponse(o.Id, o.Text, o.IsCorrect))
            .ToList();
        
        return Result<List<GetQuestionOptionsByQuestionResponse>>.Success(response);
    }
}

public sealed record GetQuestionOptionsByQuestionResponse(Guid Id, string Text, bool IsCorrect);