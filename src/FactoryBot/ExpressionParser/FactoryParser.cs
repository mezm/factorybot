using System;
using System.Linq.Expressions;

using FactoryBot.Configurations;
using FactoryBot.DSL;

namespace FactoryBot.ExpressionParser
{
    internal class FactoryParser
    {
        public BotConfiguration Parse<T>(Expression<Func<BotConfigurationBuilder, T>> factory)
        {
            Check.NotNull(factory, nameof(factory));

            if (factory.Body is MemberInitExpression memberInitExpr)
            {
                return ExpressionParserHelper.ParseMemberInit(memberInitExpr);
            }

            if (factory.Body is NewExpression newExpr)
            {
                return ExpressionParserHelper.ParseConstructor(newExpr);
            }

            throw new NotSupportedException();
        }

        
    }
}