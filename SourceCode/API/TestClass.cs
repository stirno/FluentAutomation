using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation.API
{
    public class TestClass : IDisposable
    {
        private ActionManager _actionManager = null;
        public ActionManager I
        {
            get
            {
                if (_actionManager == null)
                {
                    _actionManager = new ActionManager();
                }

                return _actionManager;
            }
        }

        public void Dispose()
        {
            I.Finish();
        }
    }
}
