using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Options;

namespace QuizArena.Application.Options.Queries;

public sealed record GetOptionsByQuestionQuery(Guid QuestionId) : IQuery<List<GetOptionsByQuestionResponse>>;

internal sealed class GetOptionsByQuestionQueryHandler(
    IOptionRepository repository) : IQueryHandler<GetOptionsByQuestionQuery, List<GetOptionsByQuestionResponse>>
{
    public async Task<Result<List<GetOptionsByQuestionResponse>>> Handle(GetOptionsByQuestionQuery request, CancellationToken cancellationToken)
    {
        var options = await repository.GetByQuestionIdAsync(request.QuestionId, cancellationToken);
        var response = options
            .Select(o => new GetOptionsByQuestionResponse(o.Id, o.Text, o.IsCorrect))
            .ToList();
        
        return Result<List<GetOptionsByQuestionResponse>>.Success(response);
    }
}

public sealed record GetOptionsByQuestionResponse(Guid Id, string Text, bool IsCorrect);