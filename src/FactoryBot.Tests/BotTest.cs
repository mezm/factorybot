using System;

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
            Bot.Define(x => new TestModel1(10, "text"));

            var model1 = Bot.Build<TestModel1>();
            var model2 = Bot.Build<TestModel1>();
            
            Assert.That(model1, Is.Not.EqualTo(model2));
        }

        [Test]
        public void BuildConstructorArgumentsOnly()
        {
            Bot.Define(x => new TestModel1(10, "text"));

            GetModelsAndAssertTheSame<TestModel1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(10));
                    Assert.That(x.Text, Is.EqualTo("text"));
                });
        }

        [Test]
        public void BuildPropertiesOnly()
        {
            Bot.Define(x => new TestModel1 { Number = 13, Text = "some text" });

            GetModelsAndAssertTheSame<TestModel1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(13));
                    Assert.That(x.Text, Is.EqualTo("some text"));
                });
        }

        [Test]
        public void BuildConstructorArgumentAndProperty()
        {
            Bot.Define(x => new TestModel1(10) { Text = "some text" });

            GetModelsAndAssertTheSame<TestModel1>(
                x =>
                    {
                        Assert.That(x.Number, Is.EqualTo(10));
                        Assert.That(x.Text, Is.EqualTo("some text"));
                    });
        }

        [Test]
        public void BuildWithGenerators()
        {
            Bot.Define(x => new TestModel1(x.Numbers.AnyInteger(10, 25)) { Text = x.Strings.Any() });

            GetModelsAndAssertTheSame<TestModel1>(
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
            Bot.Define(x => new TestModel1(number) { Text = text });

            GetModelsAndAssertTheSame<TestModel1>(
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
            Bot.Define(x => new TestModel1(data.Number, data.Text));

            GetModelsAndAssertTheSame<TestModel1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(101));
                    Assert.That(x.Text, Is.EqualTo("abc"));
                });
        }

        [Test]
        public void BuildWithOtherClassMethod()
        {
            var data = new OtherClass1();
            Bot.Define(x => new TestModel1(data.GetNumber(), data.GetText()));

            GetModelsAndAssertTheSame<TestModel1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(912));
                    Assert.That(x.Text, Is.EqualTo("test"));
                });
        }

        [Test]
        public void BuildWithOtherClassPropertyAndMethod()
        {
            var data = new OtherClass2();
            Bot.Define(x => new TestModel1(data.Other.GetNumber(), data.Other.GetText()));

            GetModelsAndAssertTheSame<TestModel1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(912));
                    Assert.That(x.Text, Is.EqualTo("test"));
                });
        }

        [Test]
        public void BuildWithMethodCall()
        {
            Bot.Define(x => new TestModel1(GetNumberInternal()) { Text = GetTextInternal() });

            GetModelsAndAssertTheSame<TestModel1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(111));
                    Assert.That(x.Text, Is.EqualTo("a"));
                });
        }

        [Test]
        public void BuildWithConstantValueOnlyAndWithNested()
        {
            Bot.Define(x => new TestModel2(136) { Date = new DateTime(2016, 1, 22) });

            GetModelsAndAssertTheSame<TestModel2>(
                x =>
                    {
                        Assert.That(x.Number, Is.EqualTo(136));
                        Assert.That(x.Date, Is.EqualTo(new DateTime(2016, 1, 22)));
                    });

        }

        [Test]
        public void BuildWithSingleModifier()
        {
            Bot.Define(x => new TestModel1(x.Numbers.AnyInteger(10, 20)));

            var model = Bot.Build<TestModel1>(x => x.Number = 100);
            Assert.That(model.Number, Is.EqualTo(100));
        }

        [Test]
        public void BuildWithMultipleModifier()
        {
            Bot.Define(x => new TestModel1(x.Numbers.AnyInteger(10, 20), "t1"));

            var model = Bot.Build<TestModel1>(x => x.Number = 100, x => x.Text = "tt2");

            Assert.That(model.Number, Is.EqualTo(100));
            Assert.That(model.Text, Is.EqualTo("tt2"));
        }

        [Test]
        public void BuildWithConstructorModifier()
        {
            Bot.Define(x => new TestModel1(x.Numbers.AnyInteger(10, 20), ""));

            var model = Bot.BuildCustom(x => new TestModel1(5, x.Builder.Strings.Any()));

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void BuildWithConstructorModifierWithKeep()
        {
            Bot.Define(x => new TestModel1(x.Numbers.AnyInteger(10, 20), "the text"));

            var model = Bot.BuildCustom(x => new TestModel1(5, x.Keep<string>()));

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.EqualTo("the text"));
        }

        [Test]
        public void BuildWithConstructorModifierArgumentsMismatch()
        {
            Bot.Define(x => new TestModel1(x.Numbers.AnyInteger(10, 20), "the text"));
            
            Assert.That(() => Bot.BuildCustom(x => new TestModel1(5)), Throws.InvalidOperationException);
        }

        [Test]
        public void BuildWithConstructorModifierAndPostConstructModifiers()
        {
            Bot.Define(x => new TestModel1(x.Numbers.AnyInteger(10, 20)));

            var model = Bot.BuildCustom(x => new TestModel1(5), x => x.Text = "a");

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.EqualTo("a"));
        }

        [Test]
        public void BuildNotDefinedType()
        {
            Assert.That(() => Bot.Build<TestModel1>(), Throws.TypeOf<UnknownTypeException>());
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

        private class TestModel1
        {
            public TestModel1()
            {
            }

            public TestModel1(int number)
            {
                Number = number;
            }

            public TestModel1(int number, string text)
            {
                Number = number;
                Text = text;
            }

            public int Number { get; set; }

            public string Text { get; set; }
        }

        private class TestModel2
        {
            public TestModel2(int number)
            {
                Number = number;
            }

            public int Number { get; }

            public DateTime Date { get; set; }
        }

        private class OtherClass1
        {
            public int GetNumber()
            {
                return 912;
            }

            public string GetText()
            {
                return "test";
            }
        }

        private class OtherClass2
        {
            public OtherClass1 Other { get; } = new OtherClass1();
        }
    }
}
