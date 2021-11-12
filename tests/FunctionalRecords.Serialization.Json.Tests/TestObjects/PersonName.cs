using FunctionalRecords;
using System.Collections.Generic;

namespace FunctionalRecords.Serialization.Json.Tests.TestObjects;

public record PersonName : ValueRecord<(string FirstName, string LastName)>
{
    public PersonName((string FirstName, string LastName) value) : base(value)
    {
    }

    protected override IEnumerable<string> GetValidationErrors((string FirstName, string LastName) value)
    {
        if (string.IsNullOrWhiteSpace(value.FirstName))
        {
            yield return "First name not set";
        }

        if (string.IsNullOrWhiteSpace(value.LastName))
        {
            yield return "Last name not set";
        }
    }

    protected override (string FirstName, string LastName) TransformValue((string FirstName, string LastName) value)
    {
        return (value.FirstName?.Trim(), value.LastName?.Trim().ToUpper());
    }
}
