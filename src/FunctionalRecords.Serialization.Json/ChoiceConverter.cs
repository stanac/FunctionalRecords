using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace FunctionalRecords.Serialization.Json;

internal static class ChoiceConverterHelper
{
    private static JsonConverter<int> _intConverter;
    private static JsonConverter<string> _stringConverter;
    private static JsonConverter<JsonElement> _elementConverter;

    public const string TypeNamePropertyName = "$choiceType";
    public const string ValuePropertyName = "value";

    public static int ReaderIndex(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        int index = GetIndexConverter(options).Read(ref reader, typeof(int), options);
        reader.Read(); // read comma
        return index;
    }

    public static string ReaderTypeName(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        reader.Read(); // read prop name
        string type = GetTypeNameConverter(options).Read(ref reader, typeof(string), options);
        return type;
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

    public static void WriteType(Utf8JsonWriter writer, IChoice choice, JsonSerializerOptions options)
    {
        JsonConverter<string> converter = (JsonConverter<string>)options.GetConverter(typeof(string));
        converter.Write(writer, choice.SelectedTypeName, options);
    }
    
    private static JsonConverter<int> GetIndexConverter(JsonSerializerOptions options)
    {
        return _intConverter ??= options.GetConverter(typeof(int)) as JsonConverter<int>;
    }

    private static JsonConverter<string> GetTypeNameConverter(JsonSerializerOptions options)
    {
        return _stringConverter ??= options.GetConverter(typeof(string)) as JsonConverter<string>;
    }

    private static JsonConverter<JsonElement> GetElementConverter(JsonSerializerOptions options)
    {
        return _elementConverter ??= options.GetConverter(typeof(JsonElement)) as JsonConverter<JsonElement>;
    }

    public static JsonElement ReadChoiceElement(ref Utf8JsonReader reader, JsonSerializerOptions options)
    {
        JsonConverter<JsonElement> converter = GetElementConverter(options);
        return converter.Read(ref reader, typeof(JsonElement), options);
    }

    public static (string typeName, JsonElement value) GetChoiceTypeAndValue(JsonElement element)
    {
        // ReSharper disable PossibleMultipleEnumeration

        JsonProperty[] props = element.EnumerateObject().ToArray();


        string type;
        var typeProperty = props.Where(x => x.Name == TypeNamePropertyName);

        if (typeProperty.Any())
        {
            type = typeProperty.First().Value.GetString();
        }
        else
        {
            throw new JsonException($"Property `{TypeNamePropertyName}` not found");
        }

        JsonElement value;
        var valueProperty = props.Where(x => x.Name == ValuePropertyName);
        if (valueProperty.Any())
        {
            value = valueProperty.First().Value;
        }
        else
        {
            throw new JsonException($"Property `{ValuePropertyName}` not found");
        }

        return (type, value);
        
        // ReSharper restore PossibleMultipleEnumeration
    }

    public static Choice<T1, T2> ConvertToChoiceFromObject<T1, T2>(JsonElement element, JsonSerializerOptions options)
    {
        (string typeName, JsonElement value) = GetChoiceTypeAndValue(element);

        Type t = TypeName.GetTypeFromName(typeName, typeof(T1), typeof(T2));

        if (t == typeof(T1)) return Choice<T1, T2>.From(value.Deserialize<T1>(options));
        if (t == typeof(T2)) return Choice<T1, T2>.From(value.Deserialize<T2>(options));

        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2, T3> ConvertToChoiceFromObject<T1, T2, T3>(JsonElement element, JsonSerializerOptions options)
    {
        (string typeName, JsonElement value) = GetChoiceTypeAndValue(element);

        Type t = TypeName.GetTypeFromName(typeName, typeof(T1), typeof(T2), typeof(T3));

        if (t == typeof(T1)) return Choice<T1, T2, T3>.From(value.Deserialize<T1>(options));
        if (t == typeof(T2)) return Choice<T1, T2, T3>.From(value.Deserialize<T2>(options));
        if (t == typeof(T3)) return Choice<T1, T2, T3>.From(value.Deserialize<T3>(options));
        
        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2, T3, T4> ConvertToChoiceFromObject<T1, T2, T3, T4>(JsonElement element, JsonSerializerOptions options)
    {
        (string typeName, JsonElement value) = GetChoiceTypeAndValue(element);

        Type t = TypeName.GetTypeFromName(typeName, typeof(T1), typeof(T2), typeof(T3), typeof(T4));

        if (t == typeof(T1)) return Choice<T1, T2, T3, T4>.From(value.Deserialize<T1>(options));
        if (t == typeof(T2)) return Choice<T1, T2, T3, T4>.From(value.Deserialize<T2>(options));
        if (t == typeof(T3)) return Choice<T1, T2, T3, T4>.From(value.Deserialize<T3>(options));
        if (t == typeof(T4)) return Choice<T1, T2, T3, T4>.From(value.Deserialize<T4>(options));

        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2, T3, T4, T5> ConvertToChoiceFromObject<T1, T2, T3, T4, T5>(JsonElement element, JsonSerializerOptions options)
    {
        (string typeName, JsonElement value) = GetChoiceTypeAndValue(element);

        Type t = TypeName.GetTypeFromName(typeName, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

        if (t == typeof(T1)) return Choice<T1, T2, T3, T4, T5>.From(value.Deserialize<T1>(options));
        if (t == typeof(T2)) return Choice<T1, T2, T3, T4, T5>.From(value.Deserialize<T2>(options));
        if (t == typeof(T3)) return Choice<T1, T2, T3, T4, T5>.From(value.Deserialize<T3>(options));
        if (t == typeof(T4)) return Choice<T1, T2, T3, T4, T5>.From(value.Deserialize<T4>(options));
        if (t == typeof(T5)) return Choice<T1, T2, T3, T4, T5>.From(value.Deserialize<T5>(options));

        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2, T3, T4, T5, T6> ConvertToChoiceFromObject<T1, T2, T3, T4, T5, T6>(JsonElement element, JsonSerializerOptions options)
    {
        (string typeName, JsonElement value) = GetChoiceTypeAndValue(element);

        Type t = TypeName.GetTypeFromName(typeName, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));

        if (t == typeof(T1)) return Choice<T1, T2, T3, T4, T5, T6>.From(value.Deserialize<T1>(options));
        if (t == typeof(T2)) return Choice<T1, T2, T3, T4, T5, T6>.From(value.Deserialize<T2>(options));
        if (t == typeof(T3)) return Choice<T1, T2, T3, T4, T5, T6>.From(value.Deserialize<T3>(options));
        if (t == typeof(T4)) return Choice<T1, T2, T3, T4, T5, T6>.From(value.Deserialize<T4>(options));
        if (t == typeof(T5)) return Choice<T1, T2, T3, T4, T5, T6>.From(value.Deserialize<T5>(options));
        if (t == typeof(T6)) return Choice<T1, T2, T3, T4, T5, T6>.From(value.Deserialize<T6>(options));

        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2> ConvertFromArray<T1, T2>(JsonElement element, JsonSerializerOptions options)
    {
        JsonElement[] items = element.EnumerateArray().ToArray();
        int index = items[0].GetInt32();

        switch (index)
        {
            case 1: return Choice<T1, T2>.From(items[1].Deserialize<T1>(options));
            case 2: return Choice<T1, T2>.From(items[1].Deserialize<T2>(options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2, T3> ConvertFromArray<T1, T2, T3>(JsonElement element, JsonSerializerOptions options)
    {
        JsonElement[] items = element.EnumerateArray().ToArray();
        int index = items[0].GetInt32();

        switch (index)
        {
            case 1: return Choice<T1, T2, T3>.From(items[1].Deserialize<T1>(options));
            case 2: return Choice<T1, T2, T3>.From(items[1].Deserialize<T2>(options));
            case 3: return Choice<T1, T2, T3>.From(items[1].Deserialize<T3>(options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2, T3, T4> ConvertFromArray<T1, T2, T3, T4>(JsonElement element, JsonSerializerOptions options)
    {
        JsonElement[] items = element.EnumerateArray().ToArray();
        int index = items[0].GetInt32();

        switch (index)
        {
            case 1: return Choice<T1, T2, T3, T4>.From(items[1].Deserialize<T1>(options));
            case 2: return Choice<T1, T2, T3, T4>.From(items[1].Deserialize<T2>(options));
            case 3: return Choice<T1, T2, T3, T4>.From(items[1].Deserialize<T3>(options));
            case 4: return Choice<T1, T2, T3, T4>.From(items[1].Deserialize<T4>(options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2, T3, T4, T5> ConvertFromArray<T1, T2, T3, T4, T5>(JsonElement element, JsonSerializerOptions options)
    {
        JsonElement[] items = element.EnumerateArray().ToArray();
        int index = items[0].GetInt32();

        switch (index)
        {
            case 1: return Choice<T1, T2, T3, T4, T5>.From(items[1].Deserialize<T1>(options));
            case 2: return Choice<T1, T2, T3, T4, T5>.From(items[1].Deserialize<T2>(options));
            case 3: return Choice<T1, T2, T3, T4, T5>.From(items[1].Deserialize<T3>(options));
            case 4: return Choice<T1, T2, T3, T4, T5>.From(items[1].Deserialize<T4>(options));
            case 5: return Choice<T1, T2, T3, T4, T5>.From(items[1].Deserialize<T5>(options));
        }

        throw new ArgumentOutOfRangeException();
    }

    public static Choice<T1, T2, T3, T4, T5, T6> ConvertFromArray<T1, T2, T3, T4, T5, T6>(JsonElement element, JsonSerializerOptions options)
    {
        JsonElement[] items = element.EnumerateArray().ToArray();
        int index = items[0].GetInt32();

        switch (index)
        {
            case 1: return Choice<T1, T2, T3, T4, T5, T6>.From(items[1].Deserialize<T1>(options));
            case 2: return Choice<T1, T2, T3, T4, T5, T6>.From(items[1].Deserialize<T2>(options));
            case 3: return Choice<T1, T2, T3, T4, T5, T6>.From(items[1].Deserialize<T3>(options));
            case 4: return Choice<T1, T2, T3, T4, T5, T6>.From(items[1].Deserialize<T4>(options));
            case 5: return Choice<T1, T2, T3, T4, T5, T6>.From(items[1].Deserialize<T5>(options));
            case 6: return Choice<T1, T2, T3, T4, T5, T6>.From(items[1].Deserialize<T6>(options));
        }

        throw new ArgumentOutOfRangeException();
    }

}

internal class ChoiceConverter<T1, T2> : JsonConverter<Choice<T1, T2>>
{
    public override Choice<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonElement element = ChoiceConverterHelper.ReadChoiceElement(ref reader, options);

        if (element.ValueKind == JsonValueKind.Array) return ChoiceConverterHelper.ConvertFromArray<T1, T2>(element, options);

        if (element.ValueKind == JsonValueKind.Object) return ChoiceConverterHelper.ConvertToChoiceFromObject<T1, T2>(element, options);

        throw new JsonException("Expected StartArray or StartObject");
    }
    
    public override void Write(Utf8JsonWriter writer, Choice<T1, T2> value, JsonSerializerOptions options)
    {
        void WriteValue<T>(T val)
        {
            
            writer.WritePropertyName(ChoiceConverterHelper.ValuePropertyName);
            ChoiceConverterHelper.WriteValue(writer, val, options);
        }

        writer.WriteStartObject();
        writer.WritePropertyName(ChoiceConverterHelper.TypeNamePropertyName);
        ChoiceConverterHelper.WriteType(writer, value, options);
        value.Match(WriteValue, WriteValue);
        writer.WriteEndObject();
    }
}

internal class ChoiceConverter<T1, T2, T3> : JsonConverter<Choice<T1, T2, T3>>
{
    public override Choice<T1, T2, T3> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonElement element = ChoiceConverterHelper.ReadChoiceElement(ref reader, options);

        if (element.ValueKind == JsonValueKind.Array) return ChoiceConverterHelper.ConvertFromArray<T1, T2, T3>(element, options);

        if (element.ValueKind == JsonValueKind.Object) return ChoiceConverterHelper.ConvertToChoiceFromObject<T1, T2, T3>(element, options);

        throw new JsonException("Expected StartArray or StartObject");
    }

    public override void Write(Utf8JsonWriter writer, Choice<T1, T2, T3> value, JsonSerializerOptions options)
    {
        void WriteValue<T>(T val)
        {
            ChoiceConverterHelper.WriteValue(writer, val, options);
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
        JsonElement element = ChoiceConverterHelper.ReadChoiceElement(ref reader, options);

        if (element.ValueKind == JsonValueKind.Array) return ChoiceConverterHelper.ConvertFromArray<T1, T2, T3, T4>(element, options);

        if (element.ValueKind == JsonValueKind.Object) return ChoiceConverterHelper.ConvertToChoiceFromObject<T1, T2, T3, T4>(element, options);

        throw new JsonException("Expected StartArray or StartObject");
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
        JsonElement element = ChoiceConverterHelper.ReadChoiceElement(ref reader, options);

        if (element.ValueKind == JsonValueKind.Array) return ChoiceConverterHelper.ConvertFromArray<T1, T2, T3, T4, T5>(element, options);

        if (element.ValueKind == JsonValueKind.Object) return ChoiceConverterHelper.ConvertToChoiceFromObject<T1, T2, T3, T4, T5>(element, options);

        throw new JsonException("Expected StartArray or StartObject");
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
        JsonElement element = ChoiceConverterHelper.ReadChoiceElement(ref reader, options);

        if (element.ValueKind == JsonValueKind.Array) return ChoiceConverterHelper.ConvertFromArray<T1, T2, T3, T4, T5, T6>(element, options);

        if (element.ValueKind == JsonValueKind.Object) return ChoiceConverterHelper.ConvertToChoiceFromObject<T1, T2, T3, T4, T5, T6>(element, options);

        throw new JsonException("Expected StartArray or StartObject");
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