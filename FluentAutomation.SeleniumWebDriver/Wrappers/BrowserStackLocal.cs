using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace FluentAutomation.Wrappers
{
    public class BrowserStackLocal : IDisposable
    {
        private const string MutexName = "{e8aa150b-3b92-44c8-a9d4-aecfb6c51416}";
        private static object _mutex = string.Empty;
        private static bool _disposed;
        private static BrowserStackLocal _instance;
        private Dictionary<string, Process> _processes;

        private BrowserStackLocal()
        {
            _processes = new Dictionary<string, Process>();
        }

        ~BrowserStackLocal()
        {
            Dispose(false);
        }

        public static BrowserStackLocal Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new BrowserStackLocal();
                        }
                    }
                }

                return _instance;
            }
        }

        /*-------------------------------------------------------------------*/

        public string BuildArguments(
        string browserStackKey,
        string browserStackLocalFolder,
        bool browserStackOnlyAutomate,
        bool browserStackForceLocal,
        bool browserStackUseProxy,
        string browserStackProxyHost,
        int? browserStackProxyPort,
        string browserStackProxyUser,
        string browserStackProxyPassword)
        {
            string arguments = "";

            if (!string.IsNullOrWhiteSpace(browserStackKey))
            {
                arguments += "-" + browserStackKey;
            }
            else
            {
               throw new ArgumentNullException("browserStackKey");
            }
            
            if (!string.IsNullOrWhiteSpace(browserStackLocalFolder))
            {
                arguments += " -f " + browserStackLocalFolder;
            }

            if (browserStackOnlyAutomate)
            {
                arguments += " -onlyAutomate";
            }

            if (browserStackForceLocal)
            {
                arguments += " -forcelocal";
            }

            if (browserStackUseProxy)
            {
                if (string.IsNullOrWhiteSpace(browserStackProxyHost))
                {
                    arguments += " -proxyHost " + browserStackProxyHost; 
                }

                if (browserStackProxyPort != null )
                {
                    arguments += " -proxyPort " + browserStackProxyPort;
                }
               
                if (!string.IsNullOrWhiteSpace(browserStackProxyUser))
                {
                    arguments += " -proxyUser " + browserStackProxyUser;
                }

                if (!string.IsNullOrWhiteSpace(browserStackProxyPassword))
                {
                    arguments += " -proxyPass " + browserStackProxyPassword; 
                }

            }

            return arguments;
        }

        public bool Start(string identifier, string arguments)
        {
            using (var mutex = new Mutex(false, MutexName))
            {
                mutex.WaitOne();

                try
                {
                    if (IsBrowserStackLocalProcessRunning(identifier))
                    {
                        Console.WriteLine("BrowserStackLocal ({0}) is already running!", identifier);
                        return false;
                    }

                    Console.WriteLine("Starting BrowserStackLocal ({0})!", identifier);
                    Process process;
                    if (_processes.TryGetValue(identifier, out process))
                    {
                        // Restart stored process
                        process.Start();
                    }
                    else
                    {
                        string targetExeFilename = ConvertToBrowserStackLocalTargetFilename(identifier);
                        string fullPathToExe = EmbeddedResources.UnpackFromAssembly(
                            "BrowserStackLocal.exe",
                            targetExeFilename,
                            Assembly.GetAssembly(typeof(SeleniumWebDriver)));

                        // Start a new process
                        var processStartInfo = GetProcessStartInfo(fullPathToExe, identifier, arguments);
                        process = Process.Start(processStartInfo);

                        if (process != null)
                        {
                            process.EnableRaisingEvents = true;
                            process.Exited += BrowserStackLocalProcessOnExited;
                        }
                    }

                    // Wait 2 second to allow BrowserStackLogic to startup and catch any error-shutdowns
                    Thread.Sleep(2000);

                    if (process != null)
                    {
                        process.Refresh();
                        if (!process.HasExited)
                        {
                            _processes[identifier] = process;
                            return true;
                        }
                    }
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    Console.WriteLine("Couldn't start the BrowserStackLocal process, perhaps the process is already running?");
                    throw;
                }
                catch (FileNotFoundException fileNotFoundException)
                {
                    Console.WriteLine("Couldn't find the required .exe file, or the file is already in use.");
                    throw;
                }
                catch (Win32Exception win32Exception)
                {
                    Console.WriteLine("A Win32 exception occured while trying to start the BrowserStackLocal process, let's panic!");
                    throw;
                }
                finally
                {
                    mutex.ReleaseMutex();
                }

                // If we end up here the process failed to start.
                return false;
            }
        }

        public bool Stop(string identifier)
        {
            using (var mutex = new Mutex(false, MutexName))
            {
                mutex.WaitOne();

                try
                {
                    if (!IsBrowserStackLocalProcessRunning(identifier)) return false;

                    Console.WriteLine("Stopping BrowserStackLocal ({0})!", identifier);

                    Process process = _processes[identifier];

                    process.Kill();
                    return process.HasExited;
                }
                catch (Win32Exception win32Exception)
                {
                    Console.WriteLine("A Win32 exception occured while trying to stop the BrowserStackLocal process, let's panic!");
                    throw;
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    Console.WriteLine("Couldn't stop the BrowserStackLocal process, the process is probably already stoped.");
                }
                finally
                {
                    mutex.ReleaseMutex();
                }

                // If we end up here the process failed to stop properly.
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose any managed objects
                    // ...
                }

                // Now disposed of any unmanaged objects
                if (_processes != null)
                {
                    string[] processIdentifiers = _processes.Keys.ToArray();
                    foreach (string identifier in processIdentifiers)
                    {
                        Stop(identifier);
                    }

                    _processes.Clear();
                    _processes = null;
                }

                _disposed = true;
            }
        }

        private ProcessStartInfo GetProcessStartInfo(string fullPathToExe, string identifier, string arguments)
        {
            if (fullPathToExe == null) throw new ArgumentNullException("fullPathToExe");
            
            // Compose process start info instance
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = !FluentSettings.Current.InDebugMode;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.FileName = fullPathToExe;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = arguments + " -localIdentifier " + identifier;

            return startInfo;
        }

        private void BrowserStackLocalProcessOnExited(object sender, EventArgs eventArgs)
        {
            var identifier = _processes.FirstOrDefault(x => x.Value == sender).Key;
            Console.WriteLine("BrowserStackLocal process ({0}) exited.", identifier);
        }

        private bool IsBrowserStackLocalProcessRunning(string identifier)
        {
            Process process;
            if (_processes != null && _processes.TryGetValue(identifier, out process))
            {
                return !process.HasExited;
            }

            return false;
        }

        private string ConvertToBrowserStackLocalTargetFilename(string identifier)
        {
            return string.Format("BrowserStackLocal_{0}.exe", string.Join(string.Empty, identifier.Split(Path.GetInvalidFileNameChars())));
        }
    }
}
