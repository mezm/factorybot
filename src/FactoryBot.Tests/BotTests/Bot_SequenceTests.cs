using FactoryBot.Tests.Models;
using NUnit.Framework;
using System.Linq;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_SequenceTests
    {
        [TearDown]
        public void Terminate() => Bot.ForgetAll();

        [Test]
        public void BuildSequence()
        {
            Bot.Define(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

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
            Bot.Define(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

            Assert.That(() => Bot.BuildSequence<Model1>().Take(Bot.SequenceMaxLength).ToArray(), Throws.Nothing);
            Assert.That(() => Bot.BuildSequence<Model1>().Take(Bot.SequenceMaxLength + 1).ToArray(), Throws.InvalidOperationException);
        }

        [Test]
        public void BuildInfiniteSequence()
        {
            Bot.Define(x => new Model1(x.Integer.Any(10, 25)) { Text = x.Strings.Any() });

            var models = Bot.BuildSequence<Model1>(true).Take(Bot.SequenceMaxLength + 10).ToArray();

            Assert.That(models, Has.Length.EqualTo(Bot.SequenceMaxLength + 10));
        }
    }
}
