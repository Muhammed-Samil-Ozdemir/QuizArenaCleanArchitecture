using Microsoft.EntityFrameworkCore;
using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Questions;

namespace QuizArena.Application.Questions.Queries;

public sealed record GetQuestionsByRoomQuery(Guid RoomId) : IQuery<List<GetQuestionsByRoomResponse>>;

internal sealed class GetQuestionsByRoomQueryHandler(
    IQuestionRepository repository) : IQueryHandler<GetQuestionsByRoomQuery, List<GetQuestionsByRoomResponse>>
{
    public async Task<Result<List<GetQuestionsByRoomResponse>>> Handle(GetQuestionsByRoomQuery request,
        CancellationToken cancellationToken)
    {
        var questions = await repository
            .GetWhere(q => q.RoomId == request.RoomId)
            .Select(q => new GetQuestionsByRoomResponse(q.Id, q.Text))
            .ToListAsync(cancellationToken);
        
        return Result<List<GetQuestionsByRoomResponse>>.Success(questions);
    }
}

public sealed record GetQuestionsByRoomResponse(Guid Id, string Text);