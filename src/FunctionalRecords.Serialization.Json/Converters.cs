﻿using System.Text.Json.Serialization;

namespace FunctionalRecords.Serialization.Json;

public static class Converters
{
    public static IReadOnlyList<JsonConverter> AllConverters =>
        new List<JsonConverter>
        {
            new MaybeConverterFactory(),
            new ResultConverterFactory(),
            new ValueTupleConverterFactory(),
            new ChoiceConverterFactory()
        };
}
