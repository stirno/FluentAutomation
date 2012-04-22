using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace FluentAutomation.Tests.Remote
{
    class Node : IDisposable
    {
        private Lazy<InteractiveRemote> interactiveFactory = new Lazy<InteractiveRemote>(() => new InteractiveRemote());
        protected InteractiveRemote interactive { get { return interactiveFactory.Value; } }

        private Lazy<FormsRemote> formsFactory = new Lazy<FormsRemote>(() => new FormsRemote());
        protected FormsRemote forms { get { return formsFactory.Value; } }

        public Node()
        {
            FluentAutomation.Remote.Bootstrap();
        }

        [Fact]
        public void YUIDragDrop()
        {
            this.interactive.YUIDragDrop();
        }

        [Fact]
        public void Autocomplete_ExpectedResult()
        {
            this.forms.Autocomplete_ExpectedResult();
        }

        public void Dispose()
        {
            try
            {
                this.interactive.Dispose();
                this.forms.Dispose();
            }
            catch (Exception) { }
        }
    }
}
