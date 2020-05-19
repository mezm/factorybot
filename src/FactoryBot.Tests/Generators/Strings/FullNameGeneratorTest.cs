using FactoryBot.Extensions;
using FactoryBot.Generators;
using FactoryBot.Generators.Strings;
using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class FullNameGeneratorTest : GeneratorTestKit
    {
        private readonly string _firstNameSource = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.FIRST_NAMES);
        private readonly string _lastNameSource = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.LAST_NAMES);

        [Test]
        public void FullName_NoCondition_ReturnsNewName() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Names.FullName() });

        [Test]
        public void FullName_FirstNameFirst_ReturnsName()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Names.FullName() },
                x =>
                    {
                        Assert.That(x.Words(), Has.Length.EqualTo(2));
                        Assert.That(_firstNameSource, Does.Contain(x.Words()[0]));
                        Assert.That(_lastNameSource, Does.Contain(x.Words()[1]));
                    });
        }

        [Test]
        public void FullName_LastNameFirst_ReturnsName()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Names.FullName(FullNameFormat.LastNameFirstName) },
                x =>
                    {
                        Assert.That(x.Words(), Has.Length.EqualTo(2));
                        Assert.That(_firstNameSource, Does.Contain(x.Words()[1]));
                        Assert.That(_lastNameSource, Does.Contain(x.Words()[0]));
                    });
        }
    }
}