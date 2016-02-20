using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using FactoryBot.Configurations;
using FactoryBot.DSL;
using FactoryBot.Generators;
using FactoryBot.Generators.Collections;

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
            var property = (PropertyInfo)binding.Member;
            switch (binding.BindingType)
            {
                case MemberBindingType.Assignment:
                    return ParseMemberAssignment(property, (MemberAssignment)binding);
                case MemberBindingType.MemberBinding:
                    return ParseMemberMemberBinding(property, (MemberMemberBinding)binding);
                case MemberBindingType.ListBinding:
                    return ParseListBinding(property, (MemberListBinding)binding);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static PropertyDefinition ParseMemberAssignment(PropertyInfo property, MemberAssignment assignmentBinding)
        {
            return new PropertyDefinition(property, ParseGeneratorVariable(assignmentBinding.Expression));
        }

        private static PropertyDefinition ParseMemberMemberBinding(
            PropertyInfo property, 
            MemberMemberBinding memberMemberBinding)
        {
            var defaultConstructor = property.PropertyType.GetConstructor(new Type[0]);
            var constructor = new ConstructorDefinition(defaultConstructor, new IGenerator[0]);
            var botConfiguration = new BotConfiguration(property.PropertyType, constructor);
            foreach (var nestedBinding in memberMemberBinding.Bindings)
            {
                botConfiguration.Properties.Add(ParseMemberBinding(nestedBinding));
            }
            return new PropertyDefinition(property, botConfiguration);
        }

        private static PropertyDefinition ParseListBinding(PropertyInfo property, MemberListBinding listBinding)
        {
            var itemType = property.PropertyType.IsGenericType
                               ? property.PropertyType.GetGenericArguments()[0]
                               : typeof(object);
            return new PropertyDefinition(
                property,
                new ListOfGenerators(
                    itemType,
                    listBinding.Initializers.Select(x => ParseGeneratorVariable(x.Arguments[0])).ToArray()));
        }
    }
}