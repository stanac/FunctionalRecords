using FunctionalRecords;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionRecords.Serialization.Json;

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
        Type subType = typeToConvert.GetGenericArguments()[0];

        Type converterType = typeof(MaybeConverter<>).MakeGenericType(subType);
        return Activator.CreateInstance(converterType) as JsonConverter;
    }
}