using MediatR;
using QuizArena.Application.Common.Results;

namespace QuizArena.Application.Abstractions;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse> { }