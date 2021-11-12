using FluentAssertions;
using FunctionalRecords;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace FunctionalRecords.Serialization.Json.Tests
{
    public class MaybeConverterTests
    {
        private readonly JsonSerializerOptions _serializerOptions;

        public MaybeConverterTests()
        {
            _serializerOptions = new();
            foreach (JsonConverter c in Converters.AllConverters)
            {
                _serializerOptions.Converters.Add(c);
            }
        }

        [Fact]
        public void NestedMaybeObject_Serialize_Deserialize_ReturnsEqualObject()
        {
            TestMaybeObject t1 = new()
            {
                B = true,
                I = 44,
                S = "1234"
            };

            string json = JsonSerializer.Serialize(t1, _serializerOptions);

            TestMaybeObject t2 = JsonSerializer.Deserialize<TestMaybeObject>(json, _serializerOptions);

            t2.Should().BeEquivalentTo(t1);
        }

        [Fact]
        public void MaybeNestedMaybeObject_Serialize_Deserialize_ReturnsEqualObject()
        {
            Maybe<TestMaybeObject> t1 = new TestMaybeObject()
            {
                B = true,
                I = 44,
                S = "1234"
            };

            string json = JsonSerializer.Serialize(t1, _serializerOptions);

            Maybe<TestMaybeObject> t2 = JsonSerializer.Deserialize<Maybe<TestMaybeObject>>(json, _serializerOptions);

            t2.IsSome.Should().BeTrue();
            t2.Value.Should().BeEquivalentTo(t1.Value);
        }

        [Fact]
        public void MaybeNestedMaybeObjectNone_Serialize_Deserialize_ReturnsEqualObject()
        {
            Maybe<TestMaybeObject> t1 = Maybe<TestMaybeObject>.None;

            string json = JsonSerializer.Serialize(t1, _serializerOptions);

            Maybe<TestMaybeObject> t2 = JsonSerializer.Deserialize<Maybe<TestMaybeObject>>(json, _serializerOptions);

            t2.IsSome.Should().BeFalse();
        }

        [Fact]
        public void SimpleMaybeObject_Serialize_Deserialize_ReturnsEqualObject()
        {
            Maybe<string> s1 = "123";

            string json = JsonSerializer.Serialize(s1, _serializerOptions);

            Maybe<string> s2 = JsonSerializer.Deserialize<Maybe<string>>(json, _serializerOptions);

            s2.Should().BeEquivalentTo(s1);
        }

        public class TestMaybeObject
        {
            public int I { get; set; }
            public Maybe<string> S { get; set; }
            public bool B { get; set; }
        }
    }
}