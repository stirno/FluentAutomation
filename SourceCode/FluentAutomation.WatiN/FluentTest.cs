using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API;

namespace FluentAutomation.WatiN
{
    public class FluentTest : FluentAutomation.API.FluentTest
    {
        public AutomationProvider Provider = null;

        private ActionManager _actionManager = null;
        public override ActionManager I
        {
            get
            {
                if (_actionManager == null)
                {
                    this.Provider = new AutomationProvider();
                    _actionManager = new ActionManager(this.Provider);
                }

                return _actionManager;
            }

            set
            {
                _actionManager = value;
            }
        }
    }
}
