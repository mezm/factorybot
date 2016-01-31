using System.Collections.Generic;
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
                var methodParameters = methodCallExpr.Method.GetParameters();
                var generatorParamenters = new Dictionary<string, object>(methodParameters.Length);
                for (var i = 0; i < methodParameters.Length; i++)
                {
                    generatorParamenters[methodParameters[i].Name] = EvaluateExpression(methodCallExpr.Arguments[i]);
                }

                return generatorAttr.CreateGenerator(methodCallExpr.Method, generatorParamenters);
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