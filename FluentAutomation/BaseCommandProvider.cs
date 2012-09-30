using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public abstract class BaseCommandProvider
    {
        protected bool ExecuteImmediate = true;
        protected List<Action> StoredActions = new List<Action>();

        public void Act(Action action)
        {
            if (this.ExecuteImmediate)
            {
                try
                {
                    action();
                }
                catch (Exceptions.FluentExpectFailedException)
                {
                    if (Settings.ScreenshotOnFailedExpect)
                    {
                        this.TakeScreenshot(string.Format(CultureInfo.CurrentCulture, "ExpectFailed_", DateTimeOffset.Now.Date.ToShortTimeString()));
                    }

                    throw;
                }
                catch (Exceptions.FluentException)
                {
                    if (Settings.ScreenshotOnFailedExpect)
                    {
                        this.TakeScreenshot(string.Format(CultureInfo.CurrentCulture, "ActionFailed_", DateTimeOffset.Now.Date.ToShortTimeString()));
                    }

                    throw;
                }
            }
            else
            {
                this.StoredActions.Add(action);
            }
        }

        public void ExecuteDeferredActions()
        {
            this.StoredActions.ForEach(action => action());
        }

        public abstract void TakeScreenshot(string screenshotName);
    }
}
