using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalRecords;

public abstract record ValueRecord<T>
{
    public T Value { get; }

    [Obsolete("Use constructor ValueRecord(T value)")]
    public ValueRecord()
    {
    }

    protected ValueRecord(T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        // ReSharper disable once VirtualMemberCallInConstructor
        Value = TransformValue(value);
        Validate(Value);
    }

    protected abstract IEnumerable<string> GetValidationErrors(T value);

    protected virtual void AfterSuccessfulValidation()
    {
    }

    protected void Validate(T value)
    {
        List<string> errors = GetValidationErrors(value).ToList();

        if (errors.Any())
        {
            string error = $"Value object {GetType().Name} validation error";
            if (errors.Count > 1)
            {
                error += "s";
            }

            error += ": " + string.Join("; ", errors);
            throw new ValidationException(GetType().Name, error);
        }

        AfterSuccessfulValidation();
    }

    protected virtual T TransformValue(T value) => value;
}
