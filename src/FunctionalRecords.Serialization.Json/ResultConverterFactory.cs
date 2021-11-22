using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal class ResultConverterFactory : JsonConverterFactory
{
    private readonly Type[] _supportedGenTypes = new[]
    {
        typeof(Result<>),
        typeof(Result<,>)
    };

    public override bool CanConvert(Type typeToConvert)
    {
        if (typeToConvert == typeof(Result))
        {
            return true;
        }

        return typeToConvert != null
               && typeToConvert.IsGenericType
               && _supportedGenTypes.Contains(typeToConvert.GetGenericTypeDefinition());
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        if (typeToConvert == typeof(Result))
        {
            return new ResultConverter();
        }

        Type[] genArgs = typeToConvert.GetGenericArguments();

        if (genArgs.Length == 1)
        {
            Type typeArgument = genArgs[0];
            Type converterType = typeof(ResultConverter<>).MakeGenericType(typeArgument);
            return Activator.CreateInstance(converterType) as JsonConverter;
        }

        Type converterTypeWithFailure = typeof(ResultConverter<,>).MakeGenericType(genArgs);
        return Activator.CreateInstance(converterTypeWithFailure) as JsonConverter;
    }
}
