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
        _serializerOptions.WriteIndented = true;
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

        c1.Should().BeEquivalentTo(c2);
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

    [Fact]
    public void PrimitiveChoice2SerializedAsArray_CanBeDeserialized()
    {
        string json = @"
            [
              2,
              ""ABC""
            ]
        ";

        Choice<int, string> c1 = JsonSerializer.Deserialize<Choice<int, string>>(json, _serializerOptions);

        c1.Is<string>().Should().BeTrue();
        c1.Value.Should().Be("ABC");
    }

    [Fact]
    public void PrimitiveChoice3SerializedAsArray_CanBeDeserialized()
    {
        string json = @"
            [
              3,
              0.0
            ]
        ";

        Choice<int, string, float> c1 = JsonSerializer.Deserialize<Choice<int, string, float>>(json, _serializerOptions);

        c1.Is<float>().Should().BeTrue();
        c1.Value.Should().Be(0f);
    }

    [Fact]
    public void PrimitiveChoice4SerializedAsArray_CanBeDeserialized()
    {
        string json = @"
            [
              4,
              ""3351023f-8a54-46f8-adb7-19de554df9b0""
            ]
        ";

        Choice<int, string, float, Guid> c1 = JsonSerializer.Deserialize<Choice<int, string, float, Guid>>(json, _serializerOptions);

        c1.Is<Guid>().Should().BeTrue();
        c1.Value.ToString().Should().Be("3351023f-8a54-46f8-adb7-19de554df9b0");
    }

    [Fact]
    public void PrimitiveChoice5SerializedAsArray_CanBeDeserialized()
    {
        string json = @"
            [
              5,
              ""2022-11-14T18:49:05.4542558Z""
            ]
        ";
        
        Choice<int, string, float, Guid, DateTime> c1 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime>>(json, _serializerOptions);

        c1.Is<DateTime>().Should().BeTrue();
        ((DateTime) c1.Value).Day.Should().Be(14);
        ((DateTime) c1.Value).Hour.Should().Be(18);
    }

    [Fact]
    public void PrimitiveChoice6SerializedAsArray_CanBeDeserialized()
    {
        string json = @"
            [
              6,
              ""2022-11-14T18:49:05.4542558Z""
            ]
        ";

        Choice<int, string, float, Guid, DateTime, DateTimeOffset> c1 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime, DateTimeOffset>>(json, _serializerOptions);

        c1.Is<DateTimeOffset>().Should().BeTrue();
        ((DateTimeOffset)c1.Value).Day.Should().Be(14);
        ((DateTimeOffset)c1.Value).Hour.Should().Be(18);
    }

    [Fact]
    public void ComplexChoice2SerializedAsArray_CanBeDeserialized()
    {
        Choice<ChoiceTypeA, ChoiceTypeB> c = new ChoiceTypeB { BValue = 98 };

        string s = JsonSerializer.Serialize(c, _serializerOptions);

        string json = @"[
          2,
          {
            ""BValue"": 98
          }
        ]";

        Choice<ChoiceTypeA, ChoiceTypeB> c1 = JsonSerializer.Deserialize<Choice<ChoiceTypeA, ChoiceTypeB>>(json, _serializerOptions);

        c1.Is<ChoiceTypeB>().Should().BeTrue();
        c1.Value.Should().BeOfType<ChoiceTypeB>();
        ((ChoiceTypeB) c1.Value).BValue.Should().Be(98);
    }

    [Fact]
    public void PrimitiveChoice2SerializedAsObject_CanBeDeserialized()
    {
        string json = @"
        {
          ""$choiceType"": ""String"",
          ""value"": ""ABC""
        }";

        Choice<int, string> c1 = JsonSerializer.Deserialize<Choice<int, string>>(json, _serializerOptions);

        string s = JsonSerializer.Serialize(c1, _serializerOptions);

        c1.Is<string>().Should().BeTrue();
        c1.Value.Should().Be("ABC");
    }

    [Fact]
    public void PrimitiveChoice3SerializedAsObject_CanBeDeserialized()
    {
        string json = @"
        {
          ""$choiceType"": ""Single"",
          ""value"": 1.0
        }";
        
        Choice<int, string, float> c1 = JsonSerializer.Deserialize<Choice<int, string, float>>(json, _serializerOptions);

        c1.Is<float>().Should().BeTrue();
        c1.Value.Should().Be(1f);
    }

    [Fact]
    public void PrimitiveChoice4SerializedAsObject_CanBeDeserialized()
    {
        string json = @"
        {
          ""$choiceType"": ""Guid"",
          ""value"": ""3351023f-8a54-46f8-adb7-19de554df9b0""
        }";

        Choice<int, string, float, Guid> c1 = JsonSerializer.Deserialize<Choice<int, string, float, Guid>>(json, _serializerOptions);

        c1.Is<Guid>().Should().BeTrue();
        c1.Value.ToString().Should().Be("3351023f-8a54-46f8-adb7-19de554df9b0");
    }

    [Fact]
    public void PrimitiveChoice5SerializedAsObject_CanBeDeserialized()
    {
        string json = @"
        {
          ""$choiceType"": ""DateTime"",
          ""value"": ""2022-11-14T18:49:05.4542558Z""
        }";
        
        Choice<int, string, float, Guid, DateTime> c1 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime>>(json, _serializerOptions);

        c1.Is<DateTime>().Should().BeTrue();
        ((DateTime)c1.Value).Day.Should().Be(14);
        ((DateTime)c1.Value).Hour.Should().Be(18);
    }

    [Fact]
    public void PrimitiveChoice6SerializedAsObject_CanBeDeserialized()
    {
        string json = @"
        {
          ""$choiceType"": ""DateTimeOffset"",
          ""value"": ""2022-11-14T18:49:05.4542558Z""
        }";
        
        Choice<int, string, float, Guid, DateTime, DateTimeOffset> c1 = JsonSerializer.Deserialize<Choice<int, string, float, Guid, DateTime, DateTimeOffset>>(json, _serializerOptions);

        c1.Is<DateTimeOffset>().Should().BeTrue();
        ((DateTimeOffset)c1.Value).Day.Should().Be(14);
        ((DateTimeOffset)c1.Value).Hour.Should().Be(18);
    }

    [Fact]
    public void ComplexChoice2SerializedAsObject_CanBeDeserialized()
    {
        Choice<ChoiceTypeA, ChoiceTypeB> c = new ChoiceTypeB { BValue = 98 };

        string s = JsonSerializer.Serialize(c, _serializerOptions);

        string json = @"
        {
          ""$choiceType"": ""ChoiceTypeB"",
          ""value"": { ""BValue"": 98 }
        }";

        Choice<ChoiceTypeA, ChoiceTypeB> c1 = JsonSerializer.Deserialize<Choice<ChoiceTypeA, ChoiceTypeB>>(json, _serializerOptions);

        c1.Is<ChoiceTypeB>().Should().BeTrue();
        c1.Value.Should().BeOfType<ChoiceTypeB>();
        ((ChoiceTypeB)c1.Value).BValue.Should().Be(98);
    }

}
