using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.RoomParticipants.Commands;
using QuizArena.Application.RoomParticipants.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.RoomParticipants;

public sealed class RoomParticipantsController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet("by-room/{roomId:guid}")]
    public async Task<IActionResult> GetByRoomIdAsync(Guid roomId, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new GetRoomParticipantsQuery(roomId), cancellationToken));
    
    [HttpPost("join")]
    public async Task<IActionResult> CreateAsync(JoinRoomCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete("leave/{roomId:guid}/{userId:guid}")]
    public async Task<IActionResult> LeaveAsync(Guid roomId, Guid userId, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new LeaveRoomCommand(roomId, userId), cancellationToken));
}