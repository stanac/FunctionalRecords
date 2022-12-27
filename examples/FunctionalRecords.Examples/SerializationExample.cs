using System.Text.Json;
using System.Text.Json.Serialization;
using FunctionalRecords.Serialization.Json;

namespace FunctionalRecords.Examples;

public static class SerializationExample
{
    public class JsonTestObj
    {
        public ValueRecordExample.PersonName PersonName { get; set; }
        public Maybe<int> MaybeInt1 { get; set; }
        public Maybe<int> MaybeInt2 { get; set; }
        public Result<Guid> Result { get; set; }
        public Choice<string, int> StringOrInt { get; set; }
    }

    public static void Example()
    {
        JsonTestObj t = new JsonTestObj
        {
            PersonName = ("Jane", "Doe"),
            StringOrInt = "Somethig",
            Result = Result.Success(Guid.NewGuid()),
            MaybeInt1 = Maybe<int>.None,
            MaybeInt2 = -5548
        };

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
        serializerOptions.WriteIndented = true;
        serializerOptions.AddFunctionalRecordsConverters();

        string json = JsonSerializer.Serialize(t, serializerOptions);
        Console.WriteLine(json);
    }
}
