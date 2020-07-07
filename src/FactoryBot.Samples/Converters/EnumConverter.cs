using FactoryBot.Samples.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FactoryBot.Samples.Converters
{
    public class EnumConverter : JsonConverter<EnumModel>
    {
        public override EnumModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotSupportedException();

        public override void Write(Utf8JsonWriter writer, EnumModel value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
    }
}
