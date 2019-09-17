using System.IO;
using FactoryBot.Utils;

namespace FactoryBot.Generators.Strings
{
    public class RandomLineFromResourceGenerator : RandomLineFromStreamGenerator
    {
        private readonly string _resource;

        public RandomLineFromResourceGenerator(string resource)
        {
            Check.NotNullOrWhiteSpace(resource, nameof(resource));

            _resource = resource;
        }

        protected override Stream OpenStream() => ResourceHelper.OpenStream(_resource);
    }
}