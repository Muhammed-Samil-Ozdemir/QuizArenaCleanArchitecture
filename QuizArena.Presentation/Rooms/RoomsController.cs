using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.Rooms.Commands;
using QuizArena.Application.Rooms.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.Rooms;

public class RoomsController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDetails(GetRoomDetailsQuery request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpGet("by-owner/{ownerId:guid}")]
    public async Task<IActionResult> GetAllByOwnerId(GetRoomsByOwnerQuery request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateRoomCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateRoomCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(DeleteRoomCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
}