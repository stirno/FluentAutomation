using System;

namespace FluentAutomation.API
{
    public abstract class FluentTest : IDisposable
    {
        public abstract ActionManager I { get; set; }

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
