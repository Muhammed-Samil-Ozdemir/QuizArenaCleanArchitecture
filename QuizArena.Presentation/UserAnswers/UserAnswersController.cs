using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.UserAnswers.Commands;
using QuizArena.Application.UserAnswers.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.UserAnswers;

public class UserAnswersController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet("by-user/{userId:guid}")]
    public async Task<IActionResult> GetByUserId(GetUserAnswersByUserQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(SubmitAnswerCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
}