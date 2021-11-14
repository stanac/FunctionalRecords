using System.Text.Json;
using System.Text.Json.Serialization;

namespace FunctionalRecords.Examples;

public static class SerializationExample
{
    public class JsonTestObj
    {
        public ValueRecordExample.PersonName PersonName { get; set; }
        public Maybe<int> MaybeInt1 { get; set; }
        public Maybe<int> MaybeInt2 { get; set; }
        public Result<Guid> Result { get; set; }
        public Choice<string, int> Choice2 { get; set; }
    }

    public static void Example()
    {
        JsonTestObj t = new JsonTestObj
        {
            PersonName = ("Jane", "Doe"),
            Choice2 = "Somethig",
            Result = Result.Success(Guid.NewGuid()),
            MaybeInt1 = Maybe<int>.None,
            MaybeInt2 = -5548
        };

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
        serializerOptions.WriteIndented = true;
        foreach (JsonConverter c in FunctionalRecords.Serialization.Json.Converters.AllConverters)
        {
            serializerOptions.Converters.Add(c);
        }

        string json = JsonSerializer.Serialize(t, serializerOptions);
        Console.WriteLine(json);
    }
}
