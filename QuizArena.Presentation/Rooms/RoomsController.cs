using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.Rooms.Commands;
using QuizArena.Application.Rooms.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.Rooms;

public sealed class RoomsController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDetails(Guid id, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new GetRoomDetailsQuery(id), cancellationToken));
    
    [HttpGet("by-owner/{ownerId:guid}")]
    public async Task<IActionResult> GetAllByOwnerId(Guid ownerId, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new GetRoomsByOwnerQuery(ownerId), cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateRoomCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateRoomCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new DeleteRoomCommand(id), cancellationToken));
}