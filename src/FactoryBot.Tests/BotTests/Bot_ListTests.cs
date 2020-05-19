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
        public void Build_NestedPrimitiveListWithGenerator_ReturnsList()
        {
            Bot.Define(x => new Model4 { SimpleList = x.List(1, 5, x.Integer.Any(100, 200)) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleList, Is.Not.Null.And.Count.InRange(1, 5).And.All.InRange(100, 200));
        }

        [Test]
        public void Build_NestedPrimitiveConstantList_ReturnsList()
        {
            Bot.Define(x => new Model4 { SimpleList = { 1, 2, 100, -12 } });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleList, Is.Not.Null.And.EquivalentTo(new[] { 1, 2, 100, -12 }));
        }

        [Test]
        public void Build_NestedPrimitiveListOfConstant_ReturnsList()
        {
            Bot.Define(x => new Model4 { SimpleList = x.List(3, 3, 54670) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleList, Is.Not.Null.And.Count.EqualTo(3).And.All.EqualTo(54670));
        }

        [Test]
        public void Build_NestedComplexListUsingKnownConfig_ReturnsList()
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
        public void Build_NestedComplexListUsingKnownConfigSimplyfiedSyntax_ReturnsList()
        {
            Bot.Define(x => new Model1(x.Integer.Any(100, 150), "the test"));
            Bot.Define(x => new Model4 { ComplexList = x.List<Model1>(1, 3) });

            var model = Bot.Build<Model4>();

            Assert.That(
                model.ComplexList,
                Is.Not.Null
                    .And.Count.InRange(1, 3)
                    .And.All.Not.Null
                    .And.All.Property("Number").InRange(100, 150));
        }

        [Test]
        public void Build_NestedComplexListUsingUnknownConfigSimplyfiedSyntax_ThrowsError()
        {
            Bot.Define(x => new Model4 { ComplexList = x.List<Model1>(1, 3) });

            Assert.Throws<UnknownTypeException>(() => Bot.Build<Model4>());
        }

        [Test]
        public void Build_NestedComplexListUsingNestedConfig_ReturnsList()
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
