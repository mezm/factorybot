using System;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_CommonTest
    {
        [TearDown]
        public void Terminate() => BotConfigurator.ForgetAll();

        [Test]
        public void Build_NoCondition_ReturnsNewInstance()
        {
            BotConfigurator.Configure(x => new Model1(10, "text"));

            var model1 = Bot.Build<Model1>();
            var model2 = Bot.Build<Model1>();

            Assert.That(model1, Is.Not.EqualTo(model2));
        }

        [Test]
        public void Build_ConstructorArgumentsOnly_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(10, "text"));

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(10));
                    Assert.That(x.Text, Is.EqualTo("text"));
                });
        }

        [Test]
        public void Build_PropertiesOnly_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1 { Number = 13, Text = "some text" });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(13));
                    Assert.That(x.Text, Is.EqualTo("some text"));
                });
        }

        [Test]
        public void Build_ConstructorArgumentAndProperty_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(10) { Text = "some text" });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                    {
                        Assert.That(x.Number, Is.EqualTo(10));
                        Assert.That(x.Text, Is.EqualTo("some text"));
                    });
        }

        [Test]
        public void Build_WithGenerators_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.GreaterThanOrEqualTo(10).And.LessThanOrEqualTo(25));
                    Assert.That(x.Text, Is.Not.Null);
                });
        }

        [Test]
        public void Build_WithLocalVariables_ReturnsObject()
        {
            var number = 889;
            var text = "some t";
            BotConfigurator.Configure(x => new Model1(number) { Text = text });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(889));
                    Assert.That(x.Text, Is.EqualTo("some t"));
                });
        }

        [Test]
        public void Build_WithOtherClassProperty_ReturnsObject()
        {
            var data = new { Number = 101, Text = "abc" };
            BotConfigurator.Configure(x => new Model1(data.Number, data.Text));

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(101));
                    Assert.That(x.Text, Is.EqualTo("abc"));
                });
        }

        [Test]
        public void Build_WithOtherClassMethod_ReturnsObject()
        {
            var data = new DataHolder1();
            BotConfigurator.Configure(x => new Model1(data.GetNumber(), data.GetText()));

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(912));
                    Assert.That(x.Text, Is.EqualTo("test"));
                });
        }

        [Test]
        public void Build_WithOtherClassPropertyAndMethod_ReturnsObject()
        {
            var data = new DataHolder2();
            BotConfigurator.Configure(x => new Model1(data.Other.GetNumber(), data.Other.GetText()));

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(912));
                    Assert.That(x.Text, Is.EqualTo("test"));
                });
        }

        [Test]
        public void Build_WithMethodCall_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(GetNumberInternal()) { Text = GetTextInternal() });

            GetModelsAndAssertTheSame<Model1>(
                x =>
                {
                    Assert.That(x.Number, Is.EqualTo(111));
                    Assert.That(x.Text, Is.EqualTo("a"));
                });
        }

        [Test]
        public void Build_WithConstantValueOnlyAndWithNested_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model2(136) { Date = new DateTime(2016, 1, 22) });

            GetModelsAndAssertTheSame<Model2>(
                x =>
                    {
                        Assert.That(x.Number, Is.EqualTo(136));
                        Assert.That(x.Date, Is.EqualTo(new DateTime(2016, 1, 22)));
                    });

        }

        [Test]
        public void Build_WithSingleModifier_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 20)));

            var model = Bot.Build<Model1>(x => x.Number = 100);
            Assert.That(model.Number, Is.EqualTo(100));
        }

        [Test]
        public void Build_WithMultipleModifier_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 20), "t1"));

            var model = Bot.Build<Model1>(x => x.Number = 100, x => x.Text = "tt2");

            Assert.That(model.Number, Is.EqualTo(100));
            Assert.That(model.Text, Is.EqualTo("tt2"));
        }

        [Test]
        public void Build_WithConstructorModifier_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 20), ""));

            var model = Bot.BuildCustom(x => new Model1(5, x.Strings.Any()));

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        public void Build_WithConstructorModifierWithKeep_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 20), "the text"));

            var model = Bot.BuildCustom(x => new Model1(5, x.Keep<string>()));

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.EqualTo("the text"));
        }



        [Test]
        public void Build_WithConstructorModifierAndPostConstructModifiers_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 20)));

            var model = Bot.BuildCustom(x => new Model1(5), x => x.Text = "a");

            Assert.That(model.Number, Is.EqualTo(5));
            Assert.That(model.Text, Is.EqualTo("a"));
        }



        [Test]
        public void Build_WithUsingNestedConfigurations_ReturnsObject()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(100, 150), "the test"));
            BotConfigurator.Configure(x => new Model3 { Number = 7, Nested = x.Use<Model1>() });

            var model = Bot.Build<Model3>();

            Assert.That(model.Number, Is.EqualTo(7));
            Assert.That(model.Nested.Number, Is.GreaterThanOrEqualTo(100).And.LessThanOrEqualTo(150));
            Assert.That(model.Nested.Text, Is.EqualTo("the test"));
        }

        [Test]
        public void Build_WithNestedComplexObject_ReturnsObject()
        {
            BotConfigurator.Configure(
                x =>
                new Model3
                {
                    Number = 10,
                    Nested = new Model1(x.Integer.Any(10, 150)) { Text = x.Strings.Words() }
                });

            var model = Bot.Build<Model3>();

            Assert.That(model.Number, Is.EqualTo(10));
            Assert.That(model.Nested.Number, Is.InRange(10, 150));
            Assert.That(model.Nested.Text, Is.Not.Empty);
        }

        [Test]
        public void Build_WithNestedComplexObjectWithMemberInit_ReturnsObject()
        {
            BotConfigurator.Configure(
                x =>
                new Model3
                {
                    Number = 10,
                    Nested = { Number = x.Integer.Any(10, 150), Text = x.Strings.Words() }
                });

            var model = Bot.Build<Model3>();

            Assert.That(model.Number, Is.EqualTo(10));
            Assert.That(model.Nested.Number, Is.InRange(10, 150));
            Assert.That(model.Nested.Text, Is.Not.Empty);
        }

        private static void GetModelsAndAssertTheSame<TModel>(Action<TModel> assertions)
        {
            for (var i = 0; i < 3; i++)
            {
                var model = Bot.Build<TModel>();
                assertions(model);
            }
        }

        private static int GetNumberInternal() => 111;

        private string GetTextInternal() => "a";
    }
}
