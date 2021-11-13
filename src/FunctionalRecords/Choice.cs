using System;

namespace FunctionalRecords
{
    public readonly record struct Choice<T1, T2>
    {
        public int SelectedIndex { get; } = -1;

        [Obsolete("Use Choice.From<TType>(...)")]
        public Choice()
        {
            // throw new InvalidOperationException("Do not call this constructor");
        }

        private Choice(NotNullValue<T1> choice1)
        {
            if (choice1 is null) throw new ArgumentNullException(nameof(choice1));
            SelectedIndex = 1;
            Choice1 = choice1.Value;
        }

        private Choice(NotNullValue<T2> choice2)
        {
            if (choice2 is null) throw new ArgumentNullException(nameof(choice2));
            SelectedIndex = 2;
            Choice2 = choice2.Value;
        }

        public Maybe<T1> Choice1 { get; } = Maybe<T1>.None;
        public Maybe<T2> Choice2 { get; } = Maybe<T2>.None;

        public static Choice<T1, T2> From(T1 value) => new Choice<T1, T2>(value);
        public static Choice<T1, T2> From(T2 value) => new Choice<T1, T2>(value);

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
                case 1: matchT1(Choice1.Value); break;
                case 2: matchT2(Choice2.Value); break;
            }
        }

        public TResult Match<TResult>(Func<T1, TResult> matchT1, Func<T2, TResult> matchT2)
        {
            return SelectedIndex switch
            {
                1 => matchT1(Choice1.Value),
                2 => matchT2(Choice2.Value),
                _ => throw new ArgumentException("Uncreachable code"),
            };
        }

        public static implicit operator Choice<T1, T2>(T1 choice1) => From(choice1);
        public static implicit operator Choice<T1, T2>(T2 choice2) => From(choice2);
    }
}
