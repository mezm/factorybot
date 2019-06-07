using System;

namespace FactoryBot.Generators
{
    /// <summary>
    /// Special marker generator which shows that original constructor generator should be choosen during custom construction.
    /// </summary>
    internal class KeepGenerator : IGenerator
    {
        public object Next() => throw new NotSupportedException();
    }
}