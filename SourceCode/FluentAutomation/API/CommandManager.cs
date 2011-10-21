// <copyright file="ActionManager.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentAutomation.API.Enumerations;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
	/// <summary>
	/// The "I" in I.Click, the primary interaction class
	/// </summary>
	public class CommandManager
	{
		private int _bucketIndex = -1;
		private List<ActionBucket> _actionBuckets = new List<ActionBucket>();

		protected AutomationProvider Provider { get; set; }
		protected ExpectManager ExpectManager { get; set; }

		/// <summary>
		/// Gets or sets the action bucket.
		/// </summary>
		/// <value>
		/// The action bucket.
		/// </value>
		public ActionBucket CurrentActionBucket
		{
			get
			{
				return _actionBuckets[_bucketIndex > -1 ? _bucketIndex : 0];
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is record replay.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is record replay; otherwise, <c>false</c>.
		/// </value>
		public bool IsRecordReplay
		{
			get
			{
				return _bucketIndex > -1;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CommandManager"/> class.
		/// </summary>
		/// <param name="automationProvider">The automation provider.</param>
		public CommandManager(AutomationProvider automationProvider)
		{
			Provider = automationProvider;
			_actionBuckets.Add(new ActionBucket(this));
		}

		/// <summary>
		/// Records this instance.
		/// </summary>
		public void Record()
		{
			if (_bucketIndex > -1) _actionBuckets.Add(new ActionBucket(this));
			_bucketIndex++;
		}

		/// <summary>
		/// Plays the stored actions with the appropriate bucket.
		/// </summary>
		public void PlayWith(params BrowserType[] browserTypes)
		{
			var bucket = CurrentActionBucket;

			// cleanup bucket
			_actionBuckets.RemoveAt(_bucketIndex);
			_bucketIndex--;

			// execute bucket
			foreach (var browser in browserTypes)
			{
				Provider.SetBrowser(browser);
				foreach (var action in bucket)
				{
					action.Invoke();
				}
				Provider.Cleanup();
			}
		}

		/// <summary>
		/// Clicks the specified element selector.
		/// </summary>
		/// <param name="elementSelector">The element selector.</param>
		public void Click(string elementSelector)
		{
			Click(elementSelector, ClickMode.Default);
		}

		/// <summary>
		/// Clicks the specified element selector.
		/// </summary>
		/// <param name="elementSelector">The element selector.</param>
		/// <param name="clickMode">The click mode.</param>
		public void Click(string elementSelector, ClickMode clickMode)
		{
			Click(elementSelector, clickMode, MatchConditions.None);
		}

		/// <summary>
		/// Clicks the specified element selector.
		/// </summary>
		/// <param name="elementSelector">The element selector.</param>
		/// <param name="clickMode">The click mode.</param>
		/// <param name="conditions">The conditions.</param>
		public void Click(string elementSelector, ClickMode clickMode, MatchConditions conditions)
		{
			CurrentActionBucket.Add(() =>
			{
				var field = Provider.GetElement(elementSelector, conditions);
				field.Click(clickMode);
			});
		}

		/// <summary>
		/// Clicks the specified point (X, Y coordinates).
		/// </summary>
		/// <param name="point">The point.</param>
		public void Click(API.Point point)
		{
			CurrentActionBucket.Add(() =>
			{
				Provider.ClickPoint(point);
			});
		}

		/// <summary>
		/// Clicks a point relative to the position of the container from the top left. Useful when pages are centered or oddly positioned.
		/// </summary>
		/// <param name="containerSelector">The container selector.</param>
		/// <param name="point">The point.</param>
		public void Click(string containerSelector, API.Point point)
		{
			CurrentActionBucket.Add(() =>
			{
                Provider.ClickWithin(containerSelector, point);
			});
		}

		/// <summary>
		/// Drags the specified field selector.
		/// </summary>
		/// <param name="fieldSelector">The field selector.</param>
		/// <returns></returns>
		public FieldCommands.DragDrop Drag(string fieldSelector)
		{
			return new FieldCommands.DragDrop(Provider, this, fieldSelector);
		}

		/// <summary>
		/// Enters the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public FieldCommands.Text Enter(string value)
		{
			return new FieldCommands.Text(Provider, this, value);
		}

		/// <summary>
		/// Enters the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public FieldCommands.Text Enter(int value)
		{
			return Enter(value.ToString());
		}

		/// <summary>
		/// Gets the ExpectManager
		/// </summary>
		public ExpectManager Expect
		{
			get
			{
				if (ExpectManager == null)
				{
					ExpectManager = new ExpectManager(Provider, this);
				}

				return ExpectManager;
			}
		}

		/// <summary>
		/// Finishes a test, called within Dispose()
		/// </summary>
		public void Finish()
		{
			Provider.Cleanup();
		}

		/// <summary>
		/// Hovers over the specified element selector.
		/// </summary>
		/// <param name="elementSelector">The element selector.</param>
		public void Hover(string elementSelector)
		{
			Hover(elementSelector, MatchConditions.None);
		}

		/// <summary>
		/// Hovers over the specified element selector that matches the conditions.
		/// </summary>
		/// <param name="elementSelector">The element selector.</param>
		/// <param name="conditions">The conditions.</param>
		public void Hover(string elementSelector, MatchConditions conditions)
		{
			CurrentActionBucket.Add(() =>
			{
				var field = Provider.GetElement(elementSelector, conditions);
				field.Hover();
			});
		}

		/// <summary>
		/// Hovers over the specified point (X, Y coordinates).
		/// </summary>
		/// <param name="point">The point.</param>
		public void Hover(API.Point point)
		{
			CurrentActionBucket.Add(() =>
			{
				Provider.HoverPoint(point);
			});
		}

		/// <summary>
		/// Navigates the browser the specified direction.
		/// </summary>
		/// <param name="direction">The direction.</param>
		public void Navigate(NavigateDirection direction)
		{
			CurrentActionBucket.Add(() =>
			{
				Provider.Navigate(direction);
			});
		}

		/// <summary>
		/// Opens the specified page URI.
		/// </summary>
		/// <param name="pageUri">The page URI.</param>
		public void Open(Uri pageUri)
		{
			CurrentActionBucket.Add(() =>
			{
				Provider.Navigate(pageUri);
			});
		}

		/// <summary>
		/// Opens the specified page URL.
		/// </summary>
		/// <param name="pageUrl">The page URL.</param>
		public void Open(string pageUrl)
		{
			Open(new Uri(pageUrl, UriKind.Absolute));
		}

		/// <summary>
		/// Presses the specified keys using ActionManager.SendKeys()
		/// </summary>
		/// <param name="keys">The keys.</param>
		public void Press(string keys)
		{
			CurrentActionBucket.Add(() =>
			{
				CommandManager.SendKeys(keys);
			});
		}

		/// <summary>
		/// Takes a screenshot of the current page.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		public void TakeScreenshot(string fileName)
		{
			if (!string.IsNullOrEmpty(Provider.ScreenshotPath))
			{
				Provider.TakeScreenshot(System.IO.Path.Combine(Provider.ScreenshotPath, fileName));
			}
			else
			{
				Provider.TakeScreenshot(fileName);
			}
		}

		/// <summary>
		/// Types the specified value using ActionManager.SendString()
		/// </summary>
		/// <param name="value">The value.</param>
		public void Type(string value)
		{
			CurrentActionBucket.Add(() =>
			{
				CommandManager.SendString(value);
			});
		}

		/// <summary>
		/// Windows.Forms.SendKeys wrapper. See http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx for details.
		/// </summary>
		/// <param name="keys">The keys.</param>
		public static void SendKeys(string keys)
		{
			System.Windows.Forms.SendKeys.SendWait(keys);
		}

		/// <summary>
		/// Windows.Forms.SendKeys wrapper for typing entire strings.
		/// </summary>
		/// <param name="keys">The keys.</param>
		public static void SendString(string keys)
		{
			foreach (var chr in keys)
			{
				System.Windows.Forms.SendKeys.SendWait(chr.ToString());
                System.Threading.Thread.Sleep(20);
			}
		}

		/// <summary>
		/// Selects the specified options where the expression returns true.
		/// </summary>
		/// <param name="optionExpression">The option expression.</param>
		/// <returns></returns>
		public FieldCommands.Select Select(Expression<Func<string, bool>> optionExpression)
		{
			return Select(optionExpression, SelectMode.Text);
		}

		/// <summary>
		/// Selects the specified options where the expression returns true.
		/// </summary>
		/// <param name="optionExpression">The option expression.</param>
		/// <param name="selectMode">The select mode.</param>
		/// <returns></returns>
		public FieldCommands.Select Select(Expression<Func<string, bool>> optionExpression, SelectMode selectMode)
		{
			return new FieldCommands.Select(Provider, this, optionExpression, selectMode);
		}

		/// <summary>
		/// Selects the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public FieldCommands.Select Select(string value)
		{
			return Select(value, SelectMode.Value);
		}

		/// <summary>
		/// Selects the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="selectMode">The select mode.</param>
		/// <returns></returns>
		public FieldCommands.Select Select(string value, SelectMode selectMode)
		{
			return new FieldCommands.Select(Provider, this, new string[] { value }, selectMode);
		}

		/// <summary>
		/// Selects the specified values.
		/// </summary>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public FieldCommands.Select Select(params string[] values)
		{
			return Select(SelectMode.Value, values);
		}

		/// <summary>
		/// Selects the specified values.
		/// </summary>
		/// <param name="selectMode">The select mode.</param>
		/// <param name="values">The values.</param>
		/// <returns></returns>
		public FieldCommands.Select Select(SelectMode selectMode, params string[] values)
		{
			return new FieldCommands.Select(Provider, this, values, selectMode);
		}

		/// <summary>
		/// Selects the specified index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public FieldCommands.Select Select(int index)
		{
			return new FieldCommands.Select(Provider, this, new int[] { index }, SelectMode.Index);
		}

		/// <summary>
		/// Selects the specified indices.
		/// </summary>
		/// <param name="indices">The indices.</param>
		/// <returns></returns>
		public FieldCommands.Select Select(params int[] indices)
		{
			return new FieldCommands.Select(Provider, this, indices, SelectMode.Index);
		}

        /// <summary>
        /// Uploads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="offset">The offset.</param>
        public void Upload(string fileName, string fieldSelector, API.Point offset)
        {
            Upload(fileName, fieldSelector, offset, MatchConditions.Visible);
        }

		/// <summary>
		/// Uploads the specified file name with the field specified.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <param name="fieldSelector">The field selector.</param>
		public void Upload(string fileName, string fieldSelector)
		{
			Upload(fileName, fieldSelector, null, MatchConditions.Visible);
		}

        /// <summary>
        /// Uploads the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="fieldSelector">The field selector.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="conditions">The conditions.</param>
        protected void Upload(string fileName, string fieldSelector, API.Point offset, MatchConditions conditions)
        {
            CurrentActionBucket.Add(() =>
            {
                Provider.Upload(fileName, fieldSelector, offset, conditions);
            });
        }

		/// <summary>
		/// Uses the specified browser type.
		/// </summary>
		/// <param name="browserType">Type of the browser.</param>
		public void Use(BrowserType browserType)
		{
			Provider.SetBrowser(browserType);
		}

		/// <summary>
		/// Waits the specified number of seconds.
		/// </summary>
		/// <param name="seconds">The seconds.</param>
		public void Wait(int seconds)
		{
			Wait(TimeSpan.FromSeconds(seconds));
		}

		/// <summary>
		/// Waits the specified time.
		/// </summary>
		/// <param name="timeSpan">The time span.</param>
		public void Wait(TimeSpan timeSpan)
		{
			CurrentActionBucket.Add(() =>
			{
				Provider.Wait(timeSpan);
			});
		}
	}
}
