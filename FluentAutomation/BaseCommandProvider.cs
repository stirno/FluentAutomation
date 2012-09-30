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
                catch (Exceptions.FluentExpectFailedException ex)
                {
                    if (Settings.ScreenshotOnFailedExpect)
                    {
                        var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
                        ex.ScreenshotPath = System.IO.Path.Combine(Settings.ScreenshotPath, screenshotName);
                        this.TakeScreenshot(screenshotName);
                    }

                    throw;
                }
                catch (Exceptions.FluentException ex)
                {
                    if (Settings.ScreenshotOnFailedAction)
                    {
                        var screenshotName = string.Format(CultureInfo.CurrentCulture, "ActionFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
                        ex.ScreenshotPath = System.IO.Path.Combine(Settings.ScreenshotPath, screenshotName);
                        this.TakeScreenshot(screenshotName);
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
