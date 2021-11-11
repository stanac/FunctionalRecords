﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalRecords;

public readonly record struct Result
{
    [Obsolete("Use Result.Success(...) or Result.Failure(...)")]
    public Result()
    {
    }

    private Result(bool success, IReadOnlyList<string> errors, Exception exception)
    {
        if (errors is null) throw new ArgumentNullException(nameof(errors));
        
        IsSuccess = success;
        Errors = errors.AsEnumerable().ToList();
        Exception = exception;
    }

    public bool IsSuccess { get; } = false;
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<string> Errors { get; } = new List<string>();
    public Maybe<Exception> Exception { get; } = Maybe<Exception>.None;

    public static Result Success() => new(true, new List<string>(), null);

    public static Result<TValue> Success<TValue>(TValue value) => new(true, value, new List<string>(), null);

    public static Result Failure() => new(false, new List<string>(), null);

    public static Result Failure(params string[] errors) => Failure(null, errors?.ToList() ?? new List<string>());

    public static Result Failure(IEnumerable<string> errors) => Failure(null, errors?.ToList() ?? new List<string>());

    public static Result Failure(Exception exception, params string[] errors) => Failure(exception, errors?.ToList() ?? new List<string>());

    public static Result Failure(Exception exception, IEnumerable<string> errors) => Failure(exception, errors?.ToList() ?? new List<string>());

    public static Result Failure(Exception exception, IReadOnlyList<string> errors) => new(false, errors, exception);

    public static Result<TValue> Failure<TValue>(params string[] errors) => Failure<TValue>(errors?.ToList() ?? new List<string>());

    public static Result<TValue> Failure<TValue>(IEnumerable<string> errors) => Failure<TValue>(errors?.ToList() ?? new List<string>());

    public static Result<TValue> Failure<TValue>(IReadOnlyList<string> errors) => new(false, Maybe<TValue>.None, errors ?? new List<string>(), null);

    public static Result<TValue> Failure<TValue>(Exception exception, IEnumerable<string> errors) => Failure<TValue>(exception, errors?.ToList() ?? new List<string>());

    public static Result<TValue> Failure<TValue>(Exception exception, params string[] errors) => Failure<TValue>(exception, errors?.ToList() ?? new List<string>());

    public static Result<TValue> Failure<TValue>(Exception exception, IReadOnlyList<string> errors) => new(false, default, errors ?? new List<string>(), exception);
}

public readonly record struct Result<TValue>
{

    [Obsolete("Use Result.Success<T>(...) or Result.Failure<T>(...)")]
    public Result()
    {
    }

    internal Result(bool success, Maybe<TValue> value, IReadOnlyList<string> errors, Maybe<Exception> exception)
    {
        if (errors == null) throw new ArgumentNullException(nameof(value));

        IsSuccess = success;
        Value = value;
        Errors = errors.AsEnumerable().ToList();
        Exception = exception;
    }

    public bool IsSuccess { get; } = false;
    public bool IsFailure => !IsSuccess;
    public IReadOnlyList<string> Errors { get; } = new List<string>();
    public Maybe<TValue> Value { get; } = Maybe<TValue>.None;
    public Maybe<Exception> Exception { get; } = Maybe<Exception>.None;
}