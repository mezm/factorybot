using System;

namespace FactoryBot.DSL
{
    /// <summary>
    /// Class helper requires to eliminate nullable warnings in DSL
    /// </summary>
    internal static class Returns
    {
        public static T Type<T>() => throw new NotSupportedException();
    }
}
