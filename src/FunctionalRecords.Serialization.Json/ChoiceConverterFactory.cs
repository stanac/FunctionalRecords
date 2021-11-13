using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal class ChoiceConverterFactory : JsonConverterFactory
{
    private static readonly Type[] _supportedTypes = new[]
    {
        typeof(Choice<,>)
    };

    private static readonly Type[] _converterTypes = new[]
    {
        typeof(ChoiceConverter<,>)
    };

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert != null
            && typeToConvert.IsGenericType
            && _supportedTypes.Contains(typeToConvert.GetGenericTypeDefinition());
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type[] genArgs = typeToConvert.GetGenericArguments();
        int typeIndex = genArgs.Length - 2;
        return Activator.CreateInstance(_converterTypes[typeIndex].MakeGenericType(genArgs)) as JsonConverter;
    }
}
