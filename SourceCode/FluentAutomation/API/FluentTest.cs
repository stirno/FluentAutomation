// <copyright file="FluentTest.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;

namespace FluentAutomation.API
{
    /// <summary>
    /// Fluent Test base class
    /// </summary>
    public abstract class FluentTest : IDisposable
    {
        private CommandManager _commandManager = null;
        public virtual CommandManager I
        {
            get
            {
                if (_commandManager == null)
                {
                    this.Setup();
                }

                return _commandManager;
            }

            set
            {
                _commandManager = value;
            }
        }

        public bool ScreenshotOnAssertException { get; set; }
        public string ScreenshotPath { get; set; }

        public virtual void Setup()
        {
            this.ScreenshotOnAssertException = false;
            this.ScreenshotPath = Environment.CurrentDirectory;
        }

		/// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                I.Finish();
                I = null;
            }
            catch (Exception) { }
        }
    }
}
