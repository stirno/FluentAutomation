using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API
{
    public class FeatureNotImplementedException : AssertException
    {
        public FeatureNotImplementedException(string featureName) : base("Feature is not available. This is intentional. [{0}]", featureName)
        {
        }
    }
}
