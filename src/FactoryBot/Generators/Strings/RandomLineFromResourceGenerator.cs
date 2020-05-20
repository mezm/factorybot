using System;
using System.IO;
using FactoryBot.Utils;

namespace FactoryBot.Generators.Strings
{
    public class RandomLineFromResourceGenerator : RandomLineFromStreamGenerator
    {
        private readonly string _resource;

        public RandomLineFromResourceGenerator(string resource)
        {
            if (string.IsNullOrWhiteSpace(resource))
            {
                throw new ArgumentException("String should not be null or empty.", nameof(resource));
            }

            _resource = resource;
        }

        protected override Stream OpenStream() => ResourceHelper.OpenStream(_resource);
    }
}