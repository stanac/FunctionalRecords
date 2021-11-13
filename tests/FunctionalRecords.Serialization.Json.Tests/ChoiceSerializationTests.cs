using FluentAssertions;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace FunctionalRecords.Serialization.Json.Tests;

public class ChoiceSerializationTests
{
    private readonly JsonSerializerOptions _serializerOptions;

    public ChoiceSerializationTests()
    {
        _serializerOptions = new();
        foreach (JsonConverter c in Converters.AllConverters)
        {
            _serializerOptions.Converters.Add(c);
        }
    }

    [Fact]
    public void Choice2_SetTo1_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string> c1 = 1231;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string> c2 = JsonSerializer.Deserialize<Choice<int, string>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }
}
