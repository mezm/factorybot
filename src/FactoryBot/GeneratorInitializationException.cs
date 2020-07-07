using System;

namespace FactoryBot
{
    /// <summary>
    /// Exception indicating that generator was not able to be initialized
    /// </summary>
    public class GeneratorInitializationException : Exception
    {
        /// <summary>
        /// Constructs <see cref="GeneratorInitializationException"/>
        /// </summary>
        /// <param name="generatorType">Type of generator failed to be initialized</param>
        /// <param name="innerException">Exception</param>
        public GeneratorInitializationException(Type generatorType, Exception innerException)
            : base($"Exception during constructing type {generatorType}.", innerException)
        {
        }
    }
}