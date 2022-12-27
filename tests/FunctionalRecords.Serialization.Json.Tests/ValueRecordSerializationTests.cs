using FluentAssertions;
using FunctionalRecords.Serialization.Json.Tests.TestObjects;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace FunctionalRecords.Serialization.Json.Tests;

public class ValueRecordSerializationTests
{
    private readonly JsonSerializerOptions _serializerOptions;

    public ValueRecordSerializationTests()
    {
        _serializerOptions = new();
        foreach (JsonConverter c in Converters.AllConverters)
        {
            _serializerOptions.Converters.Add(c);
        }
    }

    [Fact]
    public void PersonName_Serialize_Deserialize_GivesEqualObjects()
    {
        (string firstName, string lastName) tpl1 = ("Jane", "Doe");
        string j = JsonSerializer.Serialize(tpl1, _serializerOptions);

        PersonName pn1 = new PersonName(("Jane", "Doe"));
        string json = JsonSerializer.Serialize(pn1, _serializerOptions);
        PersonName pn2 = JsonSerializer.Deserialize<PersonName>(json, _serializerOptions);

        pn2.Should().Be(pn1);
    }
}
