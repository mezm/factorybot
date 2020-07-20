using FactoryBot.Tests.Models;
using NUnit.Framework;
using System;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_ErrorsTests
    {
        [TearDown]
        public void Terminate() => Bot.ForgetAll();

        [Test]
        public void BuildCustom_UseKeepWithOtherConstructor_ThrowsException()
        {
            Bot.Define(x => new Model1(x.Integer.Any(10, 20), "the text"));

            Assert.That(() => Bot.BuildCustom(x => new Model1(x.Keep<int>())), Throws.InstanceOf<NotSupportedException>());
        }

        [Test]
        public void Build_NotDefinedType_ThrowsException() => Assert.That(() => Bot.Build<Model1>(), Throws.TypeOf<UnknownTypeException>());

        [Test]
        public void Define_UnknownNested_ThrowsException()
        {
            Assert.That(() => Bot.Define(x => new Model3 { Number = 7, Nested = x.Use<Model1>() }),
                Throws.InstanceOf<UnknownTypeException>());
        }

        [Test]
        public void Define_CircularDependency_ThrowsException()
        {
            Bot.Define(x => new ModelWithCircularDependency3());
            Bot.Define(x => new ModelWithCircularDependency2(x.Use<ModelWithCircularDependency3>()));
            Bot.Define(x => new ModelWithCircularDependency1 { Model = x.Use<ModelWithCircularDependency2>() });

            Assert.That(
                () => Bot.Define(x => new ModelWithCircularDependency3 { Model = x.Use<ModelWithCircularDependency1>() }),
                Throws.InstanceOf<CircularDependencyDetectedException>());
        }

        [Test]
        public void DefineAuto_CircularDependency_ThrowsException()
        {
            Assert.That(() => Bot.DefineAuto<ModelWithCircularDependency3>(), Throws.InstanceOf<CircularDependencyDetectedException>());
        }

        [Test]
        public void Define_UnsupportedSytax_ThrowsException()
        {
            Assert.That(
                () => Bot.Define(x => new Model1 { Text = "PRE: " + x.Strings.Words() }), 
                Throws.InstanceOf<WrongSyntaxException>().And.Message.Contains("\"PRE: \" + x.Strings.Words()"));
        }
    }
}
