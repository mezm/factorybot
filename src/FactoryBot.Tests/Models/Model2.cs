using System;

namespace FactoryBot.Tests.Models
{
    public class Model2
    {
        public Model2(int number)
        {
            Number = number;
        }

        public int Number { get; }

        public DateTime Date { get; set; }
    }
}