using FactoryBot.Tests.Models;
using NUnit.Framework;
using System;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_ErrorsTests
    {
        [TearDown]
        public void Terminate() => BotConfigurator.ForgetAll();

        [Test]
        public void BuildCustom_UseKeepWithOtherConstructor_ThrowsException()
        {
            BotConfigurator.Configure(x => new Model1(x.Integer.Any(10, 20), "the text"));

            Assert.That(() => Bot.BuildCustom(x => new Model1(x.Keep<int>())), Throws.InstanceOf<NotSupportedException>());
        }

        [Test]
        public void Build_NotDefinedType_ThrowsException() => Assert.That(() => Bot.Build<Model1>(), Throws.TypeOf<UnknownTypeException>());

        [Test]
        public void Define_UnknownNested_ThrowsException()
        {
            Assert.That(() => BotConfigurator.Configure(x => new Model3 { Number = 7, Nested = x.Use<Model1>() }),
                Throws.InstanceOf<UnknownTypeException>());
        }

        [Test]
        public void Define_CircularDependency_ThrowsException()
        {
            BotConfigurator.Configure(x => new ModelWithCircularDependency3());
            BotConfigurator.Configure(x => new ModelWithCircularDependency2(x.Use<ModelWithCircularDependency3>()));
            BotConfigurator.Configure(x => new ModelWithCircularDependency1 { Model = x.Use<ModelWithCircularDependency2>() });

            Assert.That(
                () => BotConfigurator.Configure(x => new ModelWithCircularDependency3 { Model = x.Use<ModelWithCircularDependency1>() }),
                Throws.InstanceOf<CircularDependencyDetectedException>());
        }

        [Test]
        public void DefineAuto_CircularDependency_ThrowsException()
        {
            Assert.That(() => BotConfigurator.ConfigureAuto<ModelWithCircularDependency3>(), Throws.InstanceOf<CircularDependencyDetectedException>());
        }
    }
}
