using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal class MaybeConverter<T> : JsonConverter<Maybe<T>>
{
    public override Maybe<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return Maybe<T>.None;
        }

        JsonConverter<T> valueConverter = options.GetConverter(typeof(T)) as JsonConverter<T>;
        T value = valueConverter.Read(ref reader, typeof(T), options);

        return Maybe<T>.From(value);
    }

    public override void Write(Utf8JsonWriter writer, Maybe<T> value, JsonSerializerOptions options)
    {
        value.Match(
            some: v =>
            {
                JsonConverter<T> valueConverter = options.GetConverter(typeof(T)) as JsonConverter<T>;
                valueConverter.Write(writer, v, options);
            },
            none: () => writer.WriteNullValue()
            );
    }
}