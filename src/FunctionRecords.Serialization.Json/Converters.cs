using System.Text.Json.Serialization;

namespace FunctionRecords.Serialization.Json;

public static class Converters
{
    public static IReadOnlyList<JsonConverter> AllConverters
    {
        get
        {
            return new List<JsonConverter>
            {
                new MaybeConverterFactory()
            };
        }
    }
}
