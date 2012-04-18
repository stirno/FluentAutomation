using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests
{
    public class SpamTests
    {
        [Fact]
        public void MultipleSeleniumInstancesRemotely()
        {
            var handles = new List<System.Threading.Tasks.Task>();
            for (var x = 0; x <= 3; x++)
            {
                var handle = System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    using (var testClass = new Remote())
                    {
                        testClass.TestSelect();
                    }
                });
                handles.Add(handle);
            }

            System.Threading.Tasks.Task.WaitAll(handles.ToArray());
        }
    }
}
