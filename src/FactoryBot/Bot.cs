using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FactoryBot.Configurations;
using FactoryBot.DSL;
using FactoryBot.ExpressionParser;

namespace FactoryBot
{
    public class Bot
    {
        private static readonly Dictionary<Type, BotConfiguration> BuildRules = new Dictionary<Type, BotConfiguration>(); 

        public static void Define<T>(Expression<Func<BotBuilder, T>> factory)
        {
            var parser = new FactoryParser();
            var configuration = parser.Parse(factory);
            BuildRules[configuration.ConstructingType] = configuration;
        }

        public static T Build<T>()
        {
            return (T)BuildRules[typeof(T)].CreateNewObject();
        }

        public static IEnumerable<T> BuildSequence<T>()
        {
            throw new NotImplementedException();
        } 
    }
}
