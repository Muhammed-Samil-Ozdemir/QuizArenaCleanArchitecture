using FluentValidation;
using FluentValidation.Results;
using MediatR;
using QuizArena.Application.Abstractions;

namespace QuizArena.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!validators.Any())
                return await next(cancellationToken);

            var context = new ValidationContext<TRequest>(request);

            var errorDictionary = validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage, (propertyName, errorMessage) => new
                    {
                        Key = propertyName,
                        Values = errorMessage.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values[0]);

            if (errorDictionary.Any())
            {
                var errors = errorDictionary.Select(s => new ValidationFailure
                {
                    PropertyName = s.Value,
                    ErrorCode = s.Key
                });
                throw new ValidationException(errors);
            }

            return await next(cancellationToken);
        }
    }
}