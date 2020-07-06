using BenchmarkDotNet.Attributes;
using FactoryBot.Benchmark.Models;

namespace FactoryBot.Benchmark.Benchmarks
{
    public class BuildModelBenchmark
    {
        [GlobalSetup]
        public void SetUp() => Bot.DefineAuto(x => new AllTypesModel { String = x.Strings.Words() });

        [Benchmark]
        public void Build() => Bot.Build<AllTypesModel>();
    }
}
