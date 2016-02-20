using System;
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
        public static ConstructorDefinition CreateConstructorGenerator(NewExpression newExpr)
        {
            Check.NotNull(newExpr, nameof(newExpr));

            return new ConstructorDefinition(
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
                    var parameter = methodParameters[i];
                    var argumentExpr = methodCallExpr.Arguments[i];

                    generatorParamenters[parameter.Name] = parameter.IsDefined(typeof(ItemGeneratorAttribute))
                                                               ? ParseGeneratorVariable(argumentExpr)
                                                               : EvaluateExpression(argumentExpr);
                }

                return generatorAttr.CreateGenerator(methodCallExpr.Method, generatorParamenters);
            }

            var memberInitExpr = expr as MemberInitExpression;
            if (memberInitExpr != null)
            {
                return ParseMemberInit(memberInitExpr);
            }

            var constructorExpr = expr as NewExpression;
            if (constructorExpr != null)
            {
                return ParseConstructor(constructorExpr);
            }

            return new ConstantGenerator(EvaluateExpression(expr));
        }

        public static object EvaluateExpression(Expression expr)
        {
            Check.NotNull(expr, nameof(expr));

            return Expression.Lambda(expr).Compile().DynamicInvoke();
        }

        public static BotConfiguration ParseConstructor(NewExpression newExpr)
        {
            var constructorGenerator = CreateConstructorGenerator(newExpr);
            return new BotConfiguration(newExpr.Type, constructorGenerator);
        }

        public static BotConfiguration ParseMemberInit(MemberInitExpression memberInitExpr)
        {
            var config = ParseConstructor(memberInitExpr.NewExpression);
            foreach (var binding in memberInitExpr.Bindings)
            {
                config.Properties.Add(ParseMemberBinding(binding));
            }

            return config;
        }

        public static PropertyDefinition ParseMemberBinding(MemberBinding binding)
        {
            if (binding.BindingType == MemberBindingType.Assignment)
            {
                var assignmentBinding = (MemberAssignment)binding;
                return new PropertyDefinition(
                    (PropertyInfo)binding.Member,
                    ParseGeneratorVariable(assignmentBinding.Expression));
            }

            throw new NotSupportedException();
        }
    }
}