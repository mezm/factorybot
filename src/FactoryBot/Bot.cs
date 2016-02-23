using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using FactoryBot.Configurations;
using FactoryBot.DSL;
using FactoryBot.ExpressionParser;

namespace FactoryBot
{
    public class Bot
    {
        public const int SequenceMaxLength = 100;

        private static readonly Dictionary<Type, BotConfiguration> BuildRules = new Dictionary<Type, BotConfiguration>(); 

        public static void Define<T>(Expression<Func<BotConfigurationBuilder, T>> factory)
        {
            Check.NotNull(factory, nameof(factory));

            var parser = new FactoryParser();
            var configuration = parser.Parse(factory);
            CheckNestedAndCircularDependencies(configuration);

            BuildRules[configuration.ConstructingType] = configuration;
        }

        public static T Build<T>(params Action<T>[] modifiers)
        {
            Check.NotNull(modifiers, nameof(modifiers));

            var result = (T)GetConfiguration(typeof(T)).CreateNewObject();
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
            Check.NotNull(constructorModifier, nameof(constructorModifier));
            Check.NotNull(afterConstructModifiers, nameof(afterConstructModifiers));

            var parser = new ConstructorParser();
            var constructor = parser.Parse(constructorModifier);

            var result = (T)GetConfiguration(typeof(T)).CreateNewObjectWithModification(constructor);
            foreach (var modifier in afterConstructModifiers)
            {
                modifier(result);
            }

            return result;
        }

        public static IEnumerable<T> BuildSequence<T>(bool infinite = false)
        {
            if (infinite)
            {
                while (true)
                {
                    yield return Build<T>();
                }
            }

            for (var i = 0; i < SequenceMaxLength; i++)
            {
                yield return Build<T>();
            }

            throw new InvalidOperationException(
                "BuildSequence generates infinite sequence and should not be used in a foreach look. If it's not the case set infinite parameter to true.");
        }

        internal static void ForgetAll()
        {
            BuildRules.Clear();            
        }

        private static BotConfiguration GetConfiguration(Type ruleKey)
        {
            BotConfiguration config;
            if (!BuildRules.TryGetValue(ruleKey, out config))
            {
                throw new UnknownTypeException(ruleKey);
            }

            return config;
        }

        private static void CheckNestedAndCircularDependencies(BotConfiguration configuration)
        {
            var dependencies = configuration.GetNestedDependencies().ToList();
            for (var i = 0; i < dependencies.Count; i++)
            {
                var dependencyType = dependencies[i];
                var nestedDependency = (dependencyType == configuration.ConstructingType ? configuration : GetConfiguration(dependencyType)).GetNestedDependencies();
                if (nestedDependency.Any(x => dependencies.Contains(x)))
                {
                    throw new CircularDependencyDetectedException();
                }

                dependencies.AddRange(nestedDependency);
            }
        }
    }
}
