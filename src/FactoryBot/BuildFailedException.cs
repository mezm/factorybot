using System;

namespace FactoryBot
{
    /// <summary>
    /// Exception indicating that model was not able to be built
    /// </summary>
    public class BuildFailedException : Exception
    {
        /// <summary>
        /// Constructs <see cref="BuildFailedException"/>
        /// </summary>
        /// <param name="message">Exception message</param>
        public BuildFailedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructs <see cref="BuildFailedException"/>
        /// </summary>
        /// <param name="message">Exception message</param>
        /// <param name="innerException">Inner exception</param>
        public BuildFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}