using System;

namespace FunctionalRecords
{
    public class NotNullValue<T>
    {
        private NotNullValue(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        public T Value { get; }

        public static NotNullValue<T> From(T value) => new NotNullValue<T>(value);

        public static implicit operator NotNullValue<T>(T value) => new NotNullValue<T>(value);
    }
}
