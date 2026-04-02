using QuizArena.Application.Abstractions;
using QuizArena.Application.Common.Results;
using QuizArena.Domain.Rooms;

namespace QuizArena.Application.Rooms.Queries;

public sealed record GetRoomsByOwnerQuery(Guid OwnerId) : IQuery<List<GetRoomsByOwnerResponse>>;

internal sealed class GetRoomsByOwnerQueryHandler(
    IRoomRepository repository) : IQueryHandler<GetRoomsByOwnerQuery, List<GetRoomsByOwnerResponse>>
{
    public async Task<Result<List<GetRoomsByOwnerResponse>>> Handle(GetRoomsByOwnerQuery request, CancellationToken cancellationToken)
    {
        var rooms = await repository.GetByOwnerIdAsync(request.OwnerId, cancellationToken);
        var response = rooms
            .Select(r => new GetRoomsByOwnerResponse(r.Id, r.Name, r.Description))
            .ToList();
        
        return Result<List<GetRoomsByOwnerResponse>>.Success(response);
    }
}

public sealed record GetRoomsByOwnerResponse(Guid Id, string Name, string Description);