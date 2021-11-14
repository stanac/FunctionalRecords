using System;

namespace FunctionalRecords
{
    public readonly record struct Choice<T1, T2>
    {
        private readonly T1? _choice1 = default;
        private readonly T2? _choice2 = default;

        public int SelectedIndex { get; } = -1;

        public object Value
        {
            get
            {
                return SelectedIndex switch
                {
                    1 => _choice1!,
                    2 => _choice2!,
                    _ => throw new IndexOutOfRangeException("Unreachable code")
                };
            }
        }

        [Obsolete("Use Choice.From<T>(...)")]
        public Choice()
        {
            // throw new InvalidOperationException("Do not call this constructor");
        }

        private Choice(T1 choice1)
        {
            if (choice1 is null) throw new ArgumentNullException(nameof(choice1));
            SelectedIndex = 1;
            _choice1 = choice1;
        }

        private Choice(T2 choice2)
        {
            if (choice2 is null) throw new ArgumentNullException(nameof(choice2));
            SelectedIndex = 2;
            _choice2 = choice2;
        }

        public static Choice<T1, T2> From(T1 value) => new(value);
        public static Choice<T1, T2> From(T2 value) => new(value);

        public Type GetChosenType()
        {
            return SelectedIndex switch
            {
                1 => typeof(T1),
                2 => typeof(T2),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public bool Is<T>()
        {
            Type tType = typeof(T);

            if (tType == typeof(T1)) return SelectedIndex == 1;
            if (tType == typeof(T2)) return SelectedIndex == 2;
            return false;
        }

        public void Match(Action<T1> matchT1, Action<T2> matchT2)
        {
            switch (SelectedIndex)
            {
                case 1: matchT1(_choice1!); break;
                case 2: matchT2(_choice2!); break;
            }
        }

        public TResult Match<TResult>(Func<T1, TResult> matchT1, Func<T2, TResult> matchT2)
        {
            return SelectedIndex switch
            {
                1 => matchT1(_choice1!),
                2 => matchT2(_choice2!),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public static implicit operator Choice<T1, T2>(T1 choice1) => From(choice1);
        public static implicit operator Choice<T1, T2>(T2 choice2) => From(choice2);
    }

    public readonly record struct Choice<T1, T2, T3>
    {
        private readonly T1? _choice1 = default;
        private readonly T2? _choice2 = default;
        private readonly T3? _choice3 = default;

        public int SelectedIndex { get; } = -1;

        public object Value
        {
            get
            {
                return SelectedIndex switch
                {
                    1 => _choice1!,
                    2 => _choice2!,
                    3 => _choice3!,
                    _ => throw new IndexOutOfRangeException("Unreachable code")
                };
            }
        }

        [Obsolete("Use Choice.From<T>(...)")]
        public Choice()
        {
            // throw new InvalidOperationException("Do not call this constructor");
        }

        private Choice(T1 choice1)
        {
            if (choice1 is null) throw new ArgumentNullException(nameof(choice1));
            SelectedIndex = 1;
            _choice1 = choice1;
        }

        private Choice(T2 choice2)
        {
            if (choice2 is null) throw new ArgumentNullException(nameof(choice2));
            SelectedIndex = 2;
            _choice2 = choice2;
        }

        private Choice(T3 choice3)
        {
            if (choice3 is null) throw new ArgumentNullException(nameof(choice3));
            SelectedIndex = 3;
            _choice3 = choice3;
        }

        public static Choice<T1, T2, T3> From(T1 value) => new(value);
        public static Choice<T1, T2, T3> From(T2 value) => new(value);
        public static Choice<T1, T2, T3> From(T3 value) => new(value);

        public Type GetChosenType()
        {
            return SelectedIndex switch
            {
                1 => typeof(T1),
                2 => typeof(T2),
                3 => typeof(T3),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public bool Is<T>()
        {
            Type tType = typeof(T);

            if (tType == typeof(T1)) return SelectedIndex == 1;
            if (tType == typeof(T2)) return SelectedIndex == 2;
            if (tType == typeof(T3)) return SelectedIndex == 3;
            return false;
        }

        public void Match(Action<T1> matchT1, Action<T2> matchT2, Action<T3> matchT3)
        {
            switch (SelectedIndex)
            {
                case 1: matchT1(_choice1!); break;
                case 2: matchT2(_choice2!); break;
                case 3: matchT3(_choice3!); break;
            }
        }

        public TResult Match<TResult>(Func<T1, TResult> matchT1, Func<T2, TResult> matchT2, Func<T3, TResult> matchT3)
        {
            return SelectedIndex switch
            {
                1 => matchT1(_choice1!),
                2 => matchT2(_choice2!),
                3 => matchT3(_choice3!),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public static implicit operator Choice<T1, T2, T3>(T1 choice1) => From(choice1);
        public static implicit operator Choice<T1, T2, T3>(T2 choice2) => From(choice2);
        public static implicit operator Choice<T1, T2, T3>(T3 choice3) => From(choice3);
    }

    public readonly record struct Choice<T1, T2, T3, T4>
    {
        private readonly T1? _choice1 = default;
        private readonly T2? _choice2 = default;
        private readonly T3? _choice3 = default;
        private readonly T4? _choice4 = default;

        public int SelectedIndex { get; } = -1;

        public object Value
        {
            get
            {
                return SelectedIndex switch
                {
                    1 => _choice1!,
                    2 => _choice2!,
                    3 => _choice3!,
                    4 => _choice4!,
                    _ => throw new IndexOutOfRangeException("Unreachable code")
                };
            }
        }

        [Obsolete("Use Choice.From<T>(...)")]
        public Choice()
        {
            // throw new InvalidOperationException("Do not call this constructor");
        }

        private Choice(T1 choice1)
        {
            if (choice1 is null) throw new ArgumentNullException(nameof(choice1));
            SelectedIndex = 1;
            _choice1 = choice1;
        }

        private Choice(T2 choice2)
        {
            if (choice2 is null) throw new ArgumentNullException(nameof(choice2));
            SelectedIndex = 2;
            _choice2 = choice2;
        }

        private Choice(T3 choice3)
        {
            if (choice3 is null) throw new ArgumentNullException(nameof(choice3));
            SelectedIndex = 3;
            _choice3 = choice3;
        }

        private Choice(T4 choice4)
        {
            if (choice4 is null) throw new ArgumentNullException(nameof(choice4));
            SelectedIndex = 4;
            _choice4 = choice4;
        }

        public static Choice<T1, T2, T3, T4> From(T1 value) => new(value);
        public static Choice<T1, T2, T3, T4> From(T2 value) => new(value);
        public static Choice<T1, T2, T3, T4> From(T3 value) => new(value);
        public static Choice<T1, T2, T3, T4> From(T4 value) => new(value);

        public Type GetChosenType()
        {
            return SelectedIndex switch
            {
                1 => typeof(T1),
                2 => typeof(T2),
                3 => typeof(T3),
                4 => typeof(T4),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public bool Is<T>()
        {
            Type tType = typeof(T);

            if (tType == typeof(T1)) return SelectedIndex == 1;
            if (tType == typeof(T2)) return SelectedIndex == 2;
            if (tType == typeof(T3)) return SelectedIndex == 3;
            if (tType == typeof(T4)) return SelectedIndex == 4;
            return false;
        }

        public void Match(Action<T1> matchT1, Action<T2> matchT2, Action<T3> matchT3, Action<T4> matchT4)
        {
            switch (SelectedIndex)
            {
                case 1: matchT1(_choice1!); break;
                case 2: matchT2(_choice2!); break;
                case 3: matchT3(_choice3!); break;
                case 4: matchT4(_choice4!); break;
            }
        }

        public TResult Match<TResult>(Func<T1, TResult> matchT1, Func<T2, TResult> matchT2, Func<T3, TResult> matchT3, Func<T4, TResult> matchT4)
        {
            return SelectedIndex switch
            {
                1 => matchT1(_choice1!),
                2 => matchT2(_choice2!),
                3 => matchT3(_choice3!),
                4 => matchT4(_choice4!),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public static implicit operator Choice<T1, T2, T3, T4>(T1 choice1) => From(choice1);
        public static implicit operator Choice<T1, T2, T3, T4>(T2 choice2) => From(choice2);
        public static implicit operator Choice<T1, T2, T3, T4>(T3 choice3) => From(choice3);
        public static implicit operator Choice<T1, T2, T3, T4>(T4 choice4) => From(choice4);
    }

    public readonly record struct Choice<T1, T2, T3, T4, T5>
    {
        private readonly T1? _choice1 = default;
        private readonly T2? _choice2 = default;
        private readonly T3? _choice3 = default;
        private readonly T4? _choice4 = default;
        private readonly T5? _choice5 = default;

        public int SelectedIndex { get; } = -1;

        public object Value
        {
            get
            {
                return SelectedIndex switch
                {
                    1 => _choice1!,
                    2 => _choice2!,
                    3 => _choice3!,
                    4 => _choice4!,
                    5 => _choice5!,
                    _ => throw new IndexOutOfRangeException("Unreachable code")
                };
            }
        }

        [Obsolete("Use Choice.From<T>(...)")]
        public Choice()
        {
            // throw new InvalidOperationException("Do not call this constructor");
        }

        private Choice(T1 choice1)
        {
            if (choice1 is null) throw new ArgumentNullException(nameof(choice1));
            SelectedIndex = 1;
            _choice1 = choice1;
        }

        private Choice(T2 choice2)
        {
            if (choice2 is null) throw new ArgumentNullException(nameof(choice2));
            SelectedIndex = 2;
            _choice2 = choice2;
        }

        private Choice(T3 choice3)
        {
            if (choice3 is null) throw new ArgumentNullException(nameof(choice3));
            SelectedIndex = 3;
            _choice3 = choice3;
        }

        private Choice(T4 choice4)
        {
            if (choice4 is null) throw new ArgumentNullException(nameof(choice4));
            SelectedIndex = 4;
            _choice4 = choice4;
        }

        private Choice(T5 choice5)
        {
            if (choice5 is null) throw new ArgumentNullException(nameof(choice5));
            SelectedIndex = 5;
            _choice5 = choice5;
        }

        public static Choice<T1, T2, T3, T4, T5> From(T1 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5> From(T2 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5> From(T3 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5> From(T4 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5> From(T5 value) => new(value);

        public Type GetChosenType()
        {
            return SelectedIndex switch
            {
                1 => typeof(T1),
                2 => typeof(T2),
                3 => typeof(T3),
                4 => typeof(T4),
                5 => typeof(T5),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public bool Is<T>()
        {
            Type tType = typeof(T);

            if (tType == typeof(T1)) return SelectedIndex == 1;
            if (tType == typeof(T2)) return SelectedIndex == 2;
            if (tType == typeof(T3)) return SelectedIndex == 3;
            if (tType == typeof(T4)) return SelectedIndex == 4;
            if (tType == typeof(T5)) return SelectedIndex == 5;
            return false;
        }

        public void Match(Action<T1> matchT1, Action<T2> matchT2, Action<T3> matchT3, Action<T4> matchT4, Action<T5> matchT5)
        {
            switch (SelectedIndex)
            {
                case 1: matchT1(_choice1!); break;
                case 2: matchT2(_choice2!); break;
                case 3: matchT3(_choice3!); break;
                case 4: matchT4(_choice4!); break;
                case 5: matchT5(_choice5!); break;
            }
        }

        public TResult Match<TResult>(Func<T1, TResult> matchT1, Func<T2, TResult> matchT2, Func<T3, TResult> matchT3,
            Func<T4, TResult> matchT4, Func<T5, TResult> matchT5)
        {
            return SelectedIndex switch
            {
                1 => matchT1(_choice1!),
                2 => matchT2(_choice2!),
                3 => matchT3(_choice3!),
                4 => matchT4(_choice4!),
                5 => matchT5(_choice5!),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public static implicit operator Choice<T1, T2, T3, T4, T5>(T1 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5>(T2 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5>(T3 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5>(T4 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5>(T5 value) => From(value);
    }

    public readonly record struct Choice<T1, T2, T3, T4, T5, T6>
    {
        private readonly T1? _choice1 = default;
        private readonly T2? _choice2 = default;
        private readonly T3? _choice3 = default;
        private readonly T4? _choice4 = default;
        private readonly T5? _choice5 = default;
        private readonly T6? _choice6 = default;

        public int SelectedIndex { get; } = -1;

        public object Value
        {
            get
            {
                return SelectedIndex switch
                {
                    1 => _choice1!,
                    2 => _choice2!,
                    3 => _choice3!,
                    4 => _choice4!,
                    5 => _choice5!,
                    6 => _choice6!,
                    _ => throw new IndexOutOfRangeException("Unreachable code")
                };
            }
        }

        [Obsolete("Use Choice.From<T>(...)")]
        public Choice()
        {
            // throw new InvalidOperationException("Do not call this constructor");
        }

        private Choice(T1 choice1)
        {
            if (choice1 is null) throw new ArgumentNullException(nameof(choice1));
            SelectedIndex = 1;
            _choice1 = choice1;
        }

        private Choice(T2 choice2)
        {
            if (choice2 is null) throw new ArgumentNullException(nameof(choice2));
            SelectedIndex = 2;
            _choice2 = choice2;
        }

        private Choice(T3 choice3)
        {
            if (choice3 is null) throw new ArgumentNullException(nameof(choice3));
            SelectedIndex = 3;
            _choice3 = choice3;
        }

        private Choice(T4 choice4)
        {
            if (choice4 is null) throw new ArgumentNullException(nameof(choice4));
            SelectedIndex = 4;
            _choice4 = choice4;
        }

        private Choice(T5 choice5)
        {
            if (choice5 is null) throw new ArgumentNullException(nameof(choice5));
            SelectedIndex = 5;
            _choice5 = choice5;
        }

        private Choice(T6 choice6)
        {
            if (choice6 is null) throw new ArgumentNullException(nameof(choice6));
            SelectedIndex = 6;
            _choice6 = choice6;
        }

        public static Choice<T1, T2, T3, T4, T5, T6> From(T1 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5, T6> From(T2 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5, T6> From(T3 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5, T6> From(T4 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5, T6> From(T5 value) => new(value);
        public static Choice<T1, T2, T3, T4, T5, T6> From(T6 value) => new(value);

        public Type GetChosenType()
        {
            return SelectedIndex switch
            {
                1 => typeof(T1),
                2 => typeof(T2),
                3 => typeof(T3),
                4 => typeof(T4),
                5 => typeof(T5),
                6 => typeof(T6),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public bool Is<T>()
        {
            Type tType = typeof(T);

            if (tType == typeof(T1)) return SelectedIndex == 1;
            if (tType == typeof(T2)) return SelectedIndex == 2;
            if (tType == typeof(T3)) return SelectedIndex == 3;
            if (tType == typeof(T4)) return SelectedIndex == 4;
            if (tType == typeof(T5)) return SelectedIndex == 5;
            if (tType == typeof(T6)) return SelectedIndex == 6;
            return false;
        }

        public void Match(Action<T1> matchT1, Action<T2> matchT2, Action<T3> matchT3, Action<T4> matchT4, Action<T5> matchT5, Action<T6> matchT6)
        {
            switch (SelectedIndex)
            {
                case 1: matchT1(_choice1!); break;
                case 2: matchT2(_choice2!); break;
                case 3: matchT3(_choice3!); break;
                case 4: matchT4(_choice4!); break;
                case 5: matchT5(_choice5!); break;
                case 6: matchT6(_choice6!); break;
            }
        }

        public TResult Match<TResult>(Func<T1, TResult> matchT1, Func<T2, TResult> matchT2, Func<T3, TResult> matchT3,
            Func<T4, TResult> matchT4, Func<T5, TResult> matchT5, Func<T6, TResult> matchT6)
        {
            return SelectedIndex switch
            {
                1 => matchT1(_choice1!),
                2 => matchT2(_choice2!),
                3 => matchT3(_choice3!),
                4 => matchT4(_choice4!),
                5 => matchT5(_choice5!),
                6 => matchT6(_choice6!),
                _ => throw new IndexOutOfRangeException("Unreachable code")
            };
        }

        public static implicit operator Choice<T1, T2, T3, T4, T5, T6>(T1 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5, T6>(T2 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5, T6>(T3 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5, T6>(T4 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5, T6>(T5 value) => From(value);
        public static implicit operator Choice<T1, T2, T3, T4, T5, T6>(T6 value) => From(value);
    }

}
