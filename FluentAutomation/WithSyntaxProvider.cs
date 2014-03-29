using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAutomation
{
    public class WithSyntaxProvider
    {
        protected readonly IActionSyntaxProvider actionSyntaxProvider;
        protected readonly FluentSettings inlineSettings = null;

        public WithSyntaxProvider(IActionSyntaxProvider actionSyntaxProvider)
        {
            this.actionSyntaxProvider = actionSyntaxProvider;
            this.inlineSettings = FluentSettings.Current.Clone();
        }

        public WithSyntaxProvider WaitUntil(int seconds)
        {
            return this.WaitUntil(TimeSpan.FromSeconds(seconds));
        }

        public WithSyntaxProvider WaitUntil(TimeSpan timeout)
        {
            this.inlineSettings.WaitUntilTimeout = timeout;
            return this;
        }

        public WithSyntaxProvider WindowSize(int width, int height)
        {
            this.inlineSettings.WindowHeight = height;
            this.inlineSettings.WindowWidth = width;
            return this;
        }

        public IActionSyntaxProvider Then
        {
            get
            {
                var actionSyntaxProvider = (ActionSyntaxProvider)this.actionSyntaxProvider;
                actionSyntaxProvider.commandProvider.WithConfig(this.inlineSettings);
                return this.actionSyntaxProvider;
            }
        }
    }
}
