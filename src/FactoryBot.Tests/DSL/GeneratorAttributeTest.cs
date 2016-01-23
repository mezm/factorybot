using System;

using FactoryBot.DSL;
using FactoryBot.Generators;
using FactoryBot.Generators.Numbers;

using NUnit.Framework;

namespace FactoryBot.Tests.DSL
{
    [TestFixture]
    public class GeneratorAttributeTest
    {
        [Test]
        public void CreateGeneratorWithoutParameters()
        {
            var attr = new GeneratorAttribute(typeof(TestGenerator));

            dynamic generator = attr.CreateGenerator();

            Assert.That(generator, Is.Not.Null.And.InstanceOf<TestGenerator>());
            Assert.That(generator.Length, Is.EqualTo(5));
            Assert.That(generator.Source, Is.EqualTo("The really long expression."));
        }

        [Test]
        public void CreateGeneratorWithParameters()
        {
            var attr = new GeneratorAttribute(typeof(TestGenerator));

            dynamic generator = attr.CreateGenerator(15, "The short one.");

            Assert.That(generator, Is.Not.Null.And.InstanceOf<TestGenerator>());
            Assert.That(generator.Length, Is.EqualTo(15));
            Assert.That(generator.Source, Is.EqualTo("The short one."));
        }

        [Test]
        public void CreateGeneratorWithIncorrectParameters()
        {
            var attr = new GeneratorAttribute(typeof(TestGenerator));

            Assert.Throws<MissingMethodException>(() => attr.CreateGenerator("The short one.", 15));
        }

        private class TestGenerator : IGenerator
        {
            public int Length { get; }

            public string Source { get; }

            public TestGenerator() : this(5, "The really long expression.")
            {
            }

            public TestGenerator(int length, string source)
            {
                Length = length;
                Source = source;
            }

            public object Next()
            {
                throw new NotSupportedException();
            }
        }
    }
}