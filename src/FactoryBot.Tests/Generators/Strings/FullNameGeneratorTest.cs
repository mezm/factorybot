using FactoryBot.Extensions;
using FactoryBot.Generators;
using FactoryBot.Generators.Strings;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class FullNameGeneratorTest
    {
        private readonly string _firstNameSource = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.FirstNames);
        private readonly string _lastNameSource = FileUtils.GetResourceContentWithoutLineBreaks(SourceNames.LastNames);

        [Test]
        public void GenerateFirstLastName()
        {
            var generator = new FullNameGenerator(FullNameFormat.FirstNameLastName);

            var name1 = (string) generator.Next();
            var name2 = (string) generator.Next();

            Assert.That(name1, Is.Not.Null);
            Assert.That(name2, Is.Not.Null.And.Not.EqualTo(name1));
            Assert.That(name1.Words(), Has.Length.EqualTo(2));
            Assert.That(name2.Words(), Has.Length.EqualTo(2));
            Assert.That(_firstNameSource, Does.Contain(name1.Words()[0]).And.Contain(name2.Words()[0]));
            Assert.That(_lastNameSource, Does.Contain(name1.Words()[1]).And.Contain(name2.Words()[1]));
        }

        [Test]
        public void GenerateLastFirstName()
        {
            var generator = new FullNameGenerator(FullNameFormat.LastNameFirstName);

            var name1 = (string)generator.Next();
            var name2 = (string)generator.Next();

            Assert.That(name1, Is.Not.Null);
            Assert.That(name2, Is.Not.Null.And.Not.EqualTo(name1));
            Assert.That(name1.Words(), Has.Length.EqualTo(2));
            Assert.That(name2.Words(), Has.Length.EqualTo(2));
            Assert.That(_lastNameSource, Does.Contain(name1.Words()[0]).And.Contain(name2.Words()[0]));
            Assert.That(_firstNameSource, Does.Contain(name1.Words()[1]).And.Contain(name2.Words()[1]));
        }
    }
}