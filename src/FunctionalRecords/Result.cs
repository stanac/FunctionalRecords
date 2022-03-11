// ReSharper disable ConstantConditionalAccessQualifier
// ReSharper disable ConstantNullCoalescingCondition

namespace FunctionalRecords;

public readonly record struct Result : IResult
{
    [Obsolete("Use Result.Success(...) or Result.Failure(...)")]
    public Result()
    {
        Errors = new List<string>();
        Exception = Maybe<Exception>.None;
        IsSuccess = false;
    }

    private Result(bool success, IReadOnlyList<string> errors, Maybe<Exception> exception)
    {
        if (errors is null) throw new ArgumentNullException(nameof(errors));
        
        IsSuccess = success;
        Errors = errors.AsEnumerable().ToList();
        Exception = exception;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<string> Errors { get; } = new List<string>();
    public Maybe<Exception> Exception { get; } = Maybe<Exception>.None;

    public static Result Success() => new(true, new List<string>(), Maybe<Exception>.None);

    public static Result<TValue> Success<TValue>(TValue value) => new(true, value, new List<string>(), Maybe<Exception>.None);

    public static Result<TValue, TFailureType> Success<TValue, TFailureType>(TValue value)
        where TFailureType: Enum
        => new(true, value, new List<string>(), Maybe<Exception>.None, Maybe<TFailureType>.None);

    public static Result Failure() => new(false, new List<string>(), Maybe<Exception>.None);

    public static Result Failure(params string[] errors) => Failure(Maybe<Exception>.None, errors?.ToList() ?? new List<string>());

    public static Result Failure(IEnumerable<string> errors) => Failure(Maybe<Exception>.None, errors?.ToList() ?? new List<string>());

    public static Result Failure(Maybe<Exception> exception, params string[] errors) => Failure(exception, errors?.ToList() ?? new List<string>());

    public static Result Failure(Maybe<Exception> exception, IEnumerable<string> errors) => Failure(exception, errors?.ToList() ?? new List<string>());

    public static Result Failure(Maybe<Exception> exception, IReadOnlyList<string> errors) => new(false, errors, exception);

    public static Result<TValue> Failure<TValue>(params string[] errors) => Failure<TValue>(errors?.ToList() ?? new List<string>());

    public static Result<TValue> Failure<TValue>(IEnumerable<string> errors) => Failure<TValue>(errors?.ToList() ?? new List<string>());

    public static Result<TValue> Failure<TValue>(IReadOnlyList<string> errors) => new(false, Maybe<TValue>.None, errors ?? new List<string>(), Maybe<Exception>.None);

    public static Result<TValue> Failure<TValue>(Exception exception, IEnumerable<string> errors) => Failure<TValue>(exception, errors?.ToList() ?? new List<string>());

    public static Result<TValue> Failure<TValue>(Exception exception, params string[] errors) => Failure<TValue>(exception, errors?.ToList() ?? new List<string>());

    public static Result<TValue> Failure<TValue>(Exception exception, IReadOnlyList<string> errors)
        
        => new(false, default, errors ?? new List<string>(), exception);

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(params string[] errors)
        where TFailureType : Enum
        => Failure<TValue, TFailureType>(errors?.ToList() ?? new List<string>());

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(IEnumerable<string> errors)
        where TFailureType : Enum
        => Failure<TValue, TFailureType>(errors?.ToList() ?? new List<string>());

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(IReadOnlyList<string> errors)
        where TFailureType : Enum
        => new(false, Maybe<TValue>.None, errors ?? new List<string>(), Maybe<Exception>.None, Maybe<TFailureType>.None);

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(Exception exception, IEnumerable<string> errors)
        where TFailureType : Enum
        => Failure<TValue, TFailureType>(exception, errors?.ToList() ?? new List<string>());

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(Exception exception, params string[] errors)
        where TFailureType : Enum
        => Failure<TValue, TFailureType>(exception, errors?.ToList() ?? new List<string>());

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(Exception exception, IReadOnlyList<string> errors)
        where TFailureType : Enum
        => new(false, default, errors ?? new List<string>(), exception, Maybe<TFailureType>.None);
    
    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(TFailureType failure)
        where TFailureType : Enum
        => new(false, Maybe<TValue>.None, new List<string>(), Maybe<Exception>.None, failure);

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(TFailureType failure, params string[] errors)
        where TFailureType : Enum
        => Failure<TValue, TFailureType>(failure, errors?.ToList() ?? new List<string>());

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(TFailureType failure, IEnumerable<string> errors)
        where TFailureType : Enum
        => Failure<TValue, TFailureType>(failure, errors?.ToList() ?? new List<string>());

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(TFailureType failure, IReadOnlyList<string> errors)
        where TFailureType : Enum
        => new(false, Maybe<TValue>.None, errors ?? new List<string>(), Maybe<Exception>.None, failure);

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(TFailureType failure, Exception exception, IEnumerable<string> errors)
        where TFailureType : Enum
        => Failure<TValue, TFailureType>(failure, exception, errors?.ToList() ?? new List<string>());

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(TFailureType failure, Exception exception, params string[] errors)
        where TFailureType : Enum
        => Failure<TValue, TFailureType>(failure, exception, errors?.ToList() ?? new List<string>());

    public static Result<TValue, TFailureType> Failure<TValue, TFailureType>(TFailureType failure, Exception exception, IReadOnlyList<string> errors)
        where TFailureType : Enum
        => new(false, default, errors ?? new List<string>(), exception, failure);

    public void EnsureSuccess()
    {
        if (IsFailure)
        {
            string error = Errors.Any()
                ? string.Join("; ", Errors)
                : "Result is failure";

            throw new OperationFailedException(error, Exception.ValueOrDefault);
        }
    }
}

public readonly record struct Result<TValue> : IResult<TValue>
{
    [Obsolete("Use Result.Success<T>(...) or Result.Failure<T>(...)")]
    public Result()
    {
        Errors = new List<string>();
        Exception = Maybe<Exception>.None;
        IsSuccess = false;
        Value = Maybe<TValue>.None;
    }

    internal Result(bool success, Maybe<TValue> value, IReadOnlyList<string> errors, Maybe<Exception> exception)
    {
        if (errors == null) throw new ArgumentNullException(nameof(errors));

        IsSuccess = success;
        Value = value;
        Errors = errors.AsEnumerable().ToList();
        Exception = exception;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<string> Errors { get; } = new List<string>();
    public Maybe<TValue> Value { get; } = Maybe<TValue>.None;
    public Maybe<Exception> Exception { get; } = Maybe<Exception>.None;

    public void EnsureSuccess()
    {
        if (IsFailure)
        {
            string error = Errors.Any()
                ? string.Join("; ", Errors)
                : "Result is failure";

            throw new OperationFailedException(error, Exception.ValueOrDefault);
        }
    }
}

public readonly record struct Result<TValue, TFailureType> : IResult<TValue, TFailureType> where TFailureType: Enum
{
    [Obsolete("Use Result.Success<T>(...) or Result.Failure<T>(...)")]
    public Result()
    {
        Errors = new List<string>();
        Exception = Maybe<Exception>.None;
        IsSuccess = false;
        Value = Maybe<TValue>.None;
        FailureType = Maybe<TFailureType>.None;
    }

    internal Result(bool success, Maybe<TValue> value, IReadOnlyList<string> errors, Maybe<Exception> exception, Maybe<TFailureType> failureType)
    {
        if (errors == null) throw new ArgumentNullException(nameof(errors));

        IsSuccess = success;
        Value = value;
        Errors = errors.AsEnumerable().ToList();
        Exception = exception;
        FailureType = failureType;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<string> Errors { get; } = new List<string>();
    public Maybe<TValue> Value { get; } = Maybe<TValue>.None;
    public Maybe<TFailureType> FailureType { get; } = Maybe<TFailureType>.None;
    public Maybe<Exception> Exception { get; } = Maybe<Exception>.None;

    public bool Is(TFailureType value)
    {
        if (FailureType.IsSome)
        {
            int setValue = (int) (object) FailureType.Value;
            int v = (int) (object) value;

            return (setValue & v) == v;
        }

        return false;
    }

    public void EnsureSuccess()
    {
        if (IsFailure)
        {
            string error = Errors.Any()
                ? string.Join("; ", Errors)
                : "Result is failure";

            FailureType.Match(type => error = $"{type}: {error}");

            throw new OperationFailedException(error, Exception.ValueOrDefault);
        }
    }
}
