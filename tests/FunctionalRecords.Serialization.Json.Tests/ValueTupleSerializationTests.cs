using FluentAssertions;
using FunctionalRecords.Serialization.Json.Tests.TestObjects;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace FunctionalRecords.Serialization.Json.Tests;

public class ValueTupleSerializationTests
{
    private readonly JsonSerializerOptions _serializerOptions;

    public ValueTupleSerializationTests()
    {
        _serializerOptions = new();
        foreach (JsonConverter c in Converters.AllConverters)
        {
            _serializerOptions.Converters.Add(c);
        }
    }

    [Fact]
    public void ValueTuple1_Serialize_Deserialize_GivesEqualObjects()
    {
        TupleTestObject<Guid> t1 = TupleTestObject.Create1();
        string json = JsonSerializer.Serialize(t1, _serializerOptions);
        TupleTestObject<Guid> t2 = JsonSerializer.Deserialize<TupleTestObject<Guid>>(json, _serializerOptions);

        t2.Should().BeEquivalentTo(t1);
    }

    [Fact]
    public void ValueTuple2_Serialize_Deserialize_GivesEqualObjects()
    {
        TupleTestObject<Guid, string> t1 = TupleTestObject.Create2();
        string json = JsonSerializer.Serialize(t1, _serializerOptions);
        var t2 = JsonSerializer.Deserialize<TupleTestObject<Guid, string>>(json, _serializerOptions);

        t2.Should().BeEquivalentTo(t1);
    }

    [Fact]
    public void ValueTuple3_Serialize_Deserialize_GivesEqualObjects()
    {
        TupleTestObject<Guid, string, int> t1 = TupleTestObject.Create3();
        string json = JsonSerializer.Serialize(t1, _serializerOptions);
        var t2 = JsonSerializer.Deserialize<TupleTestObject<Guid, string, int>>(json, _serializerOptions);

        t2.Should().BeEquivalentTo(t1);
    }

    [Fact]
    public void ValueTuple4_Serialize_Deserialize_GivesEqualObjects()
    {
        TupleTestObject<Guid, string, int, bool> t1 = TupleTestObject.Create4();
        string json = JsonSerializer.Serialize(t1, _serializerOptions);
        var t2 = JsonSerializer.Deserialize<TupleTestObject<Guid, string, int, bool>>(json, _serializerOptions);

        t2.Should().BeEquivalentTo(t1);
    }

    [Fact]
    public void ValueTuple5_Serialize_Deserialize_GivesEqualObjects()
    {
        TupleTestObject<Guid, string, int, bool, long> t1 = TupleTestObject.Create5();
        string json = JsonSerializer.Serialize(t1, _serializerOptions);
        var t2 = JsonSerializer.Deserialize<TupleTestObject<Guid, string, int, bool, long>>(json, _serializerOptions);

        t2.Should().BeEquivalentTo(t1);
    }

    [Fact]
    public void ValueTuple6_Serialize_Deserialize_GivesEqualObjects()
    {
        TupleTestObject<Guid, string, int, bool, long, string> t1 = TupleTestObject.Create6();
        string json = JsonSerializer.Serialize(t1, _serializerOptions);
        var t2 = JsonSerializer.Deserialize<TupleTestObject<Guid, string, int, bool, long, string>>(json, _serializerOptions);

        t2.Should().BeEquivalentTo(t1);
    }

    [Fact]
    public void ValueTuple7_Serialize_Deserialize_GivesEqualObjects()
    {
        TupleTestObject<Guid, string, int, bool, long, string, string> t1 = TupleTestObject.Create7();
        string json = JsonSerializer.Serialize(t1, _serializerOptions);
        var t2 = JsonSerializer.Deserialize<TupleTestObject<Guid, string, int, bool, long, string, string>>(json, _serializerOptions);

        t2.Should().BeEquivalentTo(t1);
    }
}