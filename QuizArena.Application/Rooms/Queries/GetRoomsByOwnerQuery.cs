using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Rooms;

namespace QuizArena.Application.Rooms.Queries;

public sealed record GetRoomsByOwnerQuery(Guid OwnerId) : IQuery<List<GetRoomsByOwnerQueryResponse>>;

internal sealed class GetRoomsByOwnerQueryHandler(
    IRoomRepository repository) : IQueryHandler<GetRoomsByOwnerQuery, List<GetRoomsByOwnerQueryResponse>>
{
    public async Task<Result<List<GetRoomsByOwnerQueryResponse>>> Handle(GetRoomsByOwnerQuery request, CancellationToken cancellationToken)
    {
        var rooms = await repository.GetRoomsByOwnerAsync(request.OwnerId, cancellationToken);
        var response = rooms
            .Select(r => new GetRoomsByOwnerQueryResponse(r.Id, r.Name, r.Description))
            .ToList();
        
        return Result<List<GetRoomsByOwnerQueryResponse>>.Success(response);
    }
}

public sealed record GetRoomsByOwnerQueryResponse(Guid Id, string Name, string Description);