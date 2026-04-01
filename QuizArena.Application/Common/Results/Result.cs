namespace QuizArena.Application.Common.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<string> ErrorMessages { get; }
    public ResultStatus Status { get; }

    protected Result(bool isSuccess, ResultStatus status, List<string> errorMessages)
    {
        IsSuccess = isSuccess;
        Status = status;
        ErrorMessages = errorMessages.AsReadOnly();
    }

    public static Result Success()
        => new(true, ResultStatus.Ok, []);

    public static Result Created()
        => new(true, ResultStatus.Created, []);

    public static Result Failure(params string[] errorMessages)
        => new(false, ResultStatus.BadRequest, errorMessages.ToList());

    public static Result NotFound(params string[] errorMessages)
        => new(false, ResultStatus.NotFound, errorMessages.ToList());

    public static Result Unauthorized(params string[] errorMessages)
        => new(false, ResultStatus.Unauthorized, errorMessages.ToList());

    public static Result Conflict(params string[] errorMessages)
        => new(false, ResultStatus.Conflict, errorMessages.ToList());

    public static Result ServerError(params string[] errorMessages)
        => new(false, ResultStatus.ServerError, errorMessages.ToList());
}

public sealed class Result<T> : Result
{
    private readonly T? _value;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Failure result has no value.");

    private Result(bool isSuccess, ResultStatus status, List<string> errorMessages, T? value)
        : base(isSuccess, status, errorMessages)
        => _value = value;

    public static Result<T> Success(T value)
        => new(true, ResultStatus.Ok, [], value);

    public static Result<T> Created(T value)
        => new(true, ResultStatus.Created, [], value);

    public new static Result<T> Failure(params string[] errorMessages)
        => new(false, ResultStatus.BadRequest, errorMessages.ToList(), default);

    public new static Result<T> NotFound(params string[] errorMessages)
        => new(false, ResultStatus.NotFound, errorMessages.ToList(), default);

    public new static Result<T> Unauthorized(params string[] errorMessages)
        => new(false, ResultStatus.Unauthorized, errorMessages.ToList(), default);

    public new static Result<T> Conflict(params string[] errorMessages)
        => new(false, ResultStatus.Conflict, errorMessages.ToList(), default);

    public new static Result<T> ServerError(params string[] errorMessages)
        => new(false, ResultStatus.ServerError, errorMessages.ToList(), default);

    public static implicit operator Result<T>(T value)
        => Success(value);
}

public enum ResultStatus
{
    Ok,
    Created,
    BadRequest,
    NotFound,
    Unauthorized,
    Conflict,
    ServerError
}