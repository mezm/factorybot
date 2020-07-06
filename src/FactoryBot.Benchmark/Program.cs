using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;

namespace FactoryBot.Benchmark
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = ManualConfig.Create(DefaultConfig.Instance)
                .WithOption(ConfigOptions.DisableLogFile, true)
                .WithOption(ConfigOptions.JoinSummary, true);

            BenchmarkRunner.Run(typeof(Program).Assembly, config);

            Console.WriteLine("DONE");
            Console.ReadLine();
        }
    }
}
