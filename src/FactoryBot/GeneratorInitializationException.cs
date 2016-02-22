using System;

namespace FactoryBot
{
    public class GeneratorInitializationException : Exception
    {
        public GeneratorInitializationException(Type generatorType, Exception innerException)
            : base($"Exception during constructing type {generatorType}.", innerException)
        {
        }
    }
}