using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Collections
{
    [TestFixture]
    public class RandomFromListGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Generate()
        {
            var list = new[] { "a", "ab", "bc", "def" };
            Bot.Define(x => new AllTypesModel { String = x.Strings.RandomFromList(list) });

            var model = Bot.Build<AllTypesModel>();
            Assert.That(list, Does.Contain(model.String));
        }

        [Test]
        public void CreateWithEmptyList() => ExpectArgumentInitException(x => new AllTypesModel { Decimal = x.Decimal.RandomFromList(new decimal[0]) });
    }
}