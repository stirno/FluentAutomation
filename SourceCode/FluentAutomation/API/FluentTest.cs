using System;

namespace FluentAutomation.API
{
    public abstract class FluentTest : IDisposable
    {
        public abstract ActionManager I { get; }

        public void Dispose()
        {
            try
            {
                I.Finish();
            }
            catch (Exception) { }
        }
    }
}
