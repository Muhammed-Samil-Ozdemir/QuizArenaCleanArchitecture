using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.Questions.Commands;
using QuizArena.Application.Questions.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.Questions;

public sealed class QuestionsController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet("by-room/{roomId:guid}")]
    public async Task<IActionResult> GetByRoomIdAsync(GetQuestionsByRoomQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpGet("{id:guid}/options")]
    public async Task<IActionResult> GetWithOptionsAsync(GetQuestionWithOptionsQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(AddQuestionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateQuestionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete(("{id:guid}"))]
    public async Task<IActionResult> DeleteAsync(DeleteQuestionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
}