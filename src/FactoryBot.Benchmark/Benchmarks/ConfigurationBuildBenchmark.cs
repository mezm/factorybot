using BenchmarkDotNet.Attributes;
using FactoryBot.Benchmark.Models;

namespace FactoryBot.Benchmark.Benchmarks
{
    public class ConfigurationBuildBenchmark
    {
        [Benchmark]
        public void Define()
        {
            Bot.Define(
                x =>
                new AllTypesModel
                {
                    Boolean = x.Boolean.Any(),
                    Byte = x.Byte.Any(),
                    DateTime = x.Dates.Any(),
                    Decimal = x.Decimal.Any(),
                    Double = x.Double.Any(),
                    Float = x.Float.Any(),
                    Integer = x.Integer.Any(),
                    Long = x.Long.Any(),
                    Short = x.Short.Any(),
                    String = x.Strings.Any(),
                    TimeSpan = x.Time.Any()
                });
        }

        [Benchmark]
        public void DefineAuto() => Bot.DefineAuto<AllTypesModel>();

        [GlobalCleanup]
        public void CleanUp() => Bot.ForgetAll();
    }
}
