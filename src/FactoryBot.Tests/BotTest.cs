using System;
using System.Linq;

using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests
{
    [TestFixture]
    public class BotTest
    {
        [TearDown]
        public void Terminate()
        {
            Bot.ForgetAll();
        }

        [Test]
        public void BuildAlwaysCreatesNewInstance()
        {
            Bot.Define(x => new Model1(10, "text"));

            var model1 = Bot.Build<Model1>();
            var model2 = Bot.Build<Model1>();
            
            Assert.That(model1, Is.Not.EqualTo(model2));
        }

        [Test]
        public void BuildConstructorArgumentsOnly()
        {
            Bot.Define(x => new Model1(10, "text"));

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(10));
                    Assert.That(x.Text, Is.EqualTo("text"));
                });
        }

        [Test]
        public void BuildPropertiesOnly()
        {
            Bot.Define(x => new Model1 { Number = 13, Text = "some text" });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(13));
                    Assert.That(x.Text, Is.EqualTo("some text"));
                });
        }

        [Test]
        public void BuildConstructorArgumentAndProperty()
        {
            Bot.Define(x => new Model1(10) { Text = "some text" });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                    {
                        Assert.That(x.Number, Is.EqualTo(10));
                        Assert.That(x.Text, Is.EqualTo("some text"));
                    });
        }

        [Test]
        public void BuildWithGenerators()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 25)) { Text = x.Strings.Any() });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.GreaterThanOrEqualTo(10).And.LessThanOrEqualTo(25));
                    Assert.That(x.Text, Is.Not.Null);
                });
        }

        [Test]
        public void BuildWithLocalVariables()
        {
            var number = 889;
            var text = "some t";
            Bot.Define(x => new Model1(number) { Text = text });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(889));
                    Assert.That(x.Text, Is.EqualTo("some t"));
                });
        }

        [Test]
        public void BuildWithOtherClassProperty()
        {
            var data = new { Number = 101, Text = "abc" };
            Bot.Define(x => new Model1(data.Number, data.Text));

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(101));
                    Assert.That(x.Text, Is.EqualTo("abc"));
                });
        }

        [Test]
        public void BuildWithOtherClassMethod()
        {
            var data = new DataHolder1();
            Bot.Define(x => new Model1(data.GetNumber(), data.GetText()));

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(912));
                    Assert.That(x.Text, Is.EqualTo("test"));
                });
        }

        [Test]
        public void BuildWithOtherClassPropertyAndMethod()
        {
            var data = new DataHolder2();
            Bot.Define(x => new Model1(data.Other.GetNumber(), data.Other.GetText()));

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(912));
                    Assert.That(x.Text, Is.EqualTo("test"));
                });
        }

        [Test]
        public void BuildWithMethodCall()
        {
            Bot.Define(x => new Model1(GetNumberInternal()) { Text = GetTextInternal() });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(111));
                    Assert.That(x.Text, Is.EqualTo("a"));
                });
        }

        [Test]
        public void BuildWithConstantValueOnlyAndWithNested()
        {
            Bot.Define(x => new Model2(136) { Date = new DateTime(2016, 1, 22) });

            GetModelsAndAssertTheSame<Model2>(
                x =>
                    {
                        Assert.That(x.Number, Is.EqualTo(136));
                        Assert.That(x.Date, Is.EqualTo(new DateTime(2016, 1, 22)));
                    });

        }

        [Test]
        public void BuildWithSingleModifier()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 20)));

            var model = Bot.Build<Model1>(x => x.Number = 100);
            Assert.That(model.Number, Is.EqualTo(100));
        }

        [Test]
        public void BuildWithMultipleModifier()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 20), "t1"));

            var model = Bot.Build<Model1>(x => x.Number = 100, x => x.Text = "tt2");

            Assert.That(model.Number, Is.EqualTo(100));
            Assert.That(model.Text, Is.EqualTo("tt2"));
        }

        [Test]
        public void BuildWithConstructorModifier()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 20), ""));

            var model = Bot.BuildCustom(x => new Model1(5, x.Builder.Strings.Any()));

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void BuildWithConstructorModifierWithKeep()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 20), "the text"));

            var model = Bot.BuildCustom(x => new Model1(5, x.Keep<string>()));

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.EqualTo("the text"));
        }

        [Test]
        public void BuildWithConstructorModifierArgumentsMismatch()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 20), "the text"));
            
            Assert.That(() => Bot.BuildCustom(x => new Model1(5)), Throws.InvalidOperationException);
        }

        [Test]
        public void BuildWithConstructorModifierAndPostConstructModifiers()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 20)));

            var model = Bot.BuildCustom(x => new Model1(5), x => x.Text = "a");

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.EqualTo("a"));
        }

        [Test]
        public void BuildNotDefinedType()
        {
            Assert.That(() => Bot.Build<Model1>(), Throws.TypeOf<UnknownTypeException>());
        }

        [Test]
        public void BuildWithUsingNestedConfigurations()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(100, 150), "the test"));
            Bot.Define(x => new Model3 { Number = 7, Nested = x.Use<Model1>() });

            var model = Bot.Build<Model3>();

            Assert.That(model.Number, Is.EqualTo(7));
            Assert.That(model.Nested.Number, Is.GreaterThanOrEqualTo(100).And.LessThanOrEqualTo(150));
            Assert.That(model.Nested.Text, Is.EqualTo("the test"));
        }

        [Test]
        public void DefineConfigurationWithUnknownNested()
        {
            Assert.That(() => Bot.Define(x => new Model3 { Number = 7, Nested = x.Use<Model1>() }),
                Throws.InstanceOf<UnknownTypeException>());
        }

        [Test]
        public void BuildNestedPrimitiveArrayWithGenerator()
        {
            Bot.Define(x => new Model4 { SimpleArray = x.Array(1, 5, x.Strings.Words(1, 1)) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleArray, Is.Not.Null.And.Length.InRange(1, 5).And.All.Match(@"^\w+$"));
        }

        [Test]
        public void BuildNestedPrimitiveArrayWithConstant()
        {
            Bot.Define(x => new Model4 { SimpleArray = x.Array(3, 3, "abc") });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleArray, Is.Not.Null.And.Length.EqualTo(3).And.All.EqualTo("abc"));
        }

        [Test]
        public void BuildNestedComplexArray()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(100, 150), "the test"));
            Bot.Define(x => new Model3 { Number = 7, Nested = x.Use<Model1>() });
            Bot.Define(x => new Model4 { ComplexArray = x.Array(1, 3, x.Use<Model3>()) });

            var model = Bot.Build<Model4>();

            Assert.That(model.ComplexArray, Is.Not.Null.And.Length.InRange(1, 3).And.All.Not.Null);
        }

        [Test]
        public void BuildNestedPrimitiveListWithGenerator()
        {
            Bot.Define(x => new Model4 { SimpleList = x.List(1, 5, x.Numbers.AnyInteger(100, 200)) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleList, Is.Not.Null.And.Count.InRange(1, 5).And.All.InRange(100, 200));
        }

        [Test]
        public void BuildNestedPrimitiveListWithConstant()
        {
            Bot.Define(x => new Model4 { SimpleList = x.List(3, 3, 54670) });

            var model = Bot.Build<Model4>();

            Assert.That(model.SimpleList, Is.Not.Null.And.Count.EqualTo(3).And.All.EqualTo(54670));
        }

        [Test]
        public void BuildNestedComplexList()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(100, 150), "the test"));
            Bot.Define(x => new Model4 { ComplexList = x.List(1, 3, x.Use<Model1>()) });

            var model = Bot.Build<Model4>();

            Assert.That(model.ComplexList, Is.Not.Null.And.Count.InRange(1, 3).And.All.Not.Null);
        }

        [Test]
        public void BuildSequence()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 25)) { Text = x.Strings.Any() });

            foreach (var model in Bot.BuildSequence<Model1>().Take(5))
            {
                Assert.That(model, Is.Not.Null);
                Assert.That(model.Number, Is.GreaterThanOrEqualTo(10).And.LessThanOrEqualTo(25));
                Assert.That(model.Text, Is.Not.Null);
            }
        }

        [Test]
        public void BuildFiniteSequenceAndTakeMoreThenLimit()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 25)) { Text = x.Strings.Any() });

            Assert.That(() => Bot.BuildSequence<Model1>().Take(Bot.SequenceMaxLength).ToArray(), Throws.Nothing);
            Assert.That(() => Bot.BuildSequence<Model1>().Take(Bot.SequenceMaxLength + 1).ToArray(), Throws.InvalidOperationException);
        }

        [Test]
        public void BuildInfiniteSequence()
        {
            Bot.Define(x => new Model1(x.Numbers.AnyInteger(10, 25)) { Text = x.Strings.Any() });

            var models = Bot.BuildSequence<Model1>(true).Take(Bot.SequenceMaxLength + 10).ToArray();

            Assert.That(models, Has.Length.EqualTo(Bot.SequenceMaxLength + 10));
        }

        private static void GetModelsAndAssertTheSame<TModel>(Action<TModel> assertions)
        {
            for (var i = 0; i < 3; i++)
            {
                var model = Bot.Build<TModel>();
                assertions(model);
            }
        }

        private static int GetNumberInternal()
        {
            return 111;
        }

        private string GetTextInternal()
        {
            return "a";
        }
    }
}
