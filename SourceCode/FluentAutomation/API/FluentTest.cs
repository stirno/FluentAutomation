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
        public abstract ActionManager I { get; set; }

        public string ScreenshotPath { get; set; }

        public virtual void Setup()
        {
            this.ScreenshotPath = Environment.CurrentDirectory;
        }

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
