using System;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace FunctionalRecords.Serialization.Json.Tests;

public class ResultSerializationTests
{
    private readonly JsonSerializerOptions _serializerOptions;

    public ResultSerializationTests()
    {
        _serializerOptions = new();
        foreach (JsonConverter c in Converters.AllConverters)
        {
            _serializerOptions.Converters.Add(c);
        }
    }

    [Theory, ClassData(typeof(ResultData))]
    public void Serialize_Deserialize_GivesEqualObject(Result r)
    {
        string json1 = JsonSerializer.Serialize(r, _serializerOptions);
        Result r2 = JsonSerializer.Deserialize<Result>(json1, _serializerOptions);
        string json2 = JsonSerializer.Serialize(r2, _serializerOptions);

        json2.Should().Be(json1);
    }

    [Theory, ClassData(typeof(ResultTData))]
    public void SerializeT_DeserializeT_GivesEqualObject(Result<int> r)
    {
        string json1 = JsonSerializer.Serialize(r, _serializerOptions);
        Result<int> r2 = JsonSerializer.Deserialize<Result<int>>(json1, _serializerOptions);
        string json2 = JsonSerializer.Serialize(r2, _serializerOptions);

        json2.Should().Be(json1);
    }

    [Theory, ClassData(typeof(ResultTAndTErrorData))]
    public void SerializeTAndTError_Deserialize_GivesEqualObject(Result<int, ResultTAndTErrorData.IntError> r)
    {
        string json1 = JsonSerializer.Serialize(r, _serializerOptions);
        Result<int, ResultTAndTErrorData.IntError> r2 = JsonSerializer.Deserialize<Result<int, ResultTAndTErrorData.IntError>>(json1, _serializerOptions);
        string json2 = JsonSerializer.Serialize(r2, _serializerOptions);

        json2.Should().Be(json1);
    }

    public class ResultData: IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (Result result in GetResults())
            {
                yield return new object[] {result};
            }
        }

        private static IEnumerable<Result> GetResults()
        {
            yield return Result.Success();
            yield return Result.Failure();
            yield return Result.Failure("a");
            yield return Result.Failure("a", "|");
            yield return Result.Failure(new List<string> { "a", "B" });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ResultTData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (Result<int> result in GetResults())
            {
                yield return new object[] { result };
            }
        }

        private static IEnumerable<Result<int>> GetResults()
        {
             yield return Result.Success(1);
             yield return Result.Failure<int>();
             yield return Result.Failure<int>("a");
             yield return Result.Failure<int>("a", "|");
             yield return Result.Failure<int>(new List<string> { "a", "B" });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ResultTAndTErrorData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (Result<int, IntError> result in GetResults())
            {
                yield return new object[] { result };
            }
        }

        private static IEnumerable<Result<int, IntError>> GetResults()
        {
           // yield return Result.Success<int, IntError>(1);
            yield return Result.Failure<int, IntError>();
            // yield return Result.Failure<int, IntError>("a");
            // yield return Result.Failure<int, IntError>("a", "|");
            // yield return Result.Failure<int, IntError>(new List<string> { "a", "B" });
            // yield return Result.Failure<int, IntError>(IntError.IntIsNegative);
            // yield return Result.Failure<int, IntError>(IntError.IntIsNegative, "a");
            // yield return Result.Failure<int, IntError>(IntError.IntIsZero, "a", "|");
            // yield return Result.Failure<int, IntError>(IntError.IntIsOne, new List<string> { "a", "B" });
            // yield return Result.Failure<int, IntError>(IntError.IntIsOne, new List<string> { "a", "B" });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public enum IntError
        {
            IntIsZero,
            IntIsOne,
            IntIsNegative
        }
    }

    public class ResultTAndTErrorFlagsData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (Result<int, IntErrorFlags> result in GetResults())
            {
                yield return new object[] { result };
            }
        }

        private static IEnumerable<Result<int, IntErrorFlags>> GetResults()
        {
            yield return Result.Success<int, IntErrorFlags>(1);
            yield return Result.Failure<int, IntErrorFlags>();
            yield return Result.Failure<int, IntErrorFlags>("a");
            yield return Result.Failure<int, IntErrorFlags>("a", "|");
            yield return Result.Failure<int, IntErrorFlags>(new List<string> { "a", "B" });
            yield return Result.Failure<int, IntErrorFlags>(IntErrorFlags.IntIsNegative);
            yield return Result.Failure<int, IntErrorFlags>(IntErrorFlags.IntIsNegative, "a");
            yield return Result.Failure<int, IntErrorFlags>(IntErrorFlags.IntIsZero, "a", "|");
            yield return Result.Failure<int, IntErrorFlags>(IntErrorFlags.IntIsOne, new List<string> { "a", "B" });
            yield return Result.Failure<int, IntErrorFlags>(IntErrorFlags.IntIsOne | IntErrorFlags.IntIsNegative, new List<string> { "a", "B" });
            yield return Result.Failure<int, IntErrorFlags>(IntErrorFlags.IntIsOne | IntErrorFlags.IntIsNegative, new List<string> { "a", "B" });
            yield return Result.Failure<int, IntErrorFlags>(IntErrorFlags.IntIsOne | IntErrorFlags.IntIsNegative | IntErrorFlags.IntIsZero, new List<string> { "a", "B" });
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [Flags]
        public enum IntErrorFlags
        {
            IntIsZero,
            IntIsOne,
            IntIsNegative
        }
    }
}
