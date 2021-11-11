using System.Collections.Generic;

namespace FunctionalRecords.Tests;

public record PositiveInteger : ValueRecord<int, PositiveInteger>
{
    public PositiveInteger(int value) : base(value)
    {
    }

    protected override IEnumerable<string> GetValidationErrors(int value)
    {
        if (value < 1)
        {
            yield return "Value cannot be less than 1";
        }
    }

    public static implicit operator PositiveInteger (int i) => new(i);
}
