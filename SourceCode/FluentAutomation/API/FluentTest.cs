// <copyright file="FluentTest.cs" author="Brandon Stirnaman">
//     Copyright (c) 2011 Brandon Stirnaman, All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAutomation.API.Providers;

namespace FluentAutomation.API
{
    /// <summary>
    /// Fluent Test base class
    /// </summary>
    public abstract class FluentTest : IDisposable
    {
        private static readonly string ProviderNameAppSettingKey = "AutomationProvider";

        private static List<Type> _providers = null;
        public static List<Type> Providers
        {
            get
            {
                if (_providers == null)
                {
                    _providers = new List<Type>();

                    // load providers
                    foreach (string assemblyFileName in Directory.GetFiles(Path.GetFullPath(Environment.CurrentDirectory)))
                    {
                        var filename = new FileInfo(assemblyFileName).Name;
                        if (filename.StartsWith("FluentAutomation.") && filename.EndsWith(".dll"))
                        {
                            try
                            {
                                Assembly asm = Assembly.LoadFile(assemblyFileName);
                                foreach (Type asmType in asm.GetTypes())
                                {
                                    if (asmType.IsSubclassOf(typeof(FluentAutomation.API.Providers.AutomationProvider)))
                                    {
                                        _providers.Add(asmType);
                                    }
                                }
                            }
                            catch (Exception ex) { }
                        }
                    }
                }

                return _providers;
            }
        }

        private CommandManager _commandManager = null;
        public virtual CommandManager I
        {
            get
            {
                if (_commandManager == null)
                {
                    this.Setup();

                    Type selectedProvider = null;

                    if (Providers.Count > 0)
                    {
                        selectedProvider = Providers.First();

                        IEnumerable<Type> matchedProviders = null;
                        if (!string.IsNullOrEmpty(ProviderName))
                        {
                            matchedProviders = Providers.Where(s => s.FullName.Contains(ProviderName));
                        }
                        else
                        {
                            var providerName = ConfigurationManager.AppSettings[ProviderNameAppSettingKey];
                            if (providerName != null)
                            {
                                matchedProviders = Providers.Where(s => s.FullName.Contains(providerName));
                            }
                        }

                        if (matchedProviders != null)
                        {
                            if (matchedProviders.Count() == 0)
                            {
                                throw new InvalidOperationException("Specified automation provider unavailable.");
                            }
                            else
                            {
                                selectedProvider = matchedProviders.First();
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("No automation provider available.");
                    }

                    if (this.ProviderName == null) this.ProviderName = selectedProvider.FullName;
                    _commandManager = new CommandManager((AutomationProvider)Activator.CreateInstance(selectedProvider));
                }

                return _commandManager;
            }

            set
            {
                _commandManager = value;
            }
        }

        public bool ScreenshotOnAssertException { get; set; }
        public string ScreenshotPath { get; set; }
        public string ProviderName { get; set; }

        public virtual void Setup()
        {
            this.ScreenshotOnAssertException = false;
            this.ScreenshotPath = Environment.CurrentDirectory;
        }

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
