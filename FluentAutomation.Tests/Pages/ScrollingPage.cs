using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Tests.Pages
{
    public class ScrollingPage : PageObject<ScrollingPage>
    {
        public ScrollingPage(FluentTest test)
            : base(test)
        {
            this.Url = "/Scrolling";
        }

        public string TextSelector = "#bigtext";

        public string BodySelector = "#big";

        public string TopLeftSelector = "#topleft > button";

        public string TopRightSelector = "#topright > button";

        public string BottomLeftSelector = "#bottomleft > button";

        public string BottomRightSelector = "#bottomright > button";

        public string HoverColor = "rgb(255, 0, 0)";

        public string FocusColor = "rgb(0, 0, 255)";
    }
}