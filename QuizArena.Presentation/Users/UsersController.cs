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
    
    [HttpGet]
    public async Task<IActionResult> GetById(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpGet]
    public async Task<IActionResult> GetByUsername(GetUserByUsernameQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpGet]
    public async Task<IActionResult> GetByEmail(GetUserByEmailQuery request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUserCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateUserCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(DeleteUserCommand request, CancellationToken cancellationToken) =>
        ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPatch]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
}