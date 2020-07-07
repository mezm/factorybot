﻿using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_ArrayTests
    {
        [TearDown]
        public void Terminate() => BotConfigurator.ForgetAll();

        [Test]
        public void Build_NestedPrimitiveArrayWithGenerator_ReturnsArray()
        {
            BotConfigurator.Configure(x => new Model4 { SimpleArray = x.Array(1, 5, x.Strings.Words(1, 1)) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleArray, Is.Not.Null.And.Length.InRange(1, 5).And.All.Match(@"^[\w\-]+$"));
        }

        [Test]
        public void Build_NestedPrimitiveConstantArray_ReturnsConstantArray()
        {
            BotConfigurator.Configure(x => new Model4 { SimpleArray = new[] { "a", "b", "c" } });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleArray, Is.Not.Null.And.EquivalentTo(new[] { "a", "b", "c" }));
        }

        [Test]
        public void Build_NestedPrimitiveArrayOfConstants_ReturnsArray()
        {
            BotConfigurator.Configure(x => new Model4 { SimpleArray = x.Array(3, 3, "abc") });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleArray, Is.Not.Null.And.Length.EqualTo(3).And.All.EqualTo("abc"));
        }

        [Test]
        public void Build_NestedComplexArrayUsingKnownConfig_ReturnsArray()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(100, 150), "the test"));
            BotConfigurator.Configure(x => new Model3 { Number = 7, Nested = x.Use<Model1>() });
            BotConfigurator.Configure(x => new Model4 { ComplexArray = x.Array(1, 3, x.Use<Model3>()) });

            var model = Bot.Build<Model4>();

            Assert.That(
                model.ComplexArray,
                Is.Not.Null
                    .And.Length.InRange(1, 3)
                    .And.All.Not.Null
                    .And.All.Property("Nested").Property("Number").InRange(100, 150));
        }

        [Test]
        public void Build_NestedComplexArrayUsingKnownConfigSimplifiedSytax_ReturnsArray()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(100, 150), "the test"));
            BotConfigurator.Configure(x => new Model3 { Number = 7, Nested = x.Use<Model1>() });
            BotConfigurator.Configure(x => new Model4 { ComplexArray = x.Array<Model3>(1, 3) });

            var model = Bot.Build<Model4>();

            Assert.That(
                model.ComplexArray,
                Is.Not.Null
                    .And.Length.InRange(1, 3)
                    .And.All.Not.Null
                    .And.All.Property("Nested").Property("Number").InRange(100, 150));
        }

        [Test]
        public void Build_NestedComplexArrayUsingUnknownConfigSimplifiedSytax_ThrowsError()
        {
            BotConfigurator.Configure(x => new Model4 { ComplexArray = x.Array<Model3>(1, 3) });

            Assert.Throws<UnknownTypeException>(() => Bot.Build<Model4>());
        }

        [Test]
        public void Build_NestedComplexArrayUsingNestedConfig_ReturnsArray()
        {
            BotConfigurator.Configure(
                x =>
                new Model4
                {
                    ComplexArray =
                            x.Array(
                                1,
                                3,
                                new Model3
                                {
                                    Number = 7,
                                    Nested = new Model1(x.Integer.Any(100, 150), "the test")
                                })
                });

            var model = Bot.Build<Model4>();

            Assert.That(
                model.ComplexArray,
                Is.Not.Null
                    .And.Length.InRange(1, 3)
                    .And.All.Not.Null
                    .And.All.Property("Nested").Property("Number").InRange(100, 150));
        }
    }
}
