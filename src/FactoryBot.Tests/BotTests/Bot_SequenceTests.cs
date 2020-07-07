using FactoryBot.Tests.Models;
using NUnit.Framework;
using System.Linq;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_SequenceTests
    {
        [TearDown]
        public void Terminate() => BotConfigurator.ForgetAll();

        [Test]
        public void BuildSequence_NoCondition_ReturnsSequence()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

            foreach (var model in Bot.BuildSequence<Model1>().Take(5))
            {
                Assert.That(model, Is.Not.Null);
                Assert.That(model.Number, Is.GreaterThanOrEqualTo(10).And.LessThanOrEqualTo(25));
                Assert.That(model.Text, Is.Not.Null);
            }
        }

        [Test]
        public void BuildSequence_FiniteSequenceTakeLimit_ReturnsSequence()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

            Assert.That(() => Bot.BuildSequence<Model1>().Take(Bot.SEQUENCE_MAX_LENGTH).ToArray(), Throws.Nothing);
        }

        [Test]
        public void BuildSequence_FiniteSequenceTakeLessThenLimit_ReturnsSequence()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

            Assert.That(() => Bot.BuildSequence<Model1>().Take(Bot.SEQUENCE_MAX_LENGTH - 1).ToArray(), Throws.Nothing);
        }

        [Test]
        public void BuildSequence_FiniteSequenceTakeMoreThenLimit_ThrowsError()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

            Assert.That(() => Bot.BuildSequence<Model1>().Take(Bot.SEQUENCE_MAX_LENGTH + 1).ToArray(), Throws.InvalidOperationException);
        }

        [Test]
        public void BuildSequence_InfiniteSequence_ReturnAnyLength()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

            var models = Bot.BuildSequence<Model1>(true).Take(Bot.SEQUENCE_MAX_LENGTH + 10).ToArray();

            Assert.That(models, Has.Length.EqualTo(Bot.SEQUENCE_MAX_LENGTH + 10));
        }
    }
}
