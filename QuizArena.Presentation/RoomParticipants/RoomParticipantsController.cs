using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.RoomParticipants.Commands;
using QuizArena.Application.RoomParticipants.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.RoomParticipants;

public class RoomParticipantsController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetByRoomIdAsync(GetRoomParticipantsQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(JoinRoomCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> LeaveAsync(LeaveRoomCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
}