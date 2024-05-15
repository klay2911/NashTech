using System.Text.Json;
using System.Text.Json.Serialization;

namespace EFCore.Models.Common;

public class NoReferenceHandlingConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<T>(ref reader, new JsonSerializerOptions { ReferenceHandler = null });
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, new JsonSerializerOptions { ReferenceHandler = null });
    }
}