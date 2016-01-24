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

        public static void Define<T>(Expression<Func<BotConfigurationBuilder, T>> factory)
        {
            var parser = new FactoryParser();
            var configuration = parser.Parse(factory);
            BuildRules[configuration.ConstructingType] = configuration;
        }

        public static T Build<T>(params Action<T>[] modifiers)
        {
            var result = (T)GetConfiguration<T>().CreateNewObject();
            foreach (var modifier in modifiers)
            {
                modifier(result);
            }

            return result;
        }

        public static T BuildCustom<T>(
            Expression<Func<CustomConstructBuilder, T>> constructorModifier,
            params Action<T>[] afterConstructModifiers)
        {
            var parser = new ConstructorParser();
            var constructor = parser.Parse(constructorModifier);

            var result = (T)GetConfiguration<T>().CreateNewObjectWithModification(constructor);
            foreach (var modifier in afterConstructModifiers)
            {
                modifier(result);
            }

            return result;
        }

        public static IEnumerable<T> BuildSequence<T>()
        {
            throw new NotImplementedException();
        }

        internal static void ForgetAll()
        {
            BuildRules.Clear();            
        }

        private static BotConfiguration GetConfiguration<T>()
        {
            var ruleKey = typeof(T);
            BotConfiguration config;
            if (!BuildRules.TryGetValue(ruleKey, out config))
            {
                throw new UnknownTypeException(ruleKey);    
            }

            return config;
        }
    }
}
