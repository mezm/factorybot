using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FactoryBot.Samples
{
    public class AllTypesModel
    {
        public int Integer { get; set; }

        public long Long { get; set; }

        public short Short { get; set; }

        public byte Byte { get; set; }

        public double Double { get; set; }

        public float Float { get; set; }

        public decimal Decimal { get; set; }

        public bool Boolean { get; set; }

        public string String { get; set; }

        public DateTime DateTime { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan TimeSpan { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
