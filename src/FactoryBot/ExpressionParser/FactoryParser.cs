using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using FactoryBot.Configurations;
using FactoryBot.DSL;
using FactoryBot.Generators;

namespace FactoryBot.ExpressionParser
{
    internal class FactoryParser
    {
        private BotConfiguration _configuration;

        public BotConfiguration Parse<T>(Expression<Func<BotBuilder, T>> factory)
        {
            _configuration = new BotConfiguration();
            var meberInitExpr = factory.Body as MemberInitExpression;
            if (meberInitExpr != null)
            {
                ParseConstructor(meberInitExpr.NewExpression);
                foreach (var binding in meberInitExpr.Bindings)
                {
                    ParseMemberBinding(binding);
                }

                return _configuration;
            }

            var newExpr = factory.Body as NewExpression;
            if (newExpr != null)
            {
                ParseConstructor(newExpr);

                return _configuration;
            }

            throw new NotSupportedException();
        }

        private void ParseConstructor(NewExpression newExpr)
        {
            _configuration.ConstructingType = newExpr.Type;
            _configuration.Constructor = newExpr.Constructor;
            _configuration.ConstructorArguments.AddRange(newExpr.Arguments.Select(ParseGeneratorVariable));
        }

        private void ParseMemberBinding(MemberBinding binding)
        {
            if (binding.BindingType == MemberBindingType.Assignment)
            {
                var assignmentBinding = (MemberAssignment)binding;
                var propertyGenerator = new PropertyGenerator
                                            {
                                                Property = (PropertyInfo)binding.Member,
                                                Generator = ParseGeneratorVariable(assignmentBinding.Expression)
                                            };
                _configuration.Properties.Add(propertyGenerator);
                return;
            }

            throw new NotSupportedException();
        }

        private static IGenerator ParseGeneratorVariable(Expression expr)
        {
            var methodCallExpr = expr as MethodCallExpression;
            var generatorAttr = methodCallExpr?.Method.GetCustomAttribute<GeneratorAttribute>();
            if (generatorAttr != null)
            {
                var generatorParamenters = methodCallExpr.Arguments.Select(EvualateExpression).ToArray();
                return generatorAttr.CreateGenerator(generatorParamenters);
            }

            return new ConstantGenerator(EvualateExpression(expr));
        }
        
        private static object EvualateExpression(Expression expr)
        {
            return Expression.Lambda(expr).Compile().DynamicInvoke();
        }
    }
}