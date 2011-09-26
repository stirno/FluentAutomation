// <copyright file="MatchConditionException.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API.Exceptions
{
    /// <summary>
    /// Exception thrown when conditions don't match
    /// </summary>
    public class MatchConditionException : AssertException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MatchConditionException"/> class.
        /// </summary>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="failedCondition">The failed condition.</param>
        public MatchConditionException(string fieldSelector, MatchConditions failedCondition)
            : base("Match condition not met. Element [{0}] does not meet the specified condition: [{1}].", fieldSelector, failedCondition)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MatchConditionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="formatParams">The format params.</param>
        public MatchConditionException(string message, params object[] formatParams)
            : base(message, formatParams)
        {
        }
    }
}
