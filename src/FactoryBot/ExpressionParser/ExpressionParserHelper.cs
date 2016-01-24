using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using FactoryBot.Configurations;
using FactoryBot.DSL;
using FactoryBot.Generators;

namespace FactoryBot.ExpressionParser
{
    internal static class ExpressionParserHelper
    {
        public static ConstructorGenerator CreateConstructorGenerator(NewExpression newExpr)
        {
            Check.NotNull(newExpr, nameof(newExpr));

            return new ConstructorGenerator(
                newExpr.Constructor,
                newExpr.Arguments.Select(ParseGeneratorVariable).ToArray());
        }

        public static IGenerator ParseGeneratorVariable(Expression expr)
        {
            Check.NotNull(expr, nameof(expr));

            var methodCallExpr = expr as MethodCallExpression;
            var generatorAttr = methodCallExpr?.Method.GetCustomAttribute<GeneratorAttribute>();
            if (generatorAttr != null)
            {
                var generatorParamenters = methodCallExpr.Arguments.Select(EvaluateExpression).ToArray();
                return !methodCallExpr.Method.IsGenericMethod
                           ? generatorAttr.CreateGenerator(generatorParamenters)
                           : generatorAttr.CreateGenericGenerator(methodCallExpr.Method.GetGenericArguments(), generatorParamenters);
            }

            return new ConstantGenerator(EvaluateExpression(expr));
        }

        public static object EvaluateExpression(Expression expr)
        {
            Check.NotNull(expr, nameof(expr));

            return Expression.Lambda(expr).Compile().DynamicInvoke();
        }
    }
}