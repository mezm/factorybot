using System;

namespace FactoryBot
{
    /// <summary>
    /// Exception indicating that request to build model was not defined yet
    /// </summary>
    public class UnknownTypeException : Exception
    {
        /// <summary>
        /// Constructs <see cref="UnknownTypeException"/>
        /// </summary>
        /// <param name="unknownType">Type of model that was not defined</param>
        public UnknownTypeException(Type unknownType)
            : base($"FactoryBot does know nothing about type {unknownType}. Call Bot.Define<{unknownType.Name}>(...) first.")
        {   
        }
    }
}