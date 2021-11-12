using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal static class ValueTupleConverterHelper
{
    public static void Write<T>(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonConverter<T> valueConverter = options.GetConverter(typeof(T)) as JsonConverter<T>;
        valueConverter.Write(writer, value, options);
    }

    public static T Read<T>(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        JsonConverter<T> valueConverter = options.GetConverter(typeof(T)) as JsonConverter<T>;
        T value = valueConverter.Read(ref reader, typeof(T), options);
        reader.Read(); // read end array or comma
        return value;
    }
}

internal class ValueTupleConverter<T1> : JsonConverter<ValueTuple<T1>>
{
    public override ValueTuple<T1> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected ValueTuple to be serialized to JsonArray");
        }

        reader.Read();

        T1 value = ValueTupleConverterHelper.Read<T1>(ref reader, options);
        return ValueTuple.Create(value);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        ValueTupleConverterHelper.Write(writer, value.Item1, options);
        writer.WriteEndArray();
    }
}

internal class ValueTupleConverter<T1, T2> : JsonConverter<ValueTuple<T1, T2>>
{
    public override ValueTuple<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected ValueTuple to be serialized to JsonArray");
        }

        reader.Read();

        T1 value1 = ValueTupleConverterHelper.Read<T1>(ref reader, options);
        T2 value2 = ValueTupleConverterHelper.Read<T2>(ref reader, options);
        return ValueTuple.Create(value1, value2);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        ValueTupleConverterHelper.Write(writer, value.Item1, options);
        ValueTupleConverterHelper.Write(writer, value.Item2, options);
        writer.WriteEndArray();
    }
}

internal class ValueTupleConverter<T1, T2, T3> : JsonConverter<ValueTuple<T1, T2, T3>>
{
    public override ValueTuple<T1, T2, T3> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected ValueTuple to be serialized to JsonArray");
        }

        reader.Read();

        T1 value1 = ValueTupleConverterHelper.Read<T1>(ref reader, options);
        T2 value2 = ValueTupleConverterHelper.Read<T2>(ref reader, options);
        T3 value3 = ValueTupleConverterHelper.Read<T3>(ref reader, options);
        return ValueTuple.Create(value1, value2, value3);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2, T3> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        ValueTupleConverterHelper.Write(writer, value.Item1, options);
        ValueTupleConverterHelper.Write(writer, value.Item2, options);
        ValueTupleConverterHelper.Write(writer, value.Item3, options);
        writer.WriteEndArray();
    }
}

internal class ValueTupleConverter<T1, T2, T3, T4> : JsonConverter<ValueTuple<T1, T2, T3, T4>>
{
    public override ValueTuple<T1, T2, T3, T4> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected ValueTuple to be serialized to JsonArray");
        }

        reader.Read();

        T1 value1 = ValueTupleConverterHelper.Read<T1>(ref reader, options);
        T2 value2 = ValueTupleConverterHelper.Read<T2>(ref reader, options);
        T3 value3 = ValueTupleConverterHelper.Read<T3>(ref reader, options);
        T4 value4 = ValueTupleConverterHelper.Read<T4>(ref reader, options);
        return ValueTuple.Create(value1, value2, value3, value4);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2, T3, T4> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        ValueTupleConverterHelper.Write(writer, value.Item1, options);
        ValueTupleConverterHelper.Write(writer, value.Item2, options);
        ValueTupleConverterHelper.Write(writer, value.Item3, options);
        ValueTupleConverterHelper.Write(writer, value.Item4, options);
        writer.WriteEndArray();
    }
}

internal class ValueTupleConverter<T1, T2, T3, T4, T5> : JsonConverter<ValueTuple<T1, T2, T3, T4, T5>>
{
    public override ValueTuple<T1, T2, T3, T4, T5> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected ValueTuple to be serialized to JsonArray");
        }

        reader.Read();

        T1 value1 = ValueTupleConverterHelper.Read<T1>(ref reader, options);
        T2 value2 = ValueTupleConverterHelper.Read<T2>(ref reader, options);
        T3 value3 = ValueTupleConverterHelper.Read<T3>(ref reader, options);
        T4 value4 = ValueTupleConverterHelper.Read<T4>(ref reader, options);
        T5 value5 = ValueTupleConverterHelper.Read<T5>(ref reader, options);
        return ValueTuple.Create(value1, value2, value3, value4, value5);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2, T3, T4, T5> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        ValueTupleConverterHelper.Write(writer, value.Item1, options);
        ValueTupleConverterHelper.Write(writer, value.Item2, options);
        ValueTupleConverterHelper.Write(writer, value.Item3, options);
        ValueTupleConverterHelper.Write(writer, value.Item4, options);
        ValueTupleConverterHelper.Write(writer, value.Item5, options);
        writer.WriteEndArray();
    }
}

internal class ValueTupleConverter<T1, T2, T3, T4, T5, T6> : JsonConverter<ValueTuple<T1, T2, T3, T4, T5, T6>>
{
    public override ValueTuple<T1, T2, T3, T4, T5, T6> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected ValueTuple to be serialized to JsonArray");
        }

        reader.Read();

        T1 value1 = ValueTupleConverterHelper.Read<T1>(ref reader, options);
        T2 value2 = ValueTupleConverterHelper.Read<T2>(ref reader, options);
        T3 value3 = ValueTupleConverterHelper.Read<T3>(ref reader, options);
        T4 value4 = ValueTupleConverterHelper.Read<T4>(ref reader, options);
        T5 value5 = ValueTupleConverterHelper.Read<T5>(ref reader, options);
        T6 value6 = ValueTupleConverterHelper.Read<T6>(ref reader, options);
        return ValueTuple.Create(value1, value2, value3, value4, value5, value6);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        ValueTupleConverterHelper.Write(writer, value.Item1, options);
        ValueTupleConverterHelper.Write(writer, value.Item2, options);
        ValueTupleConverterHelper.Write(writer, value.Item3, options);
        ValueTupleConverterHelper.Write(writer, value.Item4, options);
        ValueTupleConverterHelper.Write(writer, value.Item5, options);
        ValueTupleConverterHelper.Write(writer, value.Item6, options);
        writer.WriteEndArray();
    }
}

internal class ValueTupleConverter<T1, T2, T3, T4, T5, T6, T7> : JsonConverter<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>
{
    public override ValueTuple<T1, T2, T3, T4, T5, T6, T7> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected ValueTuple to be serialized to JsonArray");
        }

        reader.Read();

        T1 value1 = ValueTupleConverterHelper.Read<T1>(ref reader, options);
        T2 value2 = ValueTupleConverterHelper.Read<T2>(ref reader, options);
        T3 value3 = ValueTupleConverterHelper.Read<T3>(ref reader, options);
        T4 value4 = ValueTupleConverterHelper.Read<T4>(ref reader, options);
        T5 value5 = ValueTupleConverterHelper.Read<T5>(ref reader, options);
        T6 value6 = ValueTupleConverterHelper.Read<T6>(ref reader, options);
        T7 value7 = ValueTupleConverterHelper.Read<T7>(ref reader, options);
        return ValueTuple.Create(value1, value2, value3, value4, value5, value6, value7);
    }

    public override void Write(Utf8JsonWriter writer, ValueTuple<T1, T2, T3, T4, T5, T6, T7> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        ValueTupleConverterHelper.Write(writer, value.Item1, options);
        ValueTupleConverterHelper.Write(writer, value.Item2, options);
        ValueTupleConverterHelper.Write(writer, value.Item3, options);
        ValueTupleConverterHelper.Write(writer, value.Item4, options);
        ValueTupleConverterHelper.Write(writer, value.Item5, options);
        ValueTupleConverterHelper.Write(writer, value.Item6, options);
        ValueTupleConverterHelper.Write(writer, value.Item7, options);
        writer.WriteEndArray();
    }
}
