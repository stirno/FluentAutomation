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
        public abstract CommandManager I { get; set; }

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
