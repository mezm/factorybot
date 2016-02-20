using System;
using System.Linq.Expressions;

using FactoryBot.Configurations;
using FactoryBot.DSL;

namespace FactoryBot.ExpressionParser
{
    internal class ConstructorParser
    {
        public ConstructorDefinition Parse<T>(Expression<Func<CustomConstructBuilder, T>> constructor)
        {
            Check.NotNull(constructor, nameof(constructor));

            var newExpr = constructor.Body as NewExpression;
            if (newExpr != null)
            {
                return ExpressionParserHelper.CreateConstructorGenerator(newExpr);
            }

            throw new NotSupportedException();
        }
    }
}