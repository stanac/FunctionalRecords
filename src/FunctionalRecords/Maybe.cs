namespace FunctionalRecords;

public readonly record struct Maybe<T> : IMaybe
{
    private readonly bool _setToNull = false;
    private readonly T? _value = default;

    public bool IsSome { get; } = false;
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

            return _value!;
        }
    }

    public T? ValueOrDefault
    {
        get
        {
            if (IsSome)
            {
                return _value!;
            }

            return default;
        }
    }

    [Obsolete("Use Maybe<T>.None or Maybe<T>.From(...)")]
    public Maybe()
    {
        
    }

    public Maybe(T value)
    {
        _value = value;
        IsSome = value != null;
        _setToNull = value == null;
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public static Maybe<T> None => new();
#pragma warning restore CS0618 // Type or member is obsolete

    public static Maybe<T> From(T value) => new(value);

    public static implicit operator Maybe<T>(T value) => new(value);

    public void Match(Action<T> some)
    {
        if (some is null) throw new ArgumentNullException(nameof(some));

        MatchInner(some, null);
    }

    public void Match(Action none)
    {
        if (none is null) throw new ArgumentNullException(nameof(none));

        MatchInner(null, none);
    }

    public void Match(Action<T> some, Action none)
    {
        if (some is null) throw new ArgumentNullException(nameof(some));
        if (none is null) throw new ArgumentNullException(nameof(none));

        MatchInner(some, none);
    }

    private void MatchInner(Action<T>? some, Action? none)
    {
        if (IsSome && some != null)
        {
            some(Value);
        }
        else if (IsNone && none != null)
        {
            none();
        }
    }

    public TResult Match<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        if (some is null) throw new ArgumentNullException(nameof(some));
        if (none is null) throw new ArgumentNullException(nameof(none));

        return MatchInner(some, none);
    }

    private TResult MatchInner<TResult>(Func<T, TResult> some, Func<TResult> none)
    {
        if (IsSome)
        {
            return some(Value);
        }
        else
        {
            return none();
        }
    }

    public override string ToString()
    {
        if (IsSome)
        {
            return $"Maybe<{typeof(T).Name}> Value: {Value}";
        }

        return $"Maybe<{typeof(T).Name}>: None";
    }
}
