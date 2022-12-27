using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

public static class JsonSerializerOptionsExtensions
{
    public static JsonSerializerOptions AddFunctionalRecordsConverters(this JsonSerializerOptions options)
    {
        if (options == null) throw new ArgumentNullException(nameof(options));

        foreach (JsonConverter c in Converters.AllConverters)
        {
            options.Converters.Add(c);
        }

        return options;
    }
}