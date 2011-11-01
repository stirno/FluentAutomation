using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using FluentAutomation.RemoteCommands;

namespace FluentAutomation.Server.ViewModel
{
    public class RemoteCommandViewModel : ViewModelBase
    {
        /// <summary>
        /// The <see cref="CommandName" /> property's name.
        /// </summary>
        public const string CommandNamePropertyName = "CommandName";

        private string _commandName = string.Empty;

        /// <summary>
        /// Sets and gets the CommandName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CommandName
        {
            get
            {
                return _commandName;
            }

            set
            {
                if (_commandName == value)
                {
                    return;
                }

                _commandName = value;
                RaisePropertyChanged(CommandNamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Status" /> property's name.
        /// </summary>
        public const string StatusPropertyName = "Status";

        private string _status = "pending";

        /// <summary>
        /// Sets and gets the Status property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }

            set
            {
                if (_status == value)
                {
                    return;
                }

                _status = value;
                RaisePropertyChanged(StatusPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsBreakpoint" /> property's name.
        /// </summary>
        public const string IsBreakpointPropertyName = "IsBreakpoint";

        private bool _isBreakpoint = false;

        /// <summary>
        /// Sets and gets the IsBreakpoint property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsBreakpoint
        {
            get
            {
                return _isBreakpoint;
            }

            set
            {
                if (_isBreakpoint == value)
                {
                    return;
                }

                _isBreakpoint = value;
                RaisePropertyChanged(IsBreakpointPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RemoteCommand" /> property's name.
        /// </summary>
        public const string RemoteCommandPropertyName = "RemoteCommand";

        private IRemoteCommand _remoteCommand = null;

        /// <summary>
        /// Sets and gets the RemoteCommand property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IRemoteCommand RemoteCommand
        {
            get
            {
                return _remoteCommand;
            }

            set
            {
                if (_remoteCommand == value)
                {
                    return;
                }

                _remoteCommand = value;
                RaisePropertyChanged(RemoteCommandPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="RemoteCommandArguments" /> property's name.
        /// </summary>
        public const string RemoteCommandArgumentsPropertyName = "RemoteCommandArguments";

        private IRemoteCommandArguments _remoteCommandArguments = null;

        /// <summary>
        /// Sets and gets the RemoteCommandArguments property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IRemoteCommandArguments RemoteCommandArguments
        {
            get
            {
                return _remoteCommandArguments;
            }

            set
            {
                if (_remoteCommandArguments == value)
                {
                    return;
                }

                _remoteCommandArguments = value;
                RaisePropertyChanged(RemoteCommandArgumentsPropertyName);
            }
        }
    }
}
