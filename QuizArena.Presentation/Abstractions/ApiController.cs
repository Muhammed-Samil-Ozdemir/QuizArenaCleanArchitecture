using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuizArena.Application.Common.Results;

namespace QuizArena.Presentation.Abstractions;

[ApiController]
[Route("api/[controller]")]
public class ApiController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator;
    
    protected IActionResult ToActionResult<T>(Result<T> result) => result.Status switch
    {
        ResultStatus.Ok => Ok(result.Value),
        ResultStatus.Created => StatusCode(201, result.Value),
        ResultStatus.NotFound => NotFound(result.ErrorMessages),
        ResultStatus.BadRequest => BadRequest(result.ErrorMessages),
        ResultStatus.Unauthorized => StatusCode(401, result.ErrorMessages),
        ResultStatus.Conflict => StatusCode(409, result.ErrorMessages),
        ResultStatus.ServerError => StatusCode(500, result.ErrorMessages),
        _ => StatusCode(500)
    };

    protected IActionResult ToActionResult(Result result) => result.Status switch
    {
        ResultStatus.Ok => Ok(),
        ResultStatus.Created => StatusCode(201),
        ResultStatus.NotFound => NotFound(result.ErrorMessages),
        ResultStatus.BadRequest => BadRequest(result.ErrorMessages),
        ResultStatus.Unauthorized => StatusCode(401, result.ErrorMessages),
        ResultStatus.Conflict => StatusCode(409, result.ErrorMessages),
        ResultStatus.ServerError => StatusCode(500, result.ErrorMessages),
        _ => StatusCode(500)
    };
}