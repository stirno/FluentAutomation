using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Tests.Pages
{
    public class InputsPage : PageObject<InputsPage>
    {
        public InputsPage(FluentTest test)
            : base(test)
        {
            this.Url = "/Inputs";
        }

        public string TextControlSelector = "#text-control";

        public string TextareaControlSelector = "#textarea-control";

        public string SelectControlSelector = "#select-control";

        public string MultiSelectControlSelector = "#multi-select-control";

        public string ButtonControlSelector = "#button-control";

        public string InputButtonControlSelector = "#input-button-control";

        public string TextChangedTextSelector = "#text-control-changed";

        public string ButtonClickedTextSelector = "#button-clicked-text";

        public string FormGroupDiv = "div[class='form-group']";

        public string HoverColor = "rgb(255, 0, 0)";

        public string FocusColor = "rgb(0, 0, 255)";
    }
}
