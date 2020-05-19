using FactoryBot.Tests.Models;
using NUnit.Framework;
using System.Linq;

namespace FactoryBot.Tests.Generators.Numbers
{
    [TestFixture]
    public class BooleanRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsBoolean()
        {
            Bot.Define(x => new AllTypesModel { Boolean = x.Boolean.Any() });

            var actual = Enumerable.Range(0, 5).Select(_ => Bot.Build<AllTypesModel>()).Select(x => x.Boolean).ToArray();

            Assert.That(actual, Does.Contain(false).And.Contain(false));
        }
    }
}
