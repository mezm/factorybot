using FactoryBot.DSL.Builders;
using System;
using System.Linq.Expressions;

namespace FactoryBot
{
    public partial class Bot
    {
        /// <summary>
        /// Manually defines configuration for a given type of model
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="factory">Configuration</param>
        /// <returns>BotDefinitionBuilder</returns>
        [Obsolete("Use BotConfigurator.Configure(factory). Will be removed in a future release")]
        public static BotDefinitionBuilder<T> Define<T>(Expression<Func<BotConfigurationBuilder, T>> factory) => BotConfigurator.Configure(factory);

        /// <summary>
        /// Auto defines configuration for a given type of model
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="overrideDefault">Manual configuration overrides</param>
        /// <returns></returns>
        [Obsolete("Use BotConfigurator.ConfigureAuto<T>(overrideDefault). Will be removed in a future release")]
        public static BotDefinitionBuilder<T> DefineAuto<T>(Expression<Func<BotConfigurationBuilder, T>>? overrideDefault = null) => BotConfigurator.ConfigureAuto<T>(overrideDefault);

        /// <summary>
        /// Sets or overrides default generator for auto defining
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="generator">New generator</param>
        [Obsolete("Use BotConfigurator.SetDefaultAutoGenerator<T>(generator). Will be removed in a future release")]
        public static void SetDefaultAutoGenerator<T>(Expression<Func<BotConfigurationBuilder, T>> generator) => BotConfigurator.SetDefaultAutoGenerator(generator);

        /// <summary>
        /// Purge all configurations
        /// </summary>
        [Obsolete("Use BotConfigurator.ForgetAll(). Will be removed in a future release")]
        public static void ForgetAll() => BotConfigurator.ForgetAll();
    }
}
