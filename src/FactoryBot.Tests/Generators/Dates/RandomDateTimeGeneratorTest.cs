using System;

using FactoryBot.Tests.Models;

using NUnit.Framework;

namespace FactoryBot.Tests.Generators.Dates
{
    [TestFixture]
    public class RandomDateTimeGeneratorTest : GeneratorTestKit
    {
        [Test]
        public void GenerateRandom() => AssertGeneratorValuesAreNotTheSame(x => new AllTypesModel { DateTime = x.Dates.Any() });

        [Test]
        public void GenerateRandomFromRange()
        {
            var from = new DateTime(2015, 10, 4);
            var to = new DateTime(2025, 1, 1);
            AssertGeneratorValue(x => new AllTypesModel { DateTime = x.Dates.Any(from, to) }, Is.InRange(from, to));
        }

        [Test]
        public void GenerateRandomSingleValue()
        {
            var value = DateTime.Now;
            AssertGeneratorValue(
                x => new AllTypesModel { DateTime = x.Dates.Any(value, value) },
                Is.EqualTo(value),
                Is.EqualTo(value));
        }

        [Test]
        public void GenerateRandomWrongRange()
        {
            ExpectInitException<ArgumentOutOfRangeException>(
                x => new AllTypesModel { DateTime = x.Dates.Any(new DateTime(2017, 1, 1), new DateTime(1992, 1, 10)) });
        }
    }
}