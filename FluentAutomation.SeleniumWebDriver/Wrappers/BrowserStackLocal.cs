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

        private static bool _disposed;
        private static BrowserStackLocal _current;
        private Dictionary<string, Process> _processes;

        private BrowserStackLocal()
        {
            _processes = new Dictionary<string, Process>();
        }

        ~BrowserStackLocal()
        {
            Dispose(false);
        }

        public static BrowserStackLocal Current
        {
            get
            {
                if (_current != null)
                {
                    return _current;
                }

                using (var mutex = new Mutex(false, MutexName))
                {
                    if (_current == null)
                    {
                        _current = new BrowserStackLocal();
                    }
                }

                return _current;
            }
        }

        /*-------------------------------------------------------------------*/

        public bool Start(string browserStackKey, string identifier)
        {
            using (var mutex = new Mutex(false, MutexName))
            {
                if (IsBrowserStackLocalProcessRunning(identifier))
                {
                    Console.WriteLine("BrowserStackLocal ({0}) is already running!", identifier);
                    return false;
                }

                Console.WriteLine("Starting BrowserStackLocal ({0})!", identifier);
                try
                {
                    string targetExeFilename = ConvertToBrowserStackLocalTargetFilename(identifier);
                    string fullPathToExe = EmbeddedResources.UnpackFromAssembly("BrowserStackLocal.exe", targetExeFilename, Assembly.GetAssembly(typeof(SeleniumWebDriver)));

                    var processStartInfo = GetProcessStartInfo(fullPathToExe, browserStackKey, identifier);
                    Process process = Process.Start(processStartInfo);

                    if (process != null)
                    {
                        process.EnableRaisingEvents = true;
                        process.Exited += BrowserStackLocalProcessOnExited;
                        process.OutputDataReceived += BrowserStackLocalProcessOnOutputDataReceived;

                        // Wait 2 second to allow BrowserStackLogic to startup and catch any error-shutdowns
                        Thread.Sleep(2000);

                        process.Refresh();
                        if (!process.HasExited)
                        {
                            _processes.Add(identifier, process);
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

                // If we end up here the process failed to start.
                return false;
            }
        }

        public bool Stop(string identifier)
        {
            using (var mutex = new Mutex(false, MutexName))
            {
                if (!IsBrowserStackLocalProcessRunning(identifier)) return false;

                Console.WriteLine("Stopping BrowserStackLocal ({0})!", identifier);
                try
                {
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

        private ProcessStartInfo GetProcessStartInfo(string fullPathToExe, string browserStackKey, string identifier)
        {
            if (fullPathToExe == null) throw new ArgumentNullException("fullPathToExe");
            if (browserStackKey == null) throw new ArgumentNullException("browserStackKey");

            // Compose process start info instance
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = false;
            startInfo.FileName = fullPathToExe;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = string.Format("-localIdentifier {0} {1}", identifier, browserStackKey);

            return startInfo;
        }

        private void BrowserStackLocalProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            Console.WriteLine(dataReceivedEventArgs.Data ?? string.Empty);
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
