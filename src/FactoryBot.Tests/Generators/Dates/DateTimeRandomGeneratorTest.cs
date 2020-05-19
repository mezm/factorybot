using System;
using FactoryBot.Tests.Models;
using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Dates
{
    [TestFixture]
    public class DateTimeRandomGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void Any_Random_GeneratesAny() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { DateTime = x.Dates.Any() });

        [Test]
        public void Any_Range_GeneratesRandomFromRange()
        {
            var from = new DateTime(2015, 10, 4);
            var to = new DateTime(2025, 1, 1);
            AssertGeneratorValue(x => new AllTypesModel { DateTime = x.Dates.Any(from, to) }, Is.InRange(from, to));
        }

        [Test]
        public void Any_SingleValue_GeneratesRandomSingleValue()
        {
            var value = DateTime.Now;
            AssertGeneratorValue(
                x => new AllTypesModel { DateTime = x.Dates.Any(value, value) },
                Is.EqualTo(value),
                Is.EqualTo(value));
        }

        [Test]
        public void Any_WrongRange_ThrowsError()
        {
            ExpectInitException<ArgumentOutOfRangeException>(
                x => new AllTypesModel { DateTime = x.Dates.Any(new DateTime(2017, 1, 1), new DateTime(1992, 1, 10)) });
        }

        [Test]
        public void Before_NoCondition_ReturnsDateBefore()
        {
            var date = new DateTime(2010, 2, 5);
            AssertGeneratorValue(x => new AllTypesModel { DateTime = x.Dates.Before(date) }, Is.LessThan(date));
        }

        [Test]
        public void BeforeNow_NoCondition_ReturnsDateBefore()
        {
            var date = DateTime.UtcNow;
            AssertGeneratorValue(x => new AllTypesModel { DateTime = x.Dates.BeforeNow() }, Is.LessThan(date));
        }

        [Test]
        public void After_NoCondition_ReturnsDateBefore()
        {
            var date = new DateTime(2010, 2, 5);
            AssertGeneratorValue(x => new AllTypesModel { DateTime = x.Dates.After(date) }, Is.GreaterThan(date));
        }

        [Test]
        public void AfterNow_NoCondition_ReturnsDateBefore()
        {
            var date = DateTime.UtcNow;
            AssertGeneratorValue(x => new AllTypesModel { DateTime = x.Dates.AfterNow() }, Is.GreaterThan(date));
        }
    }
}