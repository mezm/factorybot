using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using FactoryBot.DSL;
using FactoryBot.Tests.Models;

using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace FactoryBot.Tests.Generators
{
    public abstract class GeneratorTestKit
    {
        protected void AssertGeneratorValue(
            Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory,
            IResolveConstraint constraint)
        {
            AssertGeneratorValue(factory, constraint, new IResolveConstraint[0]);
        }

        protected void AssertGeneratorValue(
            Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory,
            IResolveConstraint constraint,
            params IResolveConstraint[] constraints)
        {
            var asserts = new[] {ConstraintToAssertAction(constraint)}
                .Concat(constraints.Select(ConstraintToAssertAction))
                .ToArray();
            AssertGenetorValuesInRow(factory, asserts);
        }

        protected void AssertGeneratorValue<T>(
            Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory,
            Action<T> assert)
        {
            AssertGenetorValuesInRow(factory, x => assert((T) x));
        }

        protected void AssertGeneratorValuesAreNotTheSame(
            Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory,
            int count = 3)
        {
            if (count < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            var memberInitExpr = (MemberInitExpression)factory.Body;
            var property = (PropertyInfo)memberInitExpr.Bindings[0].Member;

            Bot.Define(factory);

            var values =
                Enumerable.Range(0, count)
                    .Select(x => Bot.Build<AllTypesModel>())
                    .Select(x => property.GetValue(x))
                    .ToArray();
            Assert.That(values, Is.Unique);
        }

        protected void ExpectInitException<T>(Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory)
            where T : Exception
        {   
            Assert.That(
                () => Bot.Define(factory),
                Throws.InstanceOf<GeneratorInitializationException>().And.InnerException.InstanceOf<T>());
        }

        protected void ExpectArgumentInitException(Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory)
        {
            ExpectInitException<ArgumentException>(factory);
        }

        protected void ExpectArgumentOutOfRangeInitException(
            Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory)
        {
            ExpectInitException<ArgumentOutOfRangeException>(factory);
        }

        protected void ExpectBuildException<T>(Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory)
            where T : Exception
        {
            Bot.Define(factory);
            Assert.That(() => Bot.Build<AllTypesModel>(), Throws.InstanceOf<T>());
        }

        private static void AssertGenetorValuesInRow(Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory, params Action<object>[] asserts)
        {
            var memberInitExpr = (MemberInitExpression)factory.Body;
            var property = (PropertyInfo)memberInitExpr.Bindings[0].Member;

            Bot.Define(factory);

            foreach (var assert in asserts)
            {
                var model = Bot.Build<AllTypesModel>();
                var value = property.GetValue(model);
                assert(value);
            }
        }

        private static Action<object> ConstraintToAssertAction(IResolveConstraint constraint)
        {
            return x => Assert.That(x, constraint);
        }
    }
}