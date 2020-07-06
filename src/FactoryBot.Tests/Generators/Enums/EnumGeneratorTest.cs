using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Enums
{
    [TestFixture]
    public class EnumGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_NoCondition_ReturnsEnum() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { Enum = x.Enums.Any<EnumModel>() });
    }
}
