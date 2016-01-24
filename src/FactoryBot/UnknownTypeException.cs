using System;

namespace FactoryBot
{
    public class UnknownTypeException : Exception
    {
        public UnknownTypeException(Type unknownType)
            : base($"FactoryBot does know nothing about type {unknownType}. Call Bot.Define<{unknownType.Name}>(...) first.")
        {   
        }
    }
}