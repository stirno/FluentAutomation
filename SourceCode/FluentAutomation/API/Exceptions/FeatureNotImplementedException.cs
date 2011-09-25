// <copyright file="FeatureNotImplementedException.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

namespace FluentAutomation.API.Exceptions
{
    public class FeatureNotImplementedException : AssertException
    {
        public FeatureNotImplementedException(string featureName) : base("Feature is not available. This is intentional. [{0}]", featureName)
        {
        }
    }
}
