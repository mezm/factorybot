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
            AssertGeneratorValue<object>(
                factory,
                x =>
                    {
                        Assert.That(x, constraint);
                        foreach (var resolveConstraint in constraints)
                        {
                            Assert.That(x, resolveConstraint);
                        }
                    });
        }

        protected void AssertGeneratorValue<T>(
            Expression<Func<BotConfigurationBuilder, AllTypesModel>> factory,
            Action<T> assert)
        {
            var memberInitExpr = (MemberInitExpression)factory.Body;
            var property = (PropertyInfo)memberInitExpr.Bindings[0].Member;

            Bot.Define(factory);

            var model = Bot.Build<AllTypesModel>();
            var value = (T)property.GetValue(model);
            assert(value);
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
        {
            Bot.Define(factory);
            Assert.That(() => Bot.Build<AllTypesModel>(), Throws.InstanceOf<T>());
        }
    }
}