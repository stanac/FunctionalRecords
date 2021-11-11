using FluentAssertions;
using FunctionalRecords;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace FunctionRecords.Serialization.Json.Tests;

public class ResultConverterTests
{
    private readonly JsonSerializerOptions _serializerOptions;

    public ResultConverterTests()
    {
        _serializerOptions = new();
        foreach (JsonConverter c in Converters.AllConverters)
        {
            _serializerOptions.Converters.Add(c);
        }
    }

    [Fact]
    public void ResultFailureEmpty_Serialize_Deserialize_GivesEqualObjects()
    {
        Result r1 = Result.Failure();
        string json = JsonSerializer.Serialize(r1, _serializerOptions);
        Result r2 = JsonSerializer.Deserialize<Result>(json, _serializerOptions);

        r2.IsSuccess.Should().BeFalse();
        r2.Exception.IsNone.Should().BeTrue();
        r2.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ResultTFailureEmpty_Serialize_Deserialize_GivesEqualObjects()
    {
        Result<int> r1 = Result.Failure<int>();
        string json = JsonSerializer.Serialize(r1, _serializerOptions);
        Result<int> r2 = JsonSerializer.Deserialize<Result<int>>(json, _serializerOptions);

        r2.IsSuccess.Should().BeFalse();
        r2.Exception.IsNone.Should().BeTrue();
        r2.Errors.Should().BeEmpty();
    }

    [Fact]
    public void ResultFailureWithErrorsAndException_Serialize_Deserialize_GivesEqualObjects()
    {
        Exception ex = new InvalidOperationException("something");
        Result r1 = Result.Failure(ex, "1", "2");
        string json = JsonSerializer.Serialize(r1, _serializerOptions);
        Result r2 = JsonSerializer.Deserialize<Result>(json, _serializerOptions);

        r2.IsSuccess.Should().BeFalse();
        r2.Errors.Should().HaveCount(2);
        r2.Errors[0].Should().Be("1");
        r2.Errors[1].Should().Be("2");
    }

    [Fact]
    public void ResultTFailureWithErrorsAndException_Serialize_Deserialize_GivesEqualObjects()
    {
        Exception ex = new InvalidOperationException("something");
        Result<int> r1 = Result.Failure<int>(ex, "1", "2");
        string json = JsonSerializer.Serialize(r1, _serializerOptions);
        Result<int> r2 = JsonSerializer.Deserialize<Result<int>>(json, _serializerOptions);

        r2.IsSuccess.Should().BeFalse();
        r2.Errors.Should().HaveCount(2);
        r2.Errors[0].Should().Be("1");
        r2.Errors[1].Should().Be("2");
    }

    [Fact]
    public void ResultSuccess_Serialize_Deserialize_GivesEqualObjects()
    {
        Result r1 = Result.Success();
        string json = JsonSerializer.Serialize(r1, _serializerOptions);
        Result r2 = JsonSerializer.Deserialize<Result>(json, _serializerOptions);

        r2.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void ResultTSuccess_Serialize_Deserialize_GivesEqualObjects()
    {
        Result<int> r1 = Result.Success(4);
        string json = JsonSerializer.Serialize(r1, _serializerOptions);
        Result<int> r2 = JsonSerializer.Deserialize<Result<int>>(json, _serializerOptions);

        r2.IsSuccess.Should().BeTrue();
        r2.Value.ValueOrDefault.Should().Be(4);
    }
}
