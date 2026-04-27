using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.Users.Commands;
using QuizArena.Application.Users.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.Users;

public sealed class UsersController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new GetAllUsersQuery(), cancellationToken));
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new GetUserByIdQuery(id), cancellationToken));
    
    [HttpGet("by-username/{username}")]
    public async Task<IActionResult> GetByUsername(string username, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new GetUserByUsernameQuery(username), cancellationToken));
    
    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetByEmail(string email, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new GetUserByEmailQuery(email), cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUserCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateUserCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(new DeleteUserCommand(id), cancellationToken));
    
    [HttpPatch("{id:guid}/change-password")]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
}