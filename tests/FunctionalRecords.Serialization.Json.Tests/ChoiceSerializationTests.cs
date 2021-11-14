using FluentAssertions;
using FunctionalRecords.Serialization.Json.Tests.TestObjects;
using System;
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

    [Fact]
    public void Choice2_SetTo2_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string> c1 = "a";
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string> c2 = JsonSerializer.Deserialize<Choice<int, string>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice3_SetTo1_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float> c1 = 1231;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float> c2 = JsonSerializer.Deserialize<Choice<int, string, float>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice3_SetTo2_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float> c1 = "a";
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float> c2 = JsonSerializer.Deserialize<Choice<int, string, float>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice3_SetTo3_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float> c1 = 0.1f;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float> c2 = JsonSerializer.Deserialize<Choice<int, string, float>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice4_SetTo1_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid> c1 = 1231;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice4_SetTo2_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid> c1 = "a";
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice4_SetTo3_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid> c1 = 0.1f;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice4_SetTo4_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid> c1 = Guid.NewGuid();
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice5_SetTo1_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime> c1 = 1231;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice5_SetTo2_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime> c1 = "a";
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice5_SetTo3_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime> c1 = 0.1f;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice5_SetTo4_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime> c1 = Guid.NewGuid();
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice5_SetTo5_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime> c1 = DateTime.Now;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice6_SetTo1_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime, bool> c1 = 1231;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime, bool> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime, bool>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice6_SetTo2_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime, bool> c1 = "a";
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime, bool> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime, bool>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice6_SetTo3_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime, bool> c1 = 0.1f;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime, bool> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime, bool>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice6_SetTo4_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime, bool> c1 = Guid.NewGuid();
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime, bool> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime, bool>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice6_SetTo5_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime, bool> c1 = DateTime.Now;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime, bool> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime, bool>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void Choice6_SetTo6_Serialize_Deserialize_GivesEqualValue()
    {
        Choice<int, string, float, Guid, DateTime, bool> c1 = true;
        string json = JsonSerializer.Serialize(c1, _serializerOptions);
        Choice<int, string, float, Guid, DateTime, bool> c2 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime, bool>>(json, _serializerOptions);

        (c1 == c2).Should().BeTrue();
    }

    [Fact]
    public void ObjectWithMultipleChoiceProperties_Serialize_Deserialize_GivesEqualValue()
    {
        ChoiceTestObject obj1 = ChoiceTestObject.CreateTestObject();
        string json = JsonSerializer.Serialize(obj1, _serializerOptions);
        ChoiceTestObject obj2 = JsonSerializer.Deserialize<ChoiceTestObject>(json, _serializerOptions);

        obj2.Should().BeEquivalentTo(obj1);
    }
}
