// <copyright file="FeatureNotImplementedException.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

namespace FluentAutomation.API.Exceptions
{
    /// <summary>
    /// Feature Not Implemented
    /// </summary>
    public class FeatureNotImplementedException : AssertException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureNotImplementedException"/> class.
        /// </summary>
        /// <param name="featureName">Name of the feature.</param>
        public FeatureNotImplementedException(string featureName) : base("Feature is not available. This is intentional. [{0}]", featureName)
        {
        }
    }
}
