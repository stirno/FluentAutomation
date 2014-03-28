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

        private bool WaitOnAction(CommandType commandType)
        {
            switch (commandType)
            {
                case CommandType.Action:
                    return FluentSettings.Current.WaitOnAllActions;
                case CommandType.Assert:
                    return FluentSettings.Current.WaitOnAllAsserts;
                case CommandType.Expect:
                    return FluentSettings.Current.WaitOnAllExpects;
                case CommandType.Wait:
                default:
                    return false;
            }
        }

        public void Act(CommandType commandType, Action action)
        {
            bool originalWaitOnActions = FluentSettings.Current.WaitOnAllActions;
            try
            {
                if (this.WaitOnAction(commandType))
                {
                    this.WaitUntil(() => action(), FluentSettings.Current.DefaultWaitUntilTimeout);
                }
                else
                {
                    // If we've decided we don't wait on this type AND its an expect or assert, we want to disable
                    // waits on actions as well because the Expects use actions to fetch elements for verification
                    if (commandType == CommandType.Expect || commandType == CommandType.Assert)
                        FluentSettings.Current.WaitOnAllActions = false;

                    action();
                }
            }
            catch (Exceptions.FluentExpectFailedException ex)
            {
                if (FluentSettings.Current.ScreenshotOnFailedExpect)
                {
                    var screenshotName = string.Format(CultureInfo.CurrentCulture, "ExpectFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
                    ex.ScreenshotPath = System.IO.Path.Combine(FluentSettings.Current.ScreenshotPath, screenshotName);
                    this.TakeScreenshot(ex.ScreenshotPath);
                }

                throw;
            }
            catch (Exceptions.FluentException ex)
            {
                if (FluentSettings.Current.ScreenshotOnFailedAction)
                {
                    var screenshotName = string.Format(CultureInfo.CurrentCulture, "ActionFailed_{0}", DateTimeOffset.Now.Date.ToFileTime());
                    ex.ScreenshotPath = System.IO.Path.Combine(FluentSettings.Current.ScreenshotPath, screenshotName);
                    this.TakeScreenshot(ex.ScreenshotPath);
                }

                throw;
            }
            finally
            {
                // restore WaitOnAllActions settings in all cases (protection for future cases where above catches dont rethrow)
                FluentSettings.Current.WaitOnAllActions = originalWaitOnActions;
            }
        }
        
        public void Wait(int seconds)
        {
            this.Wait(TimeSpan.FromSeconds(seconds));
        }

        public void Wait()
        {
            this.Wait(FluentSettings.Current.DefaultWaitTimeout);
        }

        public void Wait(TimeSpan timeSpan)
        {
            this.Act(CommandType.Wait, () => System.Threading.Thread.Sleep(timeSpan));
        }

        public void WaitUntil(Expression<Func<bool>> conditionFunc)
        {
            this.WaitUntil(conditionFunc, FluentSettings.Current.DefaultWaitUntilTimeout);
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

                        System.Threading.Thread.Sleep(FluentSettings.Current.DefaultWaitUntilThreadSleep);
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
            this.WaitUntil(conditionAction, FluentSettings.Current.DefaultWaitUntilTimeout);
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

                    System.Threading.Thread.Sleep(FluentSettings.Current.DefaultWaitUntilThreadSleep);
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
