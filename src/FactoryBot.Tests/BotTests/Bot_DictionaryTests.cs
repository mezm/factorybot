using FactoryBot.Tests.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_DictionaryTests
    {
        [TearDown]
        public void Terminate() => BotConfigurator.ForgetAll();

        [Test]
        public void Build_NestedPrimitiveDictionaryWithGenerators_ReturnsDictionary()
        {
            BotConfigurator.Configure(x => new Model4 { SimpleDictionary = x.Dictionary(2, 5, x.Integer.Any(10, 20), x.Strings.Any()) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleDictionary, Is.Not.Null.And.Count.InRange(2, 5));
        }

        [Test]
        public void Build_NestedPrimitiveConstantDictionary_ReturnsDictionary()
        {
            BotConfigurator.Configure(x => new Model4 { SimpleDictionary = new Dictionary<int, string> { { 1, "test" }, { 3, "test2" } } });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleDictionary, Is.Not.Null.And.EqualTo(new Dictionary<int, string> { [1] = "test", [3] = "test2" }));
        }

        [Test]
        public void Build_NestedComplexDictionaryUsingKnownConfig_ReturnsDictionary()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(100, 150), "the test"));
            BotConfigurator.Configure(x => new Model2(x.Integer.Any(10, 15)));
            BotConfigurator.Configure(x => new Model4 { ComplexDictionary = x.Dictionary(1, 5, x.Use<Model1>(), x.Use<Model2>()) });

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
        public void Build_NestedComplexDictionaryUsingKnownConfigSimplifiedSyntax_ReturnsDictionary()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(100, 150), "the test"));
            BotConfigurator.Configure(x => new Model2(x.Integer.Any(10, 15)));
            BotConfigurator.Configure(x => new Model4 { ComplexDictionary = x.Dictionary<Model1, Model2>(1, 5) });

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
        public void Build_NestedComplexDictionaryUsingUnknownKeyConfigSimplifiedSintax_ThrowsError()
        {
            BotConfigurator.Configure(x => new Model2(x.Integer.Any(10, 15)));
            BotConfigurator.Configure(x => new Model4 { ComplexDictionary = x.Dictionary<Model1, Model2>(1, 5) });

            Assert.Throws<UnknownTypeException>(() => Bot.Build<Model4>());
        }

        [Test]
        public void Build_NestedComplexDictionaryUsingUnknownValueConfigSimplifiedSyntax_ThrowsError()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(100, 150), "the test"));
            BotConfigurator.Configure(x => new Model4 { ComplexDictionary = x.Dictionary<Model1, Model2>(1, 5) });

            Assert.Throws<UnknownTypeException>(() => Bot.Build<Model4>());
        }

        [Test]
        public void Build_NestedComplexDictionaryUsingNestedConfig_ReturnsDictionary()
        {
            BotConfigurator.Configure(x => new Model4 { ComplexDictionary = x.Dictionary(2, 5, new Model1(x.Integer.Any(100, 150)), new Model2(x.Integer.Any(1, 5))) });

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
        public void Build_NestedPrimitiveDictionaryOfConstant_ReturnsDictionary()
        {
            BotConfigurator.Configure(x => new Model4 { SimpleDictionary = x.Dictionary(4, 7, x.Integer.SequenceFromList(1, 2, 3, 4, 5, 6, 7), "test") });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleDictionary, Is.Not.Null.And.Count.InRange(4, 7));
            Assert.That(model.SimpleDictionary.Values, Is.All.EqualTo("test"));
        }

        [Test]
        public void Build_WithKeyDuplication_ReturnsDictionary()
        {
            BotConfigurator.Configure(x => new Model4 { SimpleDictionary = x.Dictionary(5, 5, x.Integer.SequenceFromList(1, 1, 2, 2, 3, 4, 5, 5, 5), x.Strings.Any()) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleDictionary, Is.Not.Null.And.Count.EqualTo(5));
        }

        [Test]
        public void Build_UnableToDoDueToLackOfUniqueKeys_ThrowsError()
        {
            BotConfigurator.Configure(x => new Model4 { SimpleDictionary = x.Dictionary(5, 5, x.Integer.SequenceFromList(1, 2, 3), x.Strings.Any()) });

            Assert.Throws<BuildFailedException>(() => Bot.Build<Model4>());
        }
    }
}
