using System.Collections.Generic;
using FluentAutomation.API.Enumerations;
using FluentAutomation.RemoteCommands;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using FluentAutomation.Server.Model;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Windows;
using GalaSoft.MvvmLight.Command;
using FluentAutomation.API;
using System.ComponentModel;

namespace FluentAutomation.Server.ViewModel
{
    public class TestViewModel : ViewModelBase
    {
        public TestViewModel()
        {
            MessengerInstance.Register<GenericMessage<TestDetails>>(this, (test) =>
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    // only handle one test at a time!
                    if (this.RemoteCommands.Count == 0)
                    {
                        this.Browsers = new ObservableCollection<BrowserType>(test.Content.Browsers);
                        foreach (var commandItem in test.Content.RemoteCommands)
                        {
                            this.RemoteCommands.Add(new RemoteCommandViewModel
                            {
                                CommandName = commandItem.Key.GetType().Name,
                                RemoteCommand = commandItem.Key,
                                RemoteCommandArguments = commandItem.Value
                            });
                        }

                        this.Name = "Received at " + DateTime.Now.ToLongTimeString();
                        this.ExecuteButtonVisibility = this.RemoteCommands.Count == 0 ? Visibility.Collapsed : Visibility.Visible;

                        this._manager = new CommandManager(new FluentAutomation.SeleniumWebDriver.AutomationProvider());
                    }
                });
            });
        }

        private CommandManager _manager = null;

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _name = "Waiting for test data ...";

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Browsers" /> property's name.
        /// </summary>
        public const string BrowsersPropertyName = "Browsers";

        private ObservableCollection<BrowserType> _browsers = new ObservableCollection<BrowserType>();

        /// <summary>
        /// Sets and gets the Browsers property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<BrowserType> Browsers
        {
            get
            {
                return _browsers;
            }

            set
            {
                if (_browsers == value)
                {
                    return;
                }

                _browsers = value;
                RaisePropertyChanged(BrowsersPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RemoteCommands" /> property's name.
        /// </summary>
        public const string RemoteCommandsPropertyName = "RemoteCommands";

        private ObservableCollection<RemoteCommandViewModel> _remoteCommands = new ObservableCollection<RemoteCommandViewModel>();

        /// <summary>
        /// Sets and gets the RemoteCommands property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<RemoteCommandViewModel> RemoteCommands
        {
            get
            {
                return _remoteCommands;
            }

            set
            {
                if (_remoteCommands == value)
                {
                    return;
                }

                _remoteCommands = value;
                RaisePropertyChanged(RemoteCommandsPropertyName);
            }
        }

        private RelayCommand _executeTest;

        /// <summary>
        /// Gets the ExecuteTest.
        /// </summary>
        public RelayCommand ExecuteTest
        {
            get
            {
                return _executeTest
                    ?? (_executeTest = new RelayCommand(
                                        () =>
                                        {
                                            int percentPerCommand = 100 / this.RemoteCommands.Count;

                                            this.ExecuteButtonVisibility = Visibility.Collapsed;
                                            this.ContinueButtonVisibility = Visibility.Collapsed;

                                            BackgroundWorker bgWorker = new BackgroundWorker();
                                            bgWorker.WorkerReportsProgress = true;

                                            bgWorker.DoWork += (e, a) =>
                                            {
                                                bool hitBreakpoint = false;

                                                foreach (var cmd in this.RemoteCommands)
                                                {
                                                    if (cmd.IsBreakpoint)
                                                    {
                                                        hitBreakpoint = true;
                                                        this.ContinueButtonVisibility = Visibility.Visible;
                                                        break;
                                                    }

                                                    if (cmd.Status == "Executed")
                                                    {
                                                        continue;
                                                    }

                                                    try
                                                    {
                                                        bgWorker.ReportProgress(0, new { Command = cmd, StatusText = "Executing" });
                                                        cmd.RemoteCommand.Execute(this._manager, cmd.RemoteCommandArguments);
                                                    }
                                                    catch (Exception)
                                                    {
                                                        bgWorker.ReportProgress(100, new { Command = cmd, StatusText = "Error" });
                                                    }

                                                    bgWorker.ReportProgress(100, new { Command = cmd, StatusText = "Executed" });
                                                }

                                                if (!hitBreakpoint)
                                                {
                                                    this._manager.Finish();
                                                    this.ExecuteButtonVisibility = Visibility.Visible;
                                                }
                                            };

                                            bgWorker.ProgressChanged += (e, a) =>
                                            {
                                                dynamic userState = a.UserState;
                                                userState.Command.Status = userState.StatusText;
                                            };

                                            bgWorker.RunWorkerAsync();
                                        }));
            }
        }

        /// <summary>
        /// The <see cref="ExecuteButtonVisibility" /> property's name.
        /// </summary>
        public const string ExecuteButtonVisibilityPropertyName = "ExecuteButtonVisibility";

        private Visibility _executeButtonVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the ExecuteButtonVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility ExecuteButtonVisibility
        {
            get
            {
                return _executeButtonVisibility;
            }

            set
            {
                if (_executeButtonVisibility == value)
                {
                    return;
                }

                _executeButtonVisibility = value;
                RaisePropertyChanged(ExecuteButtonVisibilityPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ContinueButtonVisibility" /> property's name.
        /// </summary>
        public const string ContinueButtonVisibilityPropertyName = "ContinueButtonVisibility";

        private Visibility _continueButtonVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the ContinueButtonVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility ContinueButtonVisibility
        {
            get
            {
                return _continueButtonVisibility;
            }

            set
            {
                if (_continueButtonVisibility == value)
                {
                    return;
                }

                _continueButtonVisibility = value;
                RaisePropertyChanged(ContinueButtonVisibilityPropertyName);
            }
        }

        private RelayCommand<RemoteCommandViewModel> _addBreakpoint;

        /// <summary>
        /// Gets the MyCommand.
        /// </summary>
        public RelayCommand<RemoteCommandViewModel> AddBreakpoint
        {
            get
            {
                return _addBreakpoint ??
                    (_addBreakpoint =
                        new RelayCommand<RemoteCommandViewModel>((command) =>
                        {
                            command.IsBreakpoint = command.IsBreakpoint ? false : true;
                        })
                    );
            }
        }

        private RelayCommand _continue;

        /// <summary>
        /// Gets the Continue.
        /// </summary>
        public RelayCommand Continue
        {
            get
            {
                return _continue ??
                    (_continue =
                        new RelayCommand(() =>
                        {
                            foreach (var command in RemoteCommands)
                            {
                                if (command.IsBreakpoint)
                                {
                                    command.IsBreakpoint = false;
                                    break;
                                }
                            }

                            this.ExecuteTest.Execute(null);
                        })
                    );
            }
        }
    }
}
