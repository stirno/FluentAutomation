using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAutomation.API;

namespace FluentAutomation.WatiN
{
    public class WatiNFluentTest : FluentTest
    {
        private ActionManager _actionManager = null;
        public override ActionManager I
        {
            get
            {
                if (_actionManager == null)
                {
                    _actionManager = new ActionManager(new AutomationProvider());
                }

                return _actionManager;
            }
        }
    }
}
