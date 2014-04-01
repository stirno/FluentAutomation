using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Tests.Pages
{
    public class TextPage : PageObject<TextPage>
    {
        public TextPage(FluentTest test)
            : base(test)
        {
            this.Url = "/Text";
        }

        public string TitleSelector = "#title";

        public string Paragraph1Selector = "#paragraph1";

        public string Paragraph2Selector = "#paragraph2";

        public string Link1Selector = "#link1";

        public string Link2Selector = "#link1";

        public string HoverColor = "rgb(255, 0, 0)";

        public string FocusColor = "rgb(0, 0, 255)";
    }
}