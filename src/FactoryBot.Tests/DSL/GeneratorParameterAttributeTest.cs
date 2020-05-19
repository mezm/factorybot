using FactoryBot.DSL.Attributes;
using NUnit.Framework;
using System;
using System.Reflection;

namespace FactoryBot.Tests.DSL
{
    [TestFixture]
    public class GeneratorParameterAttributeTest
    {
        [Test]
        public void GetParameterValue_ValueSet_ReturnsValue()
        {
            var attr = new GeneratorParameterAttribute("test") { Value = "test value" };

            var actual = attr.GetParameterValue(GetDSLMethod());

            Assert.That(actual, Is.EqualTo("test value"));
        }

        [Test]
        public void GetParameterValue_FactorySet_ReturnsValueFromFactory()
        {
            var attr = new GeneratorParameterAttribute("test") { Factory = "IntFactory" };

            var actual = attr.GetParameterValue(GetDSLMethod());

            Assert.That(actual, Is.EqualTo(47));
        }

        [Test]
        public void GetParameterValue_NothingSet_ReturnsNull()
        {
            var attr = new GeneratorParameterAttribute("test");

            var actual = attr.GetParameterValue(GetDSLMethod());

            Assert.That(actual, Is.Null);
        }

        [Test]
        public void GetParameterValue_MissingFactorySet_ThrowsError()
        {
            var attr = new GeneratorParameterAttribute("test") { Factory = "DoubleFactory" };

            Assert.Throws<MissingMethodException>(() => attr.GetParameterValue(GetDSLMethod()));
        }

        [Test]
        public void GetParameterValue_InstanceFactorySet_ThrowsError()
        {
            var attr = new GeneratorParameterAttribute("test") { Factory = "InstanceFactory" };

            Assert.Throws<MissingMethodException>(() => attr.GetParameterValue(GetDSLMethod()));
        }

        [Test]
        public void GetParameterValue_NonParameterlessFactorySet_ThrowsError()
        {
            var attr = new GeneratorParameterAttribute("test") { Factory = "FactoryWithParam" };

            Assert.Throws<MissingMethodException>(() => attr.GetParameterValue(GetDSLMethod()));
        }

        private static MethodInfo GetDSLMethod() => typeof(TestDSL).GetMethod(nameof(TestDSL.GetTestGenerator));

        private class TestDSL
        {
            public object GetTestGenerator() => default;

#pragma warning disable IDE0051 // Remove unused private members
            private int InstanceFactory() => 25;

            private static int IntFactory() => 47;

            private static int FactoryWithParam(int value) => value;
#pragma warning restore IDE0051 // Remove unused private members
        }
    }
}
