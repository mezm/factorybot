using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FactoryBot.Configurations;
using FactoryBot.DSL.Builders;
using FactoryBot.ExpressionParser;

namespace FactoryBot
{
    /// <summary>
    /// Bot that can help you with auto generating you models
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// Maximum number of item to generate by <see cref="BuildSequence{T}(bool)" /> before it throws an exception
        /// </summary>
        public const int SEQUENCE_MAX_LENGTH = 100;

        private static readonly Dictionary<Type, BotConfiguration> BuildRules = new Dictionary<Type, BotConfiguration>();

        /// <summary>
        /// Manually defines configuration for a given type of model
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="factory">Configuration</param>
        /// <returns>BotDefinitionBuilder</returns>
        public static BotDefinitionBuilder<T> Define<T>(Expression<Func<BotConfigurationBuilder, T>> factory)
        {
            var parser = new FactoryExpressionParser();
            var configuration = parser.Parse(factory);
            CheckNestedAndCircularDependencies(configuration);

            BuildRules[configuration.ConstructingType] = configuration;

            return new BotDefinitionBuilder<T>(configuration);
        }

        /// <summary>
        /// Auto defines configuration for a given type of model
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="overrideDefault">Manual configuration overrides</param>
        /// <returns></returns>
        public static BotDefinitionBuilder<T> DefineAuto<T>(Expression<Func<BotConfigurationBuilder, T>>? overrideDefault = null)
        {
            var parser = new AutoBindingParser();
            var configuration = parser.Parse<T>();
            
            if (overrideDefault != null)
            {
                var factoryParser = new FactoryExpressionParser();
                var overrideConfig = factoryParser.Parse(overrideDefault);
                overrideConfig.MergeProperties(configuration);
                configuration = overrideConfig;
            }

            BuildRules[configuration.ConstructingType] = configuration;

            return new BotDefinitionBuilder<T>(configuration);
        }

        /// <summary>
        /// Sets or overrides default generator for auto defining
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="generator">New generator</param>
        public static void SetDefaultAutoGenerator<T>(Expression<Func<BotConfigurationBuilder, T>> generator)
        {
            AutoBindingParser.DefaultGenerators[typeof(T)] = ExpressionParserHelper.ParseGeneratorVariable(generator.Body);
        }

        /// <summary>
        /// Builds a model based on configuration defined earlier for a type
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="modifiers">Optional build modifiers</param>
        /// <returns>Generated model</returns>
        public static T Build<T>(params Action<T>[] modifiers)
        {
            var result = (T)GetConfiguration(typeof(T)).CreateNewObject();
            foreach (var modifier in modifiers)
            {
                modifier(result);
            }

            return result;
        }

        /// <summary>
        /// Builds a model based on configuration defined earlier for a type with ability to alter configuration
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="constructorModifier">Configuration build modifier</param>
        /// <param name="afterConstructModifiers">Optional build modifiers</param>
        /// <returns>Generated model</returns>
        public static T BuildCustom<T>(
            Expression<Func<CustomConstructBuilder, T>> constructorModifier,
            params Action<T>[] afterConstructModifiers)
        {
            var parser = new ConstructorParser();
            var constructor = parser.Parse(constructorModifier);

            var result = (T)GetConfiguration(typeof(T)).CreateNewObjectWithModification(constructor);
            foreach (var modifier in afterConstructModifiers)
            {
                modifier(result);
            }

            return result;
        }

        /// <summary>
        /// Generates sequence of models
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="infinite">Whether it is an infinite sequence or not</param>
        /// <returns>Enumerable of generated models</returns>
        public static IEnumerable<T> BuildSequence<T>(bool infinite = false)
        {
            if (infinite)
            {
                while (true)
                {
                    yield return Build<T>();
                }
            }

            for (var i = 0; i < SEQUENCE_MAX_LENGTH; i++)
            {
                yield return Build<T>();
            }

            throw new InvalidOperationException(
                "BuildSequence generates infinite sequence and should not be used in a foreach loop. If it's not the case set infinite parameter to true.");
        }

        /// <summary>
        /// Purge all configurations
        /// </summary>
        public static void ForgetAll() => BuildRules.Clear();

        private static BotConfiguration GetConfiguration(Type ruleKey)
        {
            return BuildRules.TryGetValue(ruleKey, out var config) ? config : throw new UnknownTypeException(ruleKey);
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
