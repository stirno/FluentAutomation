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
        public FluentSettings Settings = FluentSettings.Current;

        private bool WaitOnAction(CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.Action:
                    return this.Settings.WaitOnAllActions;
                case CommandType.Assert:
                    return this.Settings.WaitOnAllAsserts;
                case CommandType.Expect:
                    return this.Settings.WaitOnAllExpects;
                case CommandType.Wait:
                default:
                    return false;
            }
        }

        public Tuple<FluentAssertFailedException, WindowState> PendingAssertFailedExceptionNotification { get; set; }
        public Tuple<FluentExpectFailedException, WindowState> PendingExpectFailedExceptionNotification { get; set; }

        private void FireAssertFailed()
        {
            var cachedAssertNotification = this.PendingAssertFailedExceptionNotification;
            this.PendingAssertFailedExceptionNotification = null;

            this.Settings.OnAssertFailed(cachedAssertNotification.Item1, cachedAssertNotification.Item2);
        }

        private void FireExpectFailed()
        {
            var cachedExpectFailed = this.PendingExpectFailedExceptionNotification;
            this.PendingExpectFailedExceptionNotification = null;

            this.Settings.OnExpectFailed(cachedExpectFailed.Item1, cachedExpectFailed.Item2);
        }

        public void Act(CommandType commandType, Action action)
        {
            bool originalWaitOnActions = this.Settings.WaitOnAllActions;
            try
            {
                if (this.WaitOnAction(commandType))
                {
                    this.WaitUntil(() => action(), this.Settings.WaitUntilTimeout);
                }
                else
                {
                    // If we've decided we don't wait on this type AND its an expect or assert, we want to disable
                    // waits on actions as well because the Expects use actions to fetch elements for verification
                    if (commandType == CommandType.Expect || commandType == CommandType.Assert)
                        this.Settings.WaitOnAllActions = false;

                    action();
                }
            }
            catch (Exceptions.FluentAssertFailedException ex)
            {
                if (this.Settings.ScreenshotOnFailedExpect)
                {
                    var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.ToFileTime());
                    ex.ScreenshotPath = System.IO.Path.Combine(this.Settings.ScreenshotPath, screenshotName);
                    this.TakeScreenshot(ex.ScreenshotPath);
                }

                // fire related event before throwing/breaking
                if (this.PendingAssertFailedExceptionNotification != null)
                    this.FireAssertFailed();

                throw;
            }
            catch (Exceptions.FluentException ex)
            {
                if (this.Settings.ScreenshotOnFailedAction)
                {
                    var screenshotName = string.Format(CultureInfo.CurrentCulture, "ActionFailed_{0}", DateTimeOffset.Now.ToFileTime());
                    ex.ScreenshotPath = System.IO.Path.Combine(this.Settings.ScreenshotPath, screenshotName);
                    this.TakeScreenshot(ex.ScreenshotPath);
                }

                if (ex.InnerException != null)
                {
                    if (ex.InnerException.GetType() == typeof(FluentExpectFailedException))
                    {
                        if (this.Settings.ScreenshotOnFailedExpect)
                        {
                            var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.ToFileTime());
                            ex.ScreenshotPath = System.IO.Path.Combine(this.Settings.ScreenshotPath, screenshotName);
                            this.TakeScreenshot(ex.ScreenshotPath);
                        }

                        if (this.PendingAssertFailedExceptionNotification != null)
                            this.FireAssertFailed();
                    }
                    else if (ex.InnerException.GetType() == typeof(FluentAssertFailedException))
                    {
                        if (this.Settings.ScreenshotOnFailedAssert)
                        {
                            var screenshotName = string.Format(CultureInfo.CurrentCulture, "AssertFailed_{0}", DateTimeOffset.Now.ToFileTime());
                            ex.ScreenshotPath = System.IO.Path.Combine(this.Settings.ScreenshotPath, screenshotName);
                            this.TakeScreenshot(ex.ScreenshotPath);
                        }

                        // fire related event before throwing/breaking
                        if (this.PendingAssertFailedExceptionNotification != null)
                            this.FireAssertFailed();
                    }
                }

                // fire related event before throwing/breaking
                if (this.PendingAssertFailedExceptionNotification != null)
                    this.FireAssertFailed();

                throw;
            }
            finally
            {
                // fire off event for expect failures
                if (this.PendingExpectFailedExceptionNotification != null)
                    this.FireExpectFailed();

                // restore WaitOnAllActions settings in all cases (protection for future cases where above catches dont rethrow)
                this.Settings.WaitOnAllActions = originalWaitOnActions;
            }
        }
        
        public void Wait()
        {
            this.Wait(this.Settings.WaitTimeout);
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.Act(CommandType.Wait, () => System.Threading.Thread.Sleep(timeSpan));
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            this.WaitUntil(conditionFunc, this.Settings.WaitUntilTimeout);
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc, TimeSpan timeout)
        {
            this.Act(CommandType.Wait, () =>
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

                        System.Threading.Thread.Sleep(this.Settings.WaitUntilInterval);
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
            });
        }

        public void WaitUntil(Expression<Action> conditionAction)
        {
            this.WaitUntil(conditionAction, this.Settings.WaitUntilTimeout);
        }

        public void WaitUntil(Expression<Action> conditionAction, TimeSpan timeout)
        {
            this.Act(CommandType.Wait, () =>
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

                    System.Threading.Thread.Sleep(this.Settings.WaitUntilInterval);
                }

                // If an exception was thrown the last loop, assume we hit the timeout
                if (threwException == true)
                {
                    throw new FluentException("Conditional wait passed the timeout [{0}ms] for expression [{1}]. See InnerException for details of the last FluentException thrown.", lastFluentException, timeout.TotalMilliseconds, conditionAction.ToExpressionString());
                }
            });
        }

        public abstract void TakeScreenshot(string screenshotName);
    }
}
