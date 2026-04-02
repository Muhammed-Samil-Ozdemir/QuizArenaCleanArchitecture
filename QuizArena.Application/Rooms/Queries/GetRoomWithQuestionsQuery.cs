using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Questions;
using QuizArena.Domain.Rooms;

namespace QuizArena.Application.Rooms.Queries;

public sealed record GetRoomWithQuestionsQuery(Guid Id) : IQuery<GetRoomWithQuestionsQueryResponse>;

internal sealed class GetRoomWithQuestionsQueryHandler(
    IRoomRepository repository) : IQueryHandler<GetRoomWithQuestionsQuery, GetRoomWithQuestionsQueryResponse>
{
    public async Task<Result<GetRoomWithQuestionsQueryResponse>> Handle(GetRoomWithQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        var room = await repository.GetRoomWithQuestionsAsync(request.Id, cancellationToken);
        if (room is null)
            return Result<GetRoomWithQuestionsQueryResponse>.NotFound("Room not found.");
        
        var response = new GetRoomWithQuestionsQueryResponse(
            room.Id,
            room.Name,
            room.Description,
            room.Questions.Select(q => new QuestionDto(q.Id, q.Text)).ToList());
        
        return Result<GetRoomWithQuestionsQueryResponse>.Success(response);
    }
}

public sealed record GetRoomWithQuestionsQueryResponse(Guid Id,
    string Name,
    string Description,
    List<QuestionDto> Questions);

public sealed record QuestionDto(Guid Id, string Text);