using FactoryBot.Configurations;
using System;

namespace FactoryBot.DSL.Builders
{
    public class BotDefinitionBuilder<T>
    {
        private readonly BotConfiguration _config;

        internal BotDefinitionBuilder(BotConfiguration config) => _config = config;

        public BotDefinitionBuilder<T> BeforePropertyBinding(Action<T> action)
        {
            _config.BeforeBindingHook = x => action((T)x);
            return this;
        }

        public BotDefinitionBuilder<T> AfterPropertyBinding(Action<T> action)
        {
            _config.AfterBindingHook = x => action((T)x);
            return this;
        }
    }
}