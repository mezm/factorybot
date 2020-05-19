using FactoryBot.Tests.Models;
using NUnit.Framework;
using System;

namespace FactoryBot.Tests.Generators.Dates
{
    [TestFixture]
    public class TimeSpanRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_Random_ReturnsNewTimeSpan() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { TimeSpan = x.Time.Any() });

        [Test]
        public void Any_Range_ReturnsTimeSpanFromRange()
        {
            var from = new TimeSpan(1, 10, 0);
            var to = new TimeSpan(3, 44, 12);
            AssertGeneratorValue(x => new AllTypesModel { TimeSpan = x.Time.Any(from, to) }, Is.InRange(from, to));
        }

        [Test]
        public void Any_SingleValue_ReturnsTheValue()
        {
            var value = TimeSpan.FromSeconds(45);
            AssertGeneratorValue(
                x => new AllTypesModel { TimeSpan = x.Time.Any(value, value) },
                Is.EqualTo(value),
                Is.EqualTo(value));
        }

        [Test]
        public void Any_WrongRange_ThrowsError()
        {
            ExpectInitException<ArgumentOutOfRangeException>(
                x => new AllTypesModel { TimeSpan = x.Time.Any(new TimeSpan(3, 44, 12), new TimeSpan(1, 10, 0)) });
        }
    }
}
