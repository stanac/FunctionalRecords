using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal static class ChoiceConverterHelper
{
    private static JsonConverter<int> _indexConverter;

    public static int ReaderIndex(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        int index = GetIndexConverter(options).Read(ref reader, typeof(int), options);
        reader.Read(); // read comma
        return index;
    }

    public static T ReadValue<T>(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        JsonConverter<T> converter = (JsonConverter<T>)options.GetConverter(typeof(T));
        T value = converter.Read(ref reader, typeof(T), options);
        reader.Read(); // read end of array
        return value;
    }

    public static void WriteIndex(Utf8JsonWriter writer, int index, JsonSerializerOptions options)
    {
        GetIndexConverter(options).Write(writer, index, options);
    }

    public static void WriteValue<T>(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonConverter<T> converter = (JsonConverter<T>)options.GetConverter(typeof(T));
        converter.Write(writer, value, options);
    }

    private static JsonConverter<int> GetIndexConverter(JsonSerializerOptions options)
    {
        return _indexConverter ?? (_indexConverter = options.GetConverter(typeof(int)) as JsonConverter<int>);
    }
}

internal class ChoiceConverter<T1, T2> : JsonConverter<Choice<T1, T2>>
{
    public override Choice<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected StartArray");
        }

        reader.Read();
        int index = ChoiceConverterHelper.ReaderIndex(ref reader, options);

        switch (index)
        {
            case 1: return Choice<T1, T2>.From(ChoiceConverterHelper.ReadValue<T1>(ref reader, options));
            case 2: return Choice<T1, T2>.From(ChoiceConverterHelper.ReadValue<T2>(ref reader, options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void Write(Utf8JsonWriter writer, Choice<T1, T2> value, JsonSerializerOptions options)
    {
        void WriteValue<T>(T value)
        {
            ChoiceConverterHelper.WriteValue<T>(writer, value, options);
        }

        writer.WriteStartArray();
        ChoiceConverterHelper.WriteIndex(writer, value.SelectedIndex, options);
        value.Match(WriteValue, WriteValue);
        writer.WriteEndArray();
    }
}

internal class ChoiceConverter<T1, T2, T3> : JsonConverter<Choice<T1, T2, T3>>
{
    public override Choice<T1, T2, T3> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected StartArray");
        }

        reader.Read();
        int index = ChoiceConverterHelper.ReaderIndex(ref reader, options);

        switch (index)
        {
            case 1: return Choice<T1, T2, T3>.From(ChoiceConverterHelper.ReadValue<T1>(ref reader, options));
            case 2: return Choice<T1, T2, T3>.From(ChoiceConverterHelper.ReadValue<T2>(ref reader, options));
            case 3: return Choice<T1, T2, T3>.From(ChoiceConverterHelper.ReadValue<T3>(ref reader, options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void Write(Utf8JsonWriter writer, Choice<T1, T2, T3> value, JsonSerializerOptions options)
    {
        void WriteValue<T>(T value)
        {
            ChoiceConverterHelper.WriteValue<T>(writer, value, options);
        }

        writer.WriteStartArray();
        ChoiceConverterHelper.WriteIndex(writer, value.SelectedIndex, options);
        value.Match(WriteValue, WriteValue, WriteValue);
        writer.WriteEndArray();
    }
}

internal class ChoiceConverter<T1, T2, T3, T4> : JsonConverter<Choice<T1, T2, T3, T4>>
{
    public override Choice<T1, T2, T3, T4> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected StartArray");
        }

        reader.Read();
        int index = ChoiceConverterHelper.ReaderIndex(ref reader, options);

        switch (index)
        {
            case 1: return Choice<T1, T2, T3, T4>.From(ChoiceConverterHelper.ReadValue<T1>(ref reader, options));
            case 2: return Choice<T1, T2, T3, T4>.From(ChoiceConverterHelper.ReadValue<T2>(ref reader, options));
            case 3: return Choice<T1, T2, T3, T4>.From(ChoiceConverterHelper.ReadValue<T3>(ref reader, options));
            case 4: return Choice<T1, T2, T3, T4>.From(ChoiceConverterHelper.ReadValue<T4>(ref reader, options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void Write(Utf8JsonWriter writer, Choice<T1, T2, T3, T4> value, JsonSerializerOptions options)
    {
        void WriteValue<T>(T value)
        {
            ChoiceConverterHelper.WriteValue<T>(writer, value, options);
        }

        writer.WriteStartArray();
        ChoiceConverterHelper.WriteIndex(writer, value.SelectedIndex, options);
        value.Match(WriteValue, WriteValue, WriteValue, WriteValue);
        writer.WriteEndArray();
    }
}

internal class ChoiceConverter<T1, T2, T3, T4, T5> : JsonConverter<Choice<T1, T2, T3, T4, T5>>
{
    public override Choice<T1, T2, T3, T4, T5> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected StartArray");
        }

        reader.Read();
        int index = ChoiceConverterHelper.ReaderIndex(ref reader, options);

        switch (index)
        {
            case 1: return Choice<T1, T2, T3, T4, T5>.From(ChoiceConverterHelper.ReadValue<T1>(ref reader, options));
            case 2: return Choice<T1, T2, T3, T4, T5>.From(ChoiceConverterHelper.ReadValue<T2>(ref reader, options));
            case 3: return Choice<T1, T2, T3, T4, T5>.From(ChoiceConverterHelper.ReadValue<T3>(ref reader, options));
            case 4: return Choice<T1, T2, T3, T4, T5>.From(ChoiceConverterHelper.ReadValue<T4>(ref reader, options));
            case 5: return Choice<T1, T2, T3, T4, T5>.From(ChoiceConverterHelper.ReadValue<T5>(ref reader, options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void Write(Utf8JsonWriter writer, Choice<T1, T2, T3, T4, T5> value, JsonSerializerOptions options)
    {
        void WriteValue<T>(T value)
        {
            ChoiceConverterHelper.WriteValue<T>(writer, value, options);
        }

        writer.WriteStartArray();
        ChoiceConverterHelper.WriteIndex(writer, value.SelectedIndex, options);
        value.Match(WriteValue, WriteValue, WriteValue, WriteValue, WriteValue);
        writer.WriteEndArray();
    }
}

internal class ChoiceConverter<T1, T2, T3, T4, T5, T6> : JsonConverter<Choice<T1, T2, T3, T4, T5, T6>>
{
    public override Choice<T1, T2, T3, T4, T5, T6> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected StartArray");
        }

        reader.Read();
        int index = ChoiceConverterHelper.ReaderIndex(ref reader, options);

        switch (index)
        {
            case 1: return Choice<T1, T2, T3, T4, T5, T6>.From(ChoiceConverterHelper.ReadValue<T1>(ref reader, options));
            case 2: return Choice<T1, T2, T3, T4, T5, T6>.From(ChoiceConverterHelper.ReadValue<T2>(ref reader, options));
            case 3: return Choice<T1, T2, T3, T4, T5, T6>.From(ChoiceConverterHelper.ReadValue<T3>(ref reader, options));
            case 4: return Choice<T1, T2, T3, T4, T5, T6>.From(ChoiceConverterHelper.ReadValue<T4>(ref reader, options));
            case 5: return Choice<T1, T2, T3, T4, T5, T6>.From(ChoiceConverterHelper.ReadValue<T5>(ref reader, options));
            case 6: return Choice<T1, T2, T3, T4, T5, T6>.From(ChoiceConverterHelper.ReadValue<T6>(ref reader, options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public override void Write(Utf8JsonWriter writer, Choice<T1, T2, T3, T4, T5, T6> value, JsonSerializerOptions options)
    {
        void WriteValue<T>(T value)
        {
            ChoiceConverterHelper.WriteValue<T>(writer, value, options);
        }

        writer.WriteStartArray();
        ChoiceConverterHelper.WriteIndex(writer, value.SelectedIndex, options);
        value.Match(WriteValue, WriteValue, WriteValue, WriteValue, WriteValue, WriteValue);
        writer.WriteEndArray();
    }
}