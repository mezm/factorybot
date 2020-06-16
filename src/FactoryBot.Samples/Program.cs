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
        }

        private static void AutoDefine(int count)
        {
            Bot.DefineAuto<AllTypesModel>();
            var model = Bot.Build<AllTypesModel>();
            Console.WriteLine("AutoDefine:");
            Bot.BuildSequence<AllTypesModel>().Take(count).ToList().ForEach(x => Console.WriteLine(x));
        }
    }
}
