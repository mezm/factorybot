﻿using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.BotTests
{
    [TestFixture]
    public class Bot_ErrorsTests
    {
        [TearDown]
        public void Terminate() => Bot.ForgetAll();

        [Test]
        public void BuildCustom_ConstructorModifierArgumentsMismatch_ShouldThrowError()
        {
            Bot.Define(x => new Model1(x.Integer.Any(10, 20), "the text"));

            Assert.That(() => Bot.BuildCustom(x => new Model1(5)), Throws.InvalidOperationException);
        }

        [Test]
        public void Build_NotDefinedType_ShouldThrowError() => Assert.That(() => Bot.Build<Model1>(), Throws.TypeOf<UnknownTypeException>());

        [Test]
        public void Define_ConfigurationWithUnknownNested_ShouldThrowError()
        {
            Assert.That(() => Bot.Define(x => new Model3 { Number = 7, Nested = x.Use<Model1>() }),
                Throws.InstanceOf<UnknownTypeException>());
        }

        [Test]
        public void Define_CircularDependency_ShouldThrowError()
        {
            Bot.Define(x => new ModelWithCircularDependency3());
            Bot.Define(x => new ModelWithCircularDependency2(x.Use<ModelWithCircularDependency3>()));
            Bot.Define(x => new ModelWithCircularDependency1 { Model = x.Use<ModelWithCircularDependency2>() });

            Assert.That(
                () => Bot.Define(x => new ModelWithCircularDependency3 { Model = x.Use<ModelWithCircularDependency1>() }),
                Throws.InstanceOf<CircularDependencyDetectedException>());
        }

        [Test]
        public void DefineAuto_CircularDependency_ShouldThrowError()
        {
            Assert.That(
                () => Bot.DefineAuto<ModelWithCircularDependency3>(),
                Throws.InstanceOf<CircularDependencyDetectedException>());
        }
    }
}
