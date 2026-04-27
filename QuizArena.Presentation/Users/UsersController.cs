using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.Users.Commands;
using QuizArena.Application.Users.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.Users;

public class UsersController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetAll(GetAllUsersQuery request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpGet("by-username/{username}")]
    public async Task<IActionResult> GetByUsername(GetUserByUsernameQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(GetUserByEmailQuery request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUserCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateUserCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(DeleteUserCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPatch("{id:guid}/change-password")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
}