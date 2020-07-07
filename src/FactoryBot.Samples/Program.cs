using FactoryBot.Samples.Models;
using System;
using System.Linq;

namespace FactoryBot.Samples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var count = 5;
            if (args.Length != 0 && int.TryParse(args[0], out var argCount))
            {
                count = argCount;
            }

            AutoDefine(count);

            Console.WriteLine();
            Bot.ForgetAll();
            ManualDefine(count);
        }

        private static void AutoDefine(int count)
        {
            Bot.DefineAuto<AllTypesModel>();
            var model = Bot.Build<AllTypesModel>();
            Console.WriteLine("Auto Define:");
            Bot.BuildSequence<AllTypesModel>().Take(count).ToList().ForEach(x => Console.WriteLine(x));
        }

        private static void ManualDefine(int count)
        {
            Bot.Define(x => new AllTypesModel
            {
                Boolean = x.Boolean.Any(),
                Byte = x.Byte.RandomFromList(3, 4, 5, 10, 12, 100),
                DateTime = x.Dates.AfterNow(),
                Decimal = x.Decimal.Any(-1000000, 1000000),
                Double = x.Double.SequenceFromList(12.45, 66.1234, 444.4),
                Enum = x.Enums.RandomFromList(EnumModel.First, EnumModel.Last),
                Float = x.Float.Any(),
                Integer = x.Integer.Any(0, 500),
                Long = x.Long.Any(),
                Short = x.Short.Any(-100, 100),
                String = x.Address.Country(),
                TimeSpan = x.Time.Any()
            });

            var model = Bot.Build<AllTypesModel>();
            Console.WriteLine("Manual Define:");
            Bot.BuildSequence<AllTypesModel>().Take(count).ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
