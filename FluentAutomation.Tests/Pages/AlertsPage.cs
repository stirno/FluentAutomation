using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.Tests.Pages
{
    public class AlertsPage : PageObject<AlertsPage>
    {
        public AlertsPage(FluentTest test)
            : base(test)
        {
            this.Url = "/Alerts";
        }

        public string TriggerAlertSelector = "#trigger-alert";

        public string TriggerConfirmSelector = "#trigger-confirm";

        public string TriggerPromptSelector = "#trigger-prompt";

        public string ResultSelector = "#result";
    }
}
