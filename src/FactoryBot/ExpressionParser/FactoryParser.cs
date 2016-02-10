using System;
using System.Linq.Expressions;
using System.Reflection;

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
                var config = ParseConstructor(memberInitExpr.NewExpression);
                foreach (var binding in memberInitExpr.Bindings)
                {
                    config.Properties.Add(ParseMemberBinding(binding));
                }

                return config;
            }

            var newExpr = factory.Body as NewExpression;
            if (newExpr != null)
            {
                return ParseConstructor(newExpr);
            }

            throw new NotSupportedException();
        }

        private static BotConfiguration ParseConstructor(NewExpression newExpr)
        {
            var constructorGenerator = ExpressionParserHelper.CreateConstructorGenerator(newExpr);
            return new BotConfiguration(newExpr.Type, constructorGenerator);
        }

        private static PropertyGenerator ParseMemberBinding(MemberBinding binding)
        {
            if (binding.BindingType == MemberBindingType.Assignment)
            {
                var assignmentBinding = (MemberAssignment)binding;
                return new PropertyGenerator(
                    (PropertyInfo)binding.Member,
                    ExpressionParserHelper.ParseGeneratorVariable(assignmentBinding.Expression));
            }

            throw new NotSupportedException();
        }
    }
}