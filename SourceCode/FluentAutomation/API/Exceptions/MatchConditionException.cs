// <copyright file="MatchConditionException.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using FluentAutomation.API.Enumerations;

namespace FluentAutomation.API.Exceptions
{
    public class MatchConditionException : AssertException
    {
        public MatchConditionException(string fieldSelector, MatchConditions failedCondition)
            : base("Match condition not met. Element [{0}] does not meet the specified condition: [{1}].", fieldSelector, failedCondition)
        {
        }

        public MatchConditionException(string message, params object[] formatParams)
            : base(message, formatParams)
        {
        }
    }
}
