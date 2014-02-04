using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentAutomation
{
    public abstract class BaseCommandProvider
    {
        protected bool ExecuteImmediate = true;
        protected List<Action> StoredActions = new List<Action>();

        public void Act(Action action, bool waitableAction = true)
        {
            if (this.ExecuteImmediate)
            {
                try
                {
                    if (waitableAction && Settings.WaitOnAllCommands)
                    {
                        this.WaitUntil(() => action(), Settings.DefaultWaitUntilTimeout);
                    }
                    else
                    {
                        action();
                    }
                }
                catch (Exceptions.FluentExpectFailedException ex)
                {
                    if (Settings.ScreenshotOnFailedExpect)
                    {
                        var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
                        ex.ScreenshotPath = System.IO.Path.Combine(Settings.ScreenshotPath, screenshotName);
                        this.TakeScreenshot(ex.ScreenshotPath);
                    }

                    throw;
                }
                catch (Exceptions.FluentException ex)
                {
                    if (Settings.ScreenshotOnFailedAction)
                    {
                        var screenshotName = string.Format(CultureInfo.CurrentCulture, "ActionFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
                        ex.ScreenshotPath = System.IO.Path.Combine(Settings.ScreenshotPath, screenshotName);
                        this.TakeScreenshot(ex.ScreenshotPath);
                    }

                    throw;
                }
            }
            else
            {
                if (waitableAction && Settings.WaitOnAllCommands)
                {
                    this.StoredActions.Add(new Action(() => this.WaitUntil(() => action(), Settings.DefaultWaitUntilTimeout)));
                }
                else
                {
                    this.StoredActions.Add(action);
                }
            }
        }

        public void ExecuteDeferredActions()
        {
            this.StoredActions.ForEach(action => action());
        }
        
        public void Wait(int seconds)
        {
            this.Wait(TimeSpan.FromSeconds(seconds));
        }

        public void Wait()
        {
            this.Wait(Settings.DefaultWaitTimeout);
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.Act(() => System.Threading.Thread.Sleep(timeSpan), false);
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            this.WaitUntil(conditionFunc, Settings.DefaultWaitUntilTimeout);
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            this.Act(() =>
            {
                DateTime dateTimeTimeout = DateTime.Now.Add(timeout);
                bool isFuncValid = false;
                var compiledFunc = conditionFunc.Compile();

                FluentException lastException = null;
                while (DateTime.Now < dateTimeTimeout)
                {
                    try
                    {
                        if (compiledFunc() == true)
                        {
                            isFuncValid = true;
                            break;
                        }

                        System.Threading.Thread.Sleep(Settings.DefaultWaitUntilThreadSleep);
                    }
                    catch (FluentException ex)
                    {
                        lastException = ex;
                    }
                    catch (Exception ex)
                    {
                        throw new FluentException("An unexpected exception was thrown inside WaitUntil(Func<bool>). See InnerException for details.", ex);
                    }
                }

                // If func is still not valid, assume we've hit the timeout.
                if (isFuncValid == false)
                {
                    throw new FluentException("Conditional wait passed the timeout [{0}ms] for expression [{1}]. See InnerException for details of the last FluentException thrown.", lastException, timeout.TotalMilliseconds, conditionFunc.ToExpressionString());
                }
            }, false);
        }

        public void WaitUntil(Expression<Action> conditionAction)
        {
            this.WaitUntil(conditionAction, Settings.DefaultWaitUntilTimeout);
        }

        public void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            this.Act(() =>
            {
                DateTime dateTimeTimeout = DateTime.Now.Add(timeout);
                bool threwException = false;
                var compiledAction = conditionAction.Compile();

                FluentException lastFluentException = null;
                while (DateTime.Now < dateTimeTimeout)
                {
                    try
                    {
                        threwException = false;
                        compiledAction();
                    }
                    catch (FluentException ex)
                    {
                        threwException = true;
                        lastFluentException = ex;
                    }
                    catch (Exception ex)
                    {
                        throw new FluentException("An unexpected exception was thrown inside WaitUntil(Action). See InnerException for details.", ex);
                    }

                    if (!threwException)
                    {
                        break;
                    }

                    System.Threading.Thread.Sleep(Settings.DefaultWaitUntilThreadSleep);
                }

                // If an exception was thrown the last loop, assume we hit the timeout
                if (threwException == true)
                {
                    throw new FluentException("Conditional wait passed the timeout [{0}ms] for expression [{1}]. See InnerException for details of the last FluentException thrown.", lastFluentException, timeout.TotalMilliseconds, conditionAction.ToExpressionString());
                }
            }, false);
        }

        public abstract void TakeScreenshot(string screenshotName);
    }
}
