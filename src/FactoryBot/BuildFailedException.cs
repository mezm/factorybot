using System;

namespace FactoryBot
{
    public class BuildFailedException : Exception
    {
        public BuildFailedException(string message) : base(message)
        {
        }

        public BuildFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}