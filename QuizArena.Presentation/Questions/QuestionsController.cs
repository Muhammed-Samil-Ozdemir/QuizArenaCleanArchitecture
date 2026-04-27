using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.Questions.Commands;
using QuizArena.Application.Questions.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.Questions;

public sealed class QuestionsController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetByRoomIdAsync(GetQuestionsByRoomQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpGet]
    public async Task<IActionResult> GetWithOptionsAsync(GetQuestionWithOptionsQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(AddQuestionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateQuestionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(DeleteQuestionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
}