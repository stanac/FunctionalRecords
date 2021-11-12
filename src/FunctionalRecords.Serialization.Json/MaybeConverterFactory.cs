using FunctionalRecords;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal class MaybeConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert != null
            && typeToConvert.IsGenericType
            && typeToConvert.GetGenericTypeDefinition() == typeof(Maybe<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type genericArgumentType = typeToConvert.GetGenericArguments()[0];

        Type converterType = typeof(MaybeConverter<>).MakeGenericType(genericArgumentType);
        return Activator.CreateInstance(converterType) as JsonConverter;
    }
}