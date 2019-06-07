using FactoryBot.Tests.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_DictionaryTests
    {
        [TearDown]
        public void Terminate() => Bot.ForgetAll();

        [Test]
        public void BuildNestedPrimitiveDictionaryWithGenerators()
        {
            Bot.Define(x => new Model4 { SimpleDictionary = x.Dictionary(2, 5, x.Integer.Any(10, 20), x.Strings.Any()) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleDictionary, Is.Not.Null.And.Count.InRange(2, 5));
        }

        [Test]
        public void BuildNestedPrimitiveConstantDictionary()
        {
            Bot.Define(x => new Model4 { SimpleDictionary = new Dictionary<int, string> { { 1, "test" }, { 3, "test2" } } });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleDictionary, Is.Not.Null.And.EqualTo(new Dictionary<int, string> { [1] = "test", [3] = "test2" }));
        }

        [Test]
        public void BuildNestedComplexDictionaryUsingKnownConfig()
        {
            Bot.Define(x => new Model1(x.Integer.Any(100, 150), "the test"));
            Bot.Define(x => new Model2(x.Integer.Any(10, 15)));
            Bot.Define(x => new Model4 { ComplexDictionary = x.Dictionary(1, 5, x.Use<Model1>(), x.Use<Model2>()) });

            var model = Bot.Build<Model4>();

            Assert.That(
                model.ComplexDictionary,
                Is.Not.Null
                    .And.Count.InRange(1, 5)
                    .And.All.Not.Null
                    .And.All.Property("Key").Property("Number").InRange(100, 150)
                    .And.All.Property("Value").Property("Number").InRange(10, 15));
        }

        [Test]
        public void BuildNestedComplexDictionaryUsingNestedConfig()
        {
            Bot.Define(x => new Model4 { ComplexDictionary = x.Dictionary(2, 5, new Model1(x.Integer.Any(100, 150)), new Model2(x.Integer.Any(1, 5))) });

            var model = Bot.Build<Model4>();

            Assert.That(
                model.ComplexDictionary,
                Is.Not.Null
                    .And.Count.InRange(2, 5)
                    .And.All.Not.Null
                    .And.All.Property("Key").Property("Number").InRange(100, 150)
                    .And.All.Property("Value").Property("Number").InRange(1, 5));
        }

        [Test]
        public void BuildNestedPrimitiveDictionaryOfConstant()
        {
            Bot.Define(x => new Model4 { SimpleDictionary = x.Dictionary(4, 7, 5, "test") });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleDictionary, Is.Not.Null.And.Count.EqualTo(1).And.All.EqualTo(new KeyValuePair<int, string>(5, "test")));
        }
    }
}
