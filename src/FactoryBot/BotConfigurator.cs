using FactoryBot.Configurations;
using FactoryBot.DSL.Builders;
using FactoryBot.ExpressionParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FactoryBot
{
    /// <summary>
    /// Bot configurator, container where all types build rules are stored
    /// </summary>
    public class BotConfigurator 
    {
        private static readonly Dictionary<Type, BotConfiguration> BuildRules = new Dictionary<Type, BotConfiguration>();

        /// <summary>
        /// Creates manually configuration for a given type of model
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="factory">Configuration</param>
        /// <returns>BotDefinitionBuilder</returns>
        public static BotDefinitionBuilder<T> Configure<T>(Expression<Func<BotConfigurationBuilder, T>> factory)
        {
            var parser = new FactoryExpressionParser();
            var configuration = parser.Parse(factory);
            CheckNestedAndCircularDependencies(configuration);

            BuildRules[configuration.ConstructingType] = configuration;

            return new BotDefinitionBuilder<T>(configuration);
        }

        /// <summary>
        /// Creates auto generated configuration for a given type of model
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="overrideDefault">Manual configuration overrides</param>
        /// <returns></returns>
        public static BotDefinitionBuilder<T> ConfigureAuto<T>(Expression<Func<BotConfigurationBuilder, T>>? overrideDefault = null)
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
        /// Purge all configurations
        /// </summary>
        public static void ForgetAll() => BuildRules.Clear();

        internal static BotConfiguration GetConfiguration(Type ruleKey)
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
