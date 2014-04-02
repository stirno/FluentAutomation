using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Tests.Pages
{
    public class SwitchPage : PageObject<SwitchPage>
    {
        public SwitchPage(FluentTest test)
            : base(test)
        {
            this.Url = "/Switch";
            this.At = () => I.Assert.Text("Switch Testbed").In("h2");
        }

        public string NewWindowSelector = "#new-window";

        public string IFrameSelector = "iframe";
    }
}
