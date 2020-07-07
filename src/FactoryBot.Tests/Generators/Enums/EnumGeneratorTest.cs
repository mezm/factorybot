using FactoryBot.Tests.Models;
using NUnit.Framework;
using System.Linq;

namespace FactoryBot.Tests.Generators.Enums
{
    [TestFixture]
    public class EnumGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsEnum()
        {
            BotConfigurator.Configure(x => new AllTypesModel { Enum = x.Enums.Any<EnumModel>() });

            var actual = Enumerable.Range(0, 10).Select(_ => Bot.Build<AllTypesModel>()).Select(x => x.Enum).ToArray();

            Assert.That(actual.Distinct().ToList(), Has.Count.GreaterThan(2));
        }
    }
}
