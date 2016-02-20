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

            var memberInitExpr = factory.Body as MemberInitExpression;
            if (memberInitExpr != null)
            {
                return ExpressionParserHelper.ParseMemberInit(memberInitExpr);
            }

            var newExpr = factory.Body as NewExpression;
            if (newExpr != null)
            {
                return ExpressionParserHelper.ParseConstructor(newExpr);
            }

            throw new NotSupportedException();
        }

        
    }
}