using System;
using System.Linq.Expressions;

namespace FactoryBot
{
    /// <summary>
    /// Exception indicating that expression rule contains not supported syntax
    /// </summary>
    public class WrongSyntaxException : Exception
    {
        /// <summary>
        /// Constructs <see cref="WrongSyntaxException"/>
        /// </summary>
        /// <param name="expr">Expressions with not supported syntax</param>
        /// <param name="innerException">Inner exception</param>
        public WrongSyntaxException(Expression expr, Exception innerException)
            : base("Not supported syntax: " + expr.ToString(), innerException)
        {

        }
    }
}