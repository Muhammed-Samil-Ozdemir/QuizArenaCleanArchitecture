using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.UserAnswers;

namespace QuizArena.Application.UserAnswers.Queries;

public sealed record GetUserAnswersByUserQuery(Guid UserId) : IQuery<List<GetUserAnswersByUserResponse>>;

internal sealed class GetUserAnswersByUserQueryHandler(
    IUserAnswerRepository repository) : IQueryHandler<GetUserAnswersByUserQuery, List<GetUserAnswersByUserResponse>>
{
    public async Task<Result<List<GetUserAnswersByUserResponse>>> Handle(GetUserAnswersByUserQuery request, CancellationToken cancellationToken)
    {
        var answers = await repository.GetByUserIdAsync(request.UserId, cancellationToken);
        
        var response = answers
            .Select(a => new GetUserAnswersByUserResponse(a.QuestionId, a.SelectedOptionId, a.IsCorrect, a.AnsweredAt))
            .ToList();
        
        return Result<List<GetUserAnswersByUserResponse>>.Success(response);
    }
}


public sealed record GetUserAnswersByUserResponse(Guid QuestionId, Guid SelectedOptionId, bool IsCorrect, DateTime AnsweredAt);