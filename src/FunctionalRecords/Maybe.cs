using System;

namespace FunctionalRecords;

public readonly record struct Maybe<T>
{
    private readonly bool _setToNull;
    private readonly T _value;

    public bool IsSome { get; }
    public bool IsNone => !IsSome;

    public T Value
    {
        get
        {
            if (!IsSome)
            {
                var error = _setToNull
                    ? $"Maybe<{typeof(T).Name}>.Value set to null"
                    : $"Maybe<{typeof(T).Name}>.Value not set";
                throw new InvalidOperationException(error);
            }

            return _value;
        }
    }

    public T ValueOrDefault
    {
        get
        {
            if (IsSome)
            {
                return _value;
            }

            return default;
        }
    }

    public Maybe()
    {
        IsSome = false;
        _setToNull = false;
        _value = default;
    }

    public Maybe(T value)
    {
        _value = value;
        IsSome = value != null;
        _setToNull = value == null;
    }

    public static Maybe<T> None => new Maybe<T>();

    public static Maybe<T> From(T value) => new(value);

    public static implicit operator Maybe<T>(T value) => new(value);

    public void Execute(Action<T> whenSet)
    {
        if (whenSet is null) throw new ArgumentNullException(nameof(whenSet));

        ExecuteInner(whenSet, null);
    }

    public void Execute(Action whenNotSet)
    {
        if (whenNotSet is null) throw new ArgumentNullException(nameof(whenNotSet));

        ExecuteInner(null, whenNotSet);
    }

    public void Execute(Action<T> whenSet, Action whenNotSet)
    {
        if (whenSet is null) throw new ArgumentNullException(nameof(whenSet));
        if (whenNotSet is null) throw new ArgumentNullException(nameof(whenNotSet));

        ExecuteInner(whenSet, whenNotSet);
    }

    private void ExecuteInner(Action<T> whenSet, Action whenNotSet)
    {
        if (IsSome && whenSet != null)
        {
            whenSet(Value);
        }
        else if (IsNone && whenNotSet != null)
        {
            whenNotSet();
        }
    }

    public TResult Execute<TResult>(Func<T, TResult> whenSet, Func<TResult> whenNotSet)
    {
        if (whenSet is null) throw new ArgumentNullException(nameof(whenSet));
        if (whenNotSet is null) throw new ArgumentNullException(nameof(whenNotSet));

        return ExecuteInner(whenSet, whenNotSet);
    }

    private TResult ExecuteInner<TResult>(Func<T, TResult> whenSet, Func<TResult> whenNotSet)
    {
        if (IsSome)
        {
            return whenSet(Value);
        }
        else
        {
            return whenNotSet();
        }
    }

}
