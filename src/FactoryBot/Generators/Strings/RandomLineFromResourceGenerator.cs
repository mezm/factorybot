using System.IO;

using FactoryBot.Utils;

namespace FactoryBot.Generators.Strings
{
    public abstract class RandomLineFromResourceGenerator : RandomLineFromStreamGenerator
    {
        private readonly string _resourceName;

        protected RandomLineFromResourceGenerator(string resourceName)
        {
            Check.NotNullOrWhiteSpace(resourceName, nameof(resourceName));

            _resourceName = resourceName;
        }

        protected override Stream OpenStream() => ResourceHelper.OpenStream(_resourceName);
    }
}