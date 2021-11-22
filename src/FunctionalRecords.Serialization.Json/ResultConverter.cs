using FunctionalRecords;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal class ResultConverter : JsonConverter<Result>
{
    public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return Result.Failure();
        }

        JsonConverter<ResultState> stateConverter = options.GetConverter(typeof(ResultState)) as JsonConverter<ResultState>;
        ResultState state = stateConverter.Read(ref reader, typeof(ResultState), options);
        return state.ToResult();
    }

    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
    {
        ResultState state = new(value);

        JsonConverter<ResultState> stateConverter = options.GetConverter(typeof(ResultState)) as JsonConverter<ResultState>;
        stateConverter.Write(writer, state, options);
    }
}

internal class ResultConverter<T> : JsonConverter<Result<T>>
{
    public override Result<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return Result.Failure<T>();
        }

        JsonConverter<ResultState<T>> stateConverter = options.GetConverter(typeof(ResultState<T>)) as JsonConverter<ResultState<T>>;
        ResultState<T> state = stateConverter.Read(ref reader, typeof(ResultState<T>), options);
        return state.ToResult();
    }

    public override void Write(Utf8JsonWriter writer, Result<T> value, JsonSerializerOptions options)
    {
        ResultState<T> state = new(value);

        JsonConverter<ResultState<T>> stateConverter = options.GetConverter(typeof(ResultState<T>)) as JsonConverter<ResultState<T>>;
        stateConverter.Write(writer, state, options);
    }
}

internal class ResultConverter<T, TFailureType> : JsonConverter<Result<T, TFailureType>>
    where TFailureType: Enum
{
    public override Result<T, TFailureType> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return Result.Failure<T, TFailureType>();
        }

        JsonConverter<ResultState<T, TFailureType>> stateConverter = options.GetConverter(typeof(ResultState<T, TFailureType>)) as JsonConverter<ResultState<T, TFailureType>>;
        ResultState<T, TFailureType> state = stateConverter.Read(ref reader, typeof(ResultState<T, TFailureType>), options);
        return state.ToResult();
    }

    public override void Write(Utf8JsonWriter writer, Result<T, TFailureType> value, JsonSerializerOptions options)
    {
        ResultState<T, TFailureType> state = new(value);

        JsonConverter<ResultState<T, TFailureType>> stateConverter = options.GetConverter(typeof(ResultState<T, TFailureType>)) as JsonConverter<ResultState<T, TFailureType>>;
        stateConverter.Write(writer, state, options);
    }
}

internal class ResultState
{
    public ResultState()
    {
    }

    public ResultState(Result result)
    {
        IsSuccess = result.IsSuccess;
        Exception = result.Exception.ValueOrDefault;
        Errors = result.Errors?.ToList() ?? new List<string>();
    }

    public bool IsSuccess { get; set; }
    public Exception Exception { get; set; }
    public List<string> Errors { get; set; }

    public Result ToResult() => IsSuccess
        ? Result.Success()
        : Result.Failure(Exception, Errors);
}

internal class ResultState<T>
{
    public ResultState()
    {
    }

    public ResultState(Result<T> result)
    {
        IsSuccess = result.IsSuccess;
        Exception = result.Exception.ValueOrDefault;
        Errors = result.Errors?.ToList() ?? new List<string>();
        Value = result.Value.ValueOrDefault;
    }

    public bool IsSuccess { get; set; }
    public Exception Exception { get; set; }
    public List<string> Errors { get; set; }
    public T Value { get; set; }

    public Result<T> ToResult() => IsSuccess
        ? Result.Success<T>(Value)
        : Result.Failure<T>(Exception, Errors);
}


internal class ResultState<T, TFailureType> where TFailureType: Enum
{
    public ResultState()
    {
    }

    public ResultState(Result<T, TFailureType> result)
    {
        IsSuccess = result.IsSuccess;
        Exception = result.Exception.ValueOrDefault;
        Errors = result.Errors?.ToList() ?? new List<string>();
        Value = result.Value.ValueOrDefault;

        if (result.FailureType.IsSome)
        {
            FailureTypeSet = true;
            FailureType = result.FailureType.Value;
        }
    }

    public bool IsSuccess { get; set; }
    public Exception Exception { get; set; }
    public List<string> Errors { get; set; }
    public T Value { get; set; }
    public bool FailureTypeSet { get; set; }
    public TFailureType FailureType { get; set; }

    public Result<T, TFailureType> ToResult()
    {
        if (IsSuccess)
        {
            return Result.Success<T, TFailureType>(Value);
        }

        if (FailureTypeSet)
        {
            return Result.Failure<T, TFailureType>(FailureType, Exception, Errors);
        }

        return Result.Failure<T, TFailureType>(Exception, Errors);
    }
}