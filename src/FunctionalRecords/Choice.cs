using System;

namespace FunctionalRecords
{
    public readonly record struct Choice<T1, T2>
    {
        private readonly T1? _choice1 = default;
        private readonly T2? _choice2 = default;

        public int SelectedIndex { get; } = -1;

        [Obsolete("Use Choice.From<TType>(...)")]
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
                _ => throw new ArgumentException("Uncreachable code"),
            };
        }

        public static implicit operator Choice<T1, T2>(T1 choice1) => From(choice1);
        public static implicit operator Choice<T1, T2>(T2 choice2) => From(choice2);
    }
}
