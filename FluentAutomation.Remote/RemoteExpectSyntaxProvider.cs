using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public class RemoteExpectSyntaxProvider : RemoteBaseExpectSyntaxProvider
    {
        public RemoteExpectSyntaxProvider(IRemoteCommandProvider commandProvider, IRemoteExpectProvider expectProvider)
            : base(commandProvider, expectProvider)
        {
        }

        #region Count
        public class RemoteExpectCountSyntaxProvider : RemoteBaseExpectSyntaxProvider
        {
            private readonly int count = 0;

            public RemoteExpectCountSyntaxProvider(IRemoteCommandProvider commandProvider, IRemoteExpectProvider expectProvider, int count)
                : base(commandProvider, expectProvider)
            {
                this.count = count;
            }

            public void On(string selector)
            {
                this.expectProvider.Count(selector, this.count);
            }
        }
        #endregion

        #region CSS Class
        public RemoteExpectClassSyntaxProvider Class(string className)
        {
            return new RemoteExpectClassSyntaxProvider(this.commandProvider, this.expectProvider, className);
        }

        public class RemoteExpectClassSyntaxProvider : RemoteBaseExpectSyntaxProvider
        {
            private readonly string className = null;

            public RemoteExpectClassSyntaxProvider(IRemoteCommandProvider commandProvider, IRemoteExpectProvider expectProvider, string className)
                : base(commandProvider, expectProvider)
            {
                this.className = className;
            }

            public void On(string selector)
            {
                this.expectProvider.CssClass(selector, this.className);
            }
        }
        #endregion

        #region Text
        public RemoteExpectTextSyntaxProvider Text(string text)
        {
            return new RemoteExpectTextSyntaxProvider(this.commandProvider, this.expectProvider, text);
        }
        
        public class RemoteExpectTextSyntaxProvider : RemoteBaseExpectSyntaxProvider
        {
            private readonly string text = null;
            private readonly Expression<Func<string, bool>> matchFunc = null;

            public RemoteExpectTextSyntaxProvider(IRemoteCommandProvider commandProvider, IRemoteExpectProvider expectProvider, string text)
                : base(commandProvider, expectProvider)
            {
                this.text = text;
            }

            public void In(string selector)
            {
                if (!string.IsNullOrEmpty(this.text))
                {
                    this.expectProvider.Text(selector, this.text);
                }
            }
        }
        #endregion

        #region Url
        public void Url(string url)
        {
            Url(new Uri(url, UriKind.Absolute));
        }

        public void Url(Uri url)
        {
            this.expectProvider.Url(url.ToString());
        }
        #endregion

        #region Value
        public RemoteExpectValueSyntaxProvider Value(int value)
        {
            return this.Value(value.ToString());
        }

        public RemoteExpectValueSyntaxProvider Value(string value)
        {
            return new RemoteExpectValueSyntaxProvider(this.commandProvider, this.expectProvider, value);
        }
        
        public class RemoteExpectValueSyntaxProvider : RemoteBaseExpectSyntaxProvider
        {
            private readonly string value = null;

            public RemoteExpectValueSyntaxProvider(IRemoteCommandProvider commandProvider, IRemoteExpectProvider expectProvider, string value)
                : base(commandProvider, expectProvider)
            {
                this.value = value;
            }

            public void In(string selector)
            {
                if (!string.IsNullOrEmpty(this.value))
                {
                    this.expectProvider.Value(selector, this.value);
                }
            }
        }
        #endregion
    }

    public class RemoteBaseExpectSyntaxProvider
    {
        internal readonly IRemoteCommandProvider commandProvider = null;
        internal readonly IRemoteExpectProvider expectProvider = null;

        public RemoteBaseExpectSyntaxProvider(IRemoteCommandProvider commandProvider, IRemoteExpectProvider expectProvider)
        {
            this.commandProvider = commandProvider;
            this.expectProvider = expectProvider;
        }
    }
}
