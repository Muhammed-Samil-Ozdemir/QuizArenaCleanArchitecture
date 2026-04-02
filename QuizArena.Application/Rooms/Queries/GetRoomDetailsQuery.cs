using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Rooms;

namespace QuizArena.Application.Rooms.Queries;

public sealed record GetRoomDetailsQuery(Guid Id) : IQuery<GetRoomDetailsResponse>;

internal sealed class GetRoomDetailsQueryHandler(
    IRoomRepository repository) : IQueryHandler<GetRoomDetailsQuery, GetRoomDetailsResponse>
{
    public async Task<Result<GetRoomDetailsResponse>> Handle(GetRoomDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var room = await repository.GetRoomWithQuestionsAsync(request.Id, cancellationToken);
        if (room is null)
            return Result<GetRoomDetailsResponse>.NotFound("Room not found.");
        
        var response = new GetRoomDetailsResponse(
            room.Id,
            room.Name,
            room.Description,
            room.Questions.Select(q => 
                new QuestionDto(
                    q.Id,
                    q.Text,
                    q.Options.Select(o => 
                        new OptionDto(
                            o.Id,
                            o.Text,
                            o.IsCorrect)).ToList()))
                .ToList());
        
        return Result<GetRoomDetailsResponse>.Success(response);
    }
}

public sealed record GetRoomDetailsResponse(Guid Id,
    string Name,
    string Description,
    List<QuestionDto> Questions);

public sealed record QuestionDto(Guid Id, string Text, List<OptionDto> Options);

public sealed record OptionDto(Guid Id, string Text, bool IsCorrect);