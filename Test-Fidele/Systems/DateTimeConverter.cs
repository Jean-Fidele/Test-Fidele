using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Test_Fidele.Systems;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime));
        return DateTime.Parse(reader.GetString()).ToUniversalTime();
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssZ"));
    }
}