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
        private readonly string _firstNameSource = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.FirstNames);
        private readonly string _lastNameSource = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.LastNames);

        [Test]
        public void AlwaysNewName() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Strings.FullName() });

        [Test]
        public void GenerateFirstLastName()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.FullName() },
                x =>
                    {
                        Assert.That(x.Words(), Has.Length.EqualTo(2));
                        Assert.That(_firstNameSource, Does.Contain(x.Words()[0]));
                        Assert.That(_lastNameSource, Does.Contain(x.Words()[1]));
                    });
        }

        [Test]
        public void GenerateLastFirstName()
        {
            AssertGeneratorValue<string>(
                x => new AllTypesModel { String = x.Strings.FullName(FullNameFormat.LastNameFirstName) },
                x =>
                    {
                        Assert.That(x.Words(), Has.Length.EqualTo(2));
                        Assert.That(_firstNameSource, Does.Contain(x.Words()[1]));
                        Assert.That(_lastNameSource, Does.Contain(x.Words()[0]));
                    });
        }
    }
}