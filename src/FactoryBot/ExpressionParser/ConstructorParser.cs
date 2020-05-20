using System;
using System.Linq.Expressions;
using FactoryBot.Configurations;
using FactoryBot.DSL.Builders;

namespace FactoryBot.ExpressionParser
{
    internal class ConstructorParser
    {
        public ConstructorDefinition Parse<T>(Expression<Func<CustomConstructBuilder, T>> constructor)
        {
            if (constructor.Body is NewExpression newExpr)
            {
                return ExpressionParserHelper.CreateConstructorGenerator(newExpr);
            }

            throw new NotSupportedException();
        }
    }
}