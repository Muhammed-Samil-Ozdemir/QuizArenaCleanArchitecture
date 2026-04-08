using MediatR;
using QuizArena.Application.Common.Results;

namespace QuizArena.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }