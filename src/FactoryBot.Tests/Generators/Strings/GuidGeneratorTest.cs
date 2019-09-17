using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class GuidGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Next_NoCondition_ValuesUnique()
        {
            AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Strings.Guid() });
        }
    }
}