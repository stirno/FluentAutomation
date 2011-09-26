// <copyright file="ExtensionMethods.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FluentAutomation.API
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Cleans up exception messages to provide better data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string PrettifyErrorValue(this string value)
        {
            if (value == string.Empty)
            {
                return "string.Empty";
            }
            else if (value == null)
            {
                return "NULL";
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Builds a string from expression used in exception messages.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns></returns>
        public static string ToExpressionString(this LambdaExpression expression)
        {
            StringBuilder sbExpression = new StringBuilder();
            foreach (var exprParam in expression.Parameters)
            {
                sbExpression.Append(exprParam);
                if (expression.Parameters.Last() != exprParam) sbExpression.Append(",");
            }
            sbExpression.Append(" => ");

            var exprBody = expression.Body.ToString();
            exprBody = exprBody.Substring(1, exprBody.Length - 2);
            exprBody = exprBody.Replace("OrElse", "||").Replace("AndAlso", "&&");

            sbExpression.Append(exprBody);

            return sbExpression.ToString();
        }
    }
}
