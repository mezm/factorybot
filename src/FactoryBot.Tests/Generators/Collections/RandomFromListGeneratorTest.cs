using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Collections
{
    [TestFixture]
    public class RandomFromListGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void RandomFromList_NonEmptyList_ReturnsValueFromList()
        {
            var list = new[] { "a", "ab", "bc", "def" };
            BotConfigurator.Configure(x => new AllTypesModel { String = x.Strings.RandomFromList(list) });

            var model = Bot.Build<AllTypesModel>();
            Assert.That(list, Does.Contain(model.String));
        }

        [Test]
        public void RandomFromList_EmptyList_ThrowsError() => ExpectArgumentInitException(x => new AllTypesModel { Decimal = x.Decimal.RandomFromList() });
    }
}