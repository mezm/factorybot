using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class GuidGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Guid_NoCondition_ReturnsUniqueValues() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Strings.Guid() });
    }
}