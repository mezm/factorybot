using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_ListTests
    {
        [TearDown]
        public void Terminate() => Bot.ForgetAll();

        [Test]
        public void BuildNestedPrimitiveListWithGenerator()
        {
            Bot.Define(x => new Model4 { SimpleList = x.List(1, 5, x.Integer.Any(100, 200)) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleList, Is.Not.Null.And.Count.InRange(1, 5).And.All.InRange(100, 200));
        }

        [Test]
        public void BuildNestedPrimitiveConstantList()
        {
            Bot.Define(x => new Model4 { SimpleList = { 1, 2, 100, -12 } });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleList, Is.Not.Null.And.EquivalentTo(new[] { 1, 2, 100, -12 }));
        }

        [Test]
        public void BuildNestedPrimitiveListOfConstant()
        {
            Bot.Define(x => new Model4 { SimpleList = x.List(3, 3, 54670) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleList, Is.Not.Null.And.Count.EqualTo(3).And.All.EqualTo(54670));
        }

        [Test]
        public void BuildNestedComplexListUsingKnownConfig()
        {
            Bot.Define(x => new Model1(x.Integer.Any(100, 150), "the test"));
            Bot.Define(x => new Model4 { ComplexList = x.List(1, 3, x.Use<Model1>()) });

            var model = Bot.Build<Model4>();

            Assert.That(
                model.ComplexList,
                Is.Not.Null
                    .And.Count.InRange(1, 3)
                    .And.All.Not.Null
                    .And.All.Property("Number").InRange(100, 150));
        }

        [Test]
        public void BuildNestedComplexListUsingNestedConfig()
        {
            Bot.Define(
                x => new Model4 { ComplexList = x.List(1, 3, new Model1(x.Integer.Any(100, 150), "the test")) });

            var model = Bot.Build<Model4>();

            Assert.That(
                model.ComplexList,
                Is.Not.Null
                    .And.Count.InRange(1, 3)
                    .And.All.Not.Null
                    .And.All.Property("Number").InRange(100, 150));
        }

    }
}
