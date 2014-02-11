using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using FluentAutomation.Exceptions;
using FluentAutomation.Interfaces;

namespace FluentAutomation
{
    public abstract class PageObject
    {
        private Uri _uri = null;

        public Uri Uri
        {
            get { return _uri; }
            set { _uri = value; }
        }

        public string Url
        {
            get { return _uri.ToString(); }
            set { _uri = new Uri(value); }
        }

        public Action At { get; set; }
    }

    public abstract class PageObject<T> : PageObject where T : PageObject
    {
        private FluentTest TestObject { get; set; }

        public PageObject(FluentTest test)
        {
            this.TestObject = test;
        }

        public INativeActionSyntaxProvider I
        {
            get { return this.TestObject.I; }
        }

        public T Go()
        {
            if (this.Uri == null)
                throw new FluentException("This page cannot be navigated to. Uri or Url is not set.");

            return this.Go(this.Uri);
        }

        public T Go(Uri uri)
        {
            return this.Go(uri.ToString());
        }

        public T Go(string url)
        {
            I.Open(url);
            if (this.At != null)
            {
                try
                {
                    this.At();
                }
                catch (FluentException ex)
                {
                    throw new FluentException("Unable to verify page navigation succeeded. See InnerException for details.", ex);
                }
            }

            return this as T;
        }

        public TNewPage Switch<TNewPage>() where TNewPage : PageObject
        {
            var newPage = (TNewPage)Activator.CreateInstance(typeof(TNewPage), new object[] { this.TestObject });
            if (newPage.At != null)
            {
                try
                {
                    newPage.At();
                }
                catch (FluentException ex)
                {
                    throw new FluentException("Unable to verify page navigation succeeded. See InnerException for details.", ex);
                }
            }

            return newPage;
        }
    }
}
