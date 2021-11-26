namespace FunctionalRecords;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    IReadOnlyList<string> Errors { get; }
    Maybe<Exception> Exception { get; }
}

public interface IResult<TValue> : IResult
{
    Maybe<TValue> Value { get; }
}

public interface IResult<TValue, TFailureType> : IResult<TValue> where TFailureType : Enum
{
    Maybe<TFailureType> FailureType { get; }
}