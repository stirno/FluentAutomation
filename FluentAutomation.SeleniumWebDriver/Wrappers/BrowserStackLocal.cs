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

        private static BrowserStackLocal _instance;
        private Process _browserStackLocalProcess;
        private bool _browserStackLocalIsRunning;

        private BrowserStackLocal()
        {
            // Private but empty
        }

        public static BrowserStackLocal Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                using (var mutex = new Mutex(false, MutexName))
                {
                    if (_instance == null)
                    {
                        _instance = new BrowserStackLocal();
                    }
                }

                return _instance;
            }
        }

        public void Dispose()
        {
            if (_browserStackLocalIsRunning)
            {
                Stop();
            }
        }

        /*-------------------------------------------------------------------*/

        public bool Start(string browserStackKey, string overrideArguments = null)
        {
            using (var mutex = new Mutex(false, MutexName))
            {
                if (_browserStackLocalIsRunning)
                {
                    Console.WriteLine("BrowserStackLocal is already running!");
                    return false;
                }

                Console.WriteLine("Starting BrowserStackLocal..");
                try
                {
                    string fullPathToExe = EmbeddedResources.UnpackFromAssembly("BrowserStackLocal.exe", Assembly.GetAssembly(typeof(SeleniumWebDriver)));

                    var processStartInfo = GetProcessStartInfo(fullPathToExe, browserStackKey, overrideArguments);
                    _browserStackLocalProcess = Process.Start(processStartInfo);

                    if (_browserStackLocalProcess != null)
                    {
                        _browserStackLocalProcess.EnableRaisingEvents = true;
                        _browserStackLocalProcess.Exited += BrowserStackLocalProcessOnExited;
                        _browserStackLocalProcess.OutputDataReceived += BrowserStackLocalProcessOnOutputDataReceived;

                        // Wait 1 second to allow BrowserStackLogic to startup and catch any error-shutdowns
                        Thread.Sleep(1000);

                        _browserStackLocalProcess.Refresh();
                        _browserStackLocalIsRunning = !_browserStackLocalProcess.HasExited;
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

                // Operation succeeded if browser stack local is running
                return _browserStackLocalIsRunning;
            }
        }

        public bool Stop()
        {
            using (var mutex = new Mutex(false, MutexName))
            {
                if (!_browserStackLocalIsRunning) return false;

                Console.WriteLine("Stoping BrowserStackLocal.");

                try
                {
                    _browserStackLocalProcess.Kill();
                    _browserStackLocalIsRunning = false;
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

                // Operation succeeded if browser stack local isn't running.
                return !_browserStackLocalIsRunning;
            }
        }

        public bool Restart(string browserStackKey, int retry = 5)
        {
            Console.WriteLine("Restarting the BrowserStackLocal process, attempt {0}/5", retry);

            Stop();
            return Start(browserStackKey, overrideArguments: "-force -v") || (retry > 0 && Restart(browserStackKey, --retry));
        }

        private ProcessStartInfo GetProcessStartInfo(string fullPathToExe, string browserStackKey, string overrideArguments = null)
        {
            if (fullPathToExe == null) throw new ArgumentNullException("fullPathToExe");
            if (browserStackKey == null) throw new ArgumentNullException("browserStackKey");

            // Compose process start info instance
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = fullPathToExe;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = string.Format("{0} {1}", browserStackKey, overrideArguments ?? "localhost,3000,0");

            return startInfo;
        }

        private void BrowserStackLocalProcessOnOutputDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        {
            Console.WriteLine(dataReceivedEventArgs.Data ?? string.Empty);
        }

        private void BrowserStackLocalProcessOnExited(object sender, EventArgs eventArgs)
        {
            _browserStackLocalIsRunning = false;

            Console.WriteLine("BrowserStackLocal process exited.");
        }
    }
}
