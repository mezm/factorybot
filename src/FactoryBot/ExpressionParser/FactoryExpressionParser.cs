using System;
using System.Linq.Expressions;
using FactoryBot.Configurations;
using FactoryBot.DSL.Builders;

namespace FactoryBot.ExpressionParser
{
    internal class FactoryExpressionParser
    {
        public BotConfiguration Parse<T>(Expression<Func<BotConfigurationBuilder, T>> factory)
        {
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