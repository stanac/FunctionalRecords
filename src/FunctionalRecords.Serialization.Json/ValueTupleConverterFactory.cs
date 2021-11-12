using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

internal class ValueTupleConverterFactory : JsonConverterFactory
{
    private static readonly Type[] _supportedTypesGenericDefinitions = new[]
    {
        typeof(ValueTuple<>),
        typeof(ValueTuple<,>),
        typeof(ValueTuple<,,>),
        typeof(ValueTuple<,,,>),
        typeof(ValueTuple<,,,,>),
        typeof(ValueTuple<,,,,,>),
        typeof(ValueTuple<,,,,,,>)
    };

    private static readonly Type[] _convertersGenericTypeDefinitions = new[]
    {
        typeof(ValueTupleConverter<>),
        typeof(ValueTupleConverter<,>),
        typeof(ValueTupleConverter<,,>),
        typeof(ValueTupleConverter<,,,>),
        typeof(ValueTupleConverter<,,,,>),
        typeof(ValueTupleConverter<,,,,,>),
        typeof(ValueTupleConverter<,,,,,,>)
    };

    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert != null
            && typeToConvert.IsGenericType
            && _supportedTypesGenericDefinitions.Contains(typeToConvert.GetGenericTypeDefinition());
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type[] genericArguments = typeToConvert.GetGenericArguments();
        Type genericDefinitionConverterType = _convertersGenericTypeDefinitions[genericArguments.Length - 1];
        Type converterType = genericDefinitionConverterType.MakeGenericType(genericArguments);
        return Activator.CreateInstance(converterType) as JsonConverter;
    }
}
