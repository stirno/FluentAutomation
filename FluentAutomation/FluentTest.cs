using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAutomation.Interfaces;
using FluentAutomation.Exceptions;

namespace FluentAutomation
{
    /// <summary>
    /// FluentTest - To be extended by tests targeting FluentAutomation. In the constructor, a user should call an appropriate bootstrap function from a FluentAutomation Provider.
    /// </summary>
    public class FluentTest : BaseFluentTest
    {
        public static bool IsMultiBrowserTest = false;

        private static object providerInstance = null;

        public static object ProviderInstance
        {
            get
            {
                if (IsMultiBrowserTest)
                    throw new FluentException("Accessing the Provider while using multiple browsers in a single test is unsupported.");

                return providerInstance;
            }

            set
            {
                providerInstance = value;
            }
        }

        public object Provider
        {
            get
            {
                if (FluentTest.ProviderInstance == null)
                    throw new FluentException("Provider is not available yet. Open a page with I.Open to create the provider.");

                return FluentTest.ProviderInstance;
            }
        }

        private FluentSession session = null;
        public FluentSession Session
        {
            get
            {
                if (session == null)
                {
                    session = new FluentSession();
                    session.RegisterSyntaxProvider<WbTstrActionSyntaxProvider>();
                }

                return session;
            }
        }

        /// <summary>
        /// Actions - Fluent's action functionality.
        /// </summary>
        public IActionSyntaxProvider I
        {
            get
            {
                var provider = SyntaxProvider as IActionSyntaxProvider;
                if (provider == null || provider.IsDisposed())
                {
                    this.Session.BootstrapTypeRegistration(FluentSettings.Current.ContainerRegistration);
                    SyntaxProvider = this.Session.GetSyntaxProvider();
                }

                var actionSyntaxProvider = (WbTstrActionSyntaxProvider)SyntaxProvider;
                actionSyntaxProvider.WithConfig(FluentSettings.Current);

                return SyntaxProvider as IActionSyntaxProvider;
            }
        }

        public FluentConfig Config
        {
            get
            {
                return FluentConfig.Current;
            }
        }

        public WithSyntaxProvider With
        {
            get
            {
                return new WithSyntaxProvider(I);
            }
        }
    }

    public class FluentTest<T> : FluentTest where T : class
    {
        public new T Provider
        {
            get
            {
                if (FluentTest.ProviderInstance == null)
                    throw new FluentException("Provider is not available yet. Open a page with I.Open to create the provider.");

                return FluentTest.ProviderInstance as T;
            }
        }
    }
}