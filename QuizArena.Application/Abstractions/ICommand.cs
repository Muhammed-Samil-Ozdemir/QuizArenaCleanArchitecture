using MediatR;
using QuizArena.Application.Common.Results;

namespace QuizArena.Application.Abstractions;

public interface ICommand : IRequest<Result> { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }