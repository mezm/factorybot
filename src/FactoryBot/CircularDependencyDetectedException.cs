using System;

namespace FactoryBot
{
    /// <summary>
    /// Exception indicating that there is circular dependency in object type graph
    /// </summary>
    public class CircularDependencyDetectedException : Exception
    {
    }
}