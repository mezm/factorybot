using FactoryBot.Tests.Models;
using NUnit.Framework;
using System;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_HooksTest
    {
        [TearDown]
        public void Terminate() => Bot.ForgetAll();

        [Test]
        public void Build_WithBeforeBindingHook_PropertySetInHookIsSkipped()
        {
            Bot.Define(x => new Model1 { Number = x.Integer.Any(5, 100), Text = x.Strings.Any(10, 15) })
                .BeforePropertyBinding(x =>
                {
                    x.Number = -126;
                    x.Text = "t1";
                });

            var model = Bot.Build<Model1>();

            Assert.That(model.Number, Is.EqualTo(-126));
            Assert.That(model.Text, Is.EqualTo("t1"));
        }

        [Test]
        public void Build_WithBeforeBindingHookAndOnlyPropertyBinding_PropertiesAreEmptyInHook()
        {
            Bot.Define(x => new Model1 { Number = x.Integer.Any(5, 100), Text = x.Strings.Any(10, 15) })
                .BeforePropertyBinding(x =>
                {
                    Assert.That(x.Number, Is.EqualTo(0));
                    Assert.That(x.Text, Is.Null);
                });

            Bot.Build<Model1>();
        }

        [Test]
        public void Build_WithBeforeBindingHookAndOnlyConstructorBinding_PropertiesAreBind()
        {
            Bot.Define(x => new Model1(x.Integer.Any(5, 100), x.Strings.Any(10, 15)))
                .BeforePropertyBinding(x =>
                {
                    Assert.That(x.Number, Is.InRange(5, 100));
                    Assert.That(x.Text, Is.Not.Null.And.Length.InRange(10, 15));
                });

            Bot.Build<Model1>();
        }

        [Test]
        public void Build_WithBeforeBindingHookAndThrows_ExceptionPassed()
        {
            Bot.Define(x => new Model1 { Number = x.Integer.Any(5, 100), Text = x.Strings.Any(10, 15) })
                .BeforePropertyBinding(x => throw new NotSupportedException("test"));

            Assert.Throws<BuildFailedException>(() => Bot.Build<Model1>());
        }

        [Test]
        public void Build_WithAfterBindingHook_PropertyFromHookIsNotOverriden()
        {
            Bot.Define(x => new Model1 { Number = x.Integer.Any(-100, -10), Text = x.Strings.Any(10, 15) })
                .AfterPropertyBinding(x => x.Number = x.Text.Length);

            var model = Bot.Build<Model1>();

            Assert.That(model.Number, Is.GreaterThanOrEqualTo(10).And.EqualTo(model.Text.Length));
        }

        [Test]
        public void Build_WithAfterBindingHookAndThrows_ExceptionPassed()
        {
            Bot.Define(x => new Model1 { Number = x.Integer.Any(5, 100), Text = x.Strings.Any(10, 15) })
                .AfterPropertyBinding(x => throw new NotSupportedException("test"));

            Assert.Throws<BuildFailedException>(() => Bot.Build<Model1>());
        }
    }
}
