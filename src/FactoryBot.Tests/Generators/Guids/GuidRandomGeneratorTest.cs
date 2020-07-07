using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Guids
{
    [TestFixture]
    public class GuidRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsGuid() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Guid = x.Guid.Any() });
    }
}
