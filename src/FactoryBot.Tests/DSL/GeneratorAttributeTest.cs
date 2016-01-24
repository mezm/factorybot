using System;

using FactoryBot.DSL;
using FactoryBot.Generators;

using NUnit.Framework;

namespace FactoryBot.Tests.DSL
{
    [TestFixture]
    public class GeneratorAttributeTest
    {
        [Test]
        public void CreateAttributeWithNonGeneratorClass()
        {
            Assert.That(() => new GeneratorAttribute(typeof(string)), Throws.ArgumentException);
        }

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

        [Test]
        public void CreateNonGenericGeneratorFromGenericMethod()
        {
            var attr = new GeneratorAttribute(typeof(TestGenerator));

            var generator = attr.CreateGenericGenerator(new[] { typeof(int) });

            Assert.That(generator, Is.Not.Null.And.InstanceOf<TestGenerator>());
        }

        [Test]
        public void CreateGenericGenerator()
        {
            var attr = new GeneratorAttribute(typeof(TestGenericGenerator<,>));

            dynamic generator = attr.CreateGenericGenerator(new[] { typeof(string), typeof(int) }, "test", 554);

            Assert.That(generator, Is.InstanceOf<TestGenericGenerator<string, int>>());
            Assert.That(generator.Value1, Is.EqualTo("test"));
            Assert.That(generator.Value2, Is.EqualTo(554)); 
        }

        [Test]
        public void CreateGenericGeneratorWithNoTypeArguments()
        {
            var attr = new GeneratorAttribute(typeof(TestGenericGenerator<,>));

            Assert.That(() => attr.CreateGenericGenerator(new[] { typeof(int) }, 554), Throws.ArgumentException);
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

        private class TestGenericGenerator<T1, T2> : IGenerator
        {
            public TestGenericGenerator(T1 value1, T2 value2)
            {
                Value1 = value1;
                Value2 = value2;
            }

            public T1 Value1 { get; }

            public T2 Value2 { get; }

            public object Next()
            {
                throw new NotSupportedException();
            }
        }
    }
}