using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.QuestionOptions.Commands;
using QuizArena.Application.QuestionOptions.Queries;
using QuizArena.Presentation.Abstractions;

namespace QuizArena.Presentation.QuestionOptions;

public sealed class QuestionOptionsController(IMediator mediator) : ApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetByQuestionIdAsync(GetQuestionOptionsByQuestionQuery request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(AddQuestionOptionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateQuestionOptionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
    
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(DeleteQuestionOptionCommand request,
        CancellationToken cancellationToken) => ToActionResult(await Mediator.Send(request, cancellationToken));
}