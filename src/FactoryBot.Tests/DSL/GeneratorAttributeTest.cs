using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

using FactoryBot.DSL;
using FactoryBot.Generators;

using NUnit.Framework;

namespace FactoryBot.Tests.DSL
{
    [TestFixture]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
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

            dynamic generator = attr.CreateGenerator(GetDSLMethod(x => x.GetTestGenerator()), new Dictionary<string, object>());

            Assert.That(generator, Is.Not.Null.And.InstanceOf<TestGenerator>());
            Assert.That(generator.Length, Is.EqualTo(5));
            Assert.That(generator.Source, Is.EqualTo("The really long expression."));
        }

        [Test]
        public void CreateGeneratorWithParameters()
        {
            var attr = new GeneratorAttribute(typeof(TestGenerator));

            dynamic generator = attr.CreateGenerator(
                GetDSLMethod(x => x.GetTestGenerator(1, "a")),
                new Dictionary<string, object> { ["length"] = 15, ["source"] = "The short one." });

            Assert.That(generator, Is.Not.Null.And.InstanceOf<TestGenerator>());
            Assert.That(generator.Length, Is.EqualTo(15));
            Assert.That(generator.Source, Is.EqualTo("The short one."));
        }

        [Test]
        public void CreateGeneratorWithIncorrectParameters()
        {
            var attr = new GeneratorAttribute(typeof(TestGenerator));

            Assert.Throws<MissingMethodException>(
                () =>
                attr.CreateGenerator(
                    GetDSLMethod(x => x.GetTestGenerator(1, "adf")),
                    new Dictionary<string, object> { ["source1"] = 15, ["length"] = "The short one." }));
        }

        [Test]
        public void CreateNonGenericGeneratorFromGenericMethod()
        {
            var attr = new GeneratorAttribute(typeof(TestGenerator));

            var generator = attr.CreateGenerator(
                GetDSLMethod(x => x.GetTestGenericGenerator<int, string>()),
                new Dictionary<string, object>());

            Assert.That(generator, Is.Not.Null.And.InstanceOf<TestGenerator>());
        }

        [Test]
        public void CreateGenericGenerator()
        {
            var attr = new GeneratorAttribute(typeof(TestGenericGenerator<,>));

            dynamic generator = attr.CreateGenerator(
                GetDSLMethod(x => x.GetTestGenericGenerator("a", 33)),
                new Dictionary<string, object> { ["value1"] = "test", ["value2"] = 554 });

            Assert.That(generator, Is.InstanceOf<TestGenericGenerator<string, int>>());
            Assert.That(generator.Value1, Is.EqualTo("test"));
            Assert.That(generator.Value2, Is.EqualTo(554)); 
        }

        [Test]
        public void CreateGenericGeneratorWithNoTypeArguments()
        {
            var attr = new GeneratorAttribute(typeof(TestGenericGenerator<,>));

            Assert.That(
                () => attr.CreateGenerator(GetDSLMethod(x => x.GetTestGenerator()), new Dictionary<string, object>()),
                Throws.ArgumentException);
        }

        [Test]
        public void CreateGeneratorWithDefaultParameters()
        {
            var attr = new GeneratorAttribute(typeof(TestGeneratorWithDefaultParameters));

            dynamic generator = attr.CreateGenerator(
                GetDSLMethod(x => x.GetTestGeneratorWithDefaultParameters(0, "")),
                new Dictionary<string, object> { ["numberInteger"] = 10, ["text2"] = "abc" });

            Assert.That(generator, Is.InstanceOf<TestGeneratorWithDefaultParameters>());
            Assert.That(generator.NumberInteger, Is.EqualTo(10));
            Assert.That(generator.Text1, Is.EqualTo("a"));
            Assert.That(generator.Text2, Is.EqualTo("abc"));
        }

        private static MethodInfo GetDSLMethod(Expression<Func<TestDSL, object>> getMethodExpr)
        {
            return ((MethodCallExpression)getMethodExpr.Body).Method;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        [SuppressMessage("ReSharper", "UnusedTypeParameter")]
        private class TestDSL
        {
            public object GetTestGenerator() => default(object);

            public object GetTestGenerator(int length, string source) => default(object);

            public object GetTestGenericGenerator<T1, T2>() => default(object);

            public object GetTestGenericGenerator<T1, T2>(T1 value1, T2 value2) => default(object);

            public object GetTestGeneratorWithDefaultParameters(int numberInteger, string text2) => default(object);
        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
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

        private class TestGeneratorWithDefaultParameters : IGenerator
        {
            public TestGeneratorWithDefaultParameters(int numberInteger, string text1 = "a", string text2 = "b")
            {
                NumberInteger = numberInteger;
                Text1 = text1;
                Text2 = text2;
            }

            public int NumberInteger { get; }

            public string Text1 { get; }

            public string Text2 { get; }

            public object Next()
            {
                throw new NotSupportedException();
            }
        }
    }
}