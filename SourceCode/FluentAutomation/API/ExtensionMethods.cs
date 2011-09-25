// <copyright file="ExtensionMethods.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FluentAutomation.API
{
    public static class ExtensionMethods
    {
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
