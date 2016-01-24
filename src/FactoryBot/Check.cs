using System;
using System.Diagnostics;

namespace FactoryBot
{
    [DebuggerStepThrough]
    internal static class Check
    {
        public static void NotNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}