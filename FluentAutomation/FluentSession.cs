using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class FluentSession : IDisposable
    {
        internal static FluentSession Current { get; set; }

        public FluentSession()
        {
            if (FluentSession.Current != null)
            {
                this.Container = FluentSession.Current.Container;
                this.SyntaxProviderRegisterOptions = FluentSession.Current.SyntaxProviderRegisterOptions;
            }
            else
            {
                this.Container = new TinyIoC.TinyIoCContainer();
            }
            
            if (Settings.MinimizeAllWindowsOnTestStart) Win32Magic.MinimizeAllWindows();
        }

        internal TinyIoC.TinyIoCContainer.RegisterOptions SyntaxProviderRegisterOptions = null;

        internal bool HasBootstrappedTypes = false;

        public TinyIoC.TinyIoCContainer Container { get; private set; }

        public void RegisterSyntaxProvider<T>() where T : ISyntaxProvider
        {
            if (this.SyntaxProviderRegisterOptions == null)
                this.SyntaxProviderRegisterOptions = this.Container.Register(typeof(ISyntaxProvider), typeof(T));
        }

        public ISyntaxProvider GetSyntaxProvider()
        {
            return this.Container.Resolve<ISyntaxProvider>();
        }

        public void BootstrapTypeRegistration(Action<TinyIoC.TinyIoCContainer> containerAction)
        {
            if (FluentSession.Current == null)
            {
                containerAction(this.Container);
            }
            else if (FluentSession.Current.HasBootstrappedTypes == false)
            {
                containerAction(this.Container);
                FluentSession.Current.HasBootstrappedTypes = true;
            }
        }

        public static void EnableStickySession()
        {
            FluentSession.Current = new FluentSession();
            if (FluentSession.Current.SyntaxProviderRegisterOptions == null)
                FluentSession.Current.RegisterSyntaxProvider<ActionSyntaxProvider>();

            FluentSession.Current.SyntaxProviderRegisterOptions.AsSingleton();
        }

        public static void DisableStickySession()
        {
            FluentSession.Current.SyntaxProviderRegisterOptions.AsMultiInstance();
            FluentSession.Current = null;
        }

        public static void SetStickySession(FluentSession session)
        {
            FluentSession.Current = session;
            FluentSession.Current.SyntaxProviderRegisterOptions.AsSingleton();
        }

        public void Dispose()
        {
            try
            {
                this.GetSyntaxProvider().Dispose();
            }
            catch (Exception) { }
        }
    }
}
