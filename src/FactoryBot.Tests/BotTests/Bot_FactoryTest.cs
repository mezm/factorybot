using FactoryBot.Tests.Models;
using NUnit.Framework;
using System;
using System.Linq;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_FactoryTest
    {
        private static int LastestGeneratedValue = 0;

        [TearDown]
        public void Terminate() => BotConfigurator.ForgetAll();

        [Test]
        public void Build_FactoryReturnConstant_AllGeneratedValueAreEqual()
        {
            BotConfigurator.Configure(x => new Model1 { Text = x.Factory(() => "abc") });

            var models = Bot.BuildSequence<Model1>().Take(100);

            Assert.That(models.Select(x => x.Text), Is.All.EqualTo("abc"));
        }

        [Test]
        public void Build_FactoryReturnUniqueValue_AllGeneratedValueAreDifferent()
        {
            BotConfigurator.Configure(x => new Model1 { Number = x.Factory(IntegerFactory) });

            var models = Bot.BuildSequence<Model1>().Take(100);

            Assert.That(models.Select(x => x.Number), Is.Unique);
        }

        [Test]
        public void Build_FactoryThrowsException_ExceptionPassed()
        {
            BotConfigurator.Configure(x => new Model1 { Number = x.Factory(BrokenIntegerFactory) });

            Assert.Throws<BuildFailedException>(() => Bot.Build<Model1>());
        }

        private static int IntegerFactory() => LastestGeneratedValue++;

        private static int BrokenIntegerFactory() => throw new InvalidOperationException("test");
    }
}
