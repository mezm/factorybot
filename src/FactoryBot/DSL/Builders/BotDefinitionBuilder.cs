using FactoryBot.Configurations;
using System;

namespace FactoryBot.DSL.Builders
{
    /// <summary>
    /// Bot definition builder
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public class BotDefinitionBuilder<T>
    {
        private readonly BotConfiguration _config;

        internal BotDefinitionBuilder(BotConfiguration config) => _config = config;

        /// <summary>
        /// Hook that is called before properties are going to set by generators. All properties set in the hook won't be rewritten
        /// </summary>
        /// <param name="action">Hook action</param>
        /// <returns>BotDefinitionBuilder</returns>
        public BotDefinitionBuilder<T> BeforePropertyBinding(Action<T> action)
        {
            _config.BeforeBindingHook = x => action((T)x);
            return this;
        }

        /// <summary>
        /// Hook that is called after all properties are set by generators. 
        /// </summary>
        /// <param name="action">Hook action</param>
        /// <returns>BotDefinitionBuilder</returns>
        public BotDefinitionBuilder<T> AfterPropertyBinding(Action<T> action)
        {
            _config.AfterBindingHook = x => action((T)x);
            return this;
        }
    }
}