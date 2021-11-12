using FunctionalRecords;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal class ResultConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (typeToConvert == typeof(Result))
        {
            return true;
        }

        return typeToConvert != null
            && typeToConvert.IsGenericType
            && typeToConvert.GetGenericTypeDefinition() == typeof(Result<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert == typeof(Result))
        {
            return new ResultConverter();
        }

        Type typeArgument = typeToConvert.GetGenericArguments()[0];
        Type converterType = typeof(ResultConverter<>).MakeGenericType(typeArgument);
        return Activator.CreateInstance(converterType) as JsonConverter;
    }
}
