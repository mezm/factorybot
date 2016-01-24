using System;
using System.Linq.Expressions;

using FactoryBot.Configurations;
using FactoryBot.DSL;

namespace FactoryBot.ExpressionParser
{
    internal class ConstructorParser
    {
        public ConstructorGenerator Parse<T>(Expression<Func<CustomConstructBuilder, T>> constructor)
        {
            var newExpr = constructor.Body as NewExpression;
            if (newExpr != null)
            {
                return ExpressionParserHelper.CreateConstructorGenerator(newExpr);
            }

            throw new NotSupportedException();
        }
    }
}