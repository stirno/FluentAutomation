using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API
{
    internal static class WatinExtensionMethods
    {
        internal static void FireJavaScriptChange(this WatiN.Core.Element element)
        {
            // Fire change event directly in JavaScript -- adds unfortunate dependency on jQuery, fix this later
            element.DomContainer.Eval(String.Format("$({0}).change();", element.GetJavascriptElementReference()));
        }
    }
}
