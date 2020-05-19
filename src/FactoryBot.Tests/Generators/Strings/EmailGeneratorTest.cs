using FactoryBot.Tests.Models;
using NUnit.Framework;
using System.Net.Mail;

namespace FactoryBot.Tests.Generators.Strings
{
    [TestFixture]
    public class EmailGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Email_NoCondition_ReturnsNewEmail() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { String = x.Network.Email() });

        [Test]
        public void Email_NoCondition_ReturnsValidEmail() => AssertGeneratorValue<string>(x => new AllTypesModel { String = x.Network.Email() }, x => new MailAddress(x));
    }
}
