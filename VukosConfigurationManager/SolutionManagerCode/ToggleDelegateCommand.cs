using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace VukosConfigurationManager
{

    public abstract class ToggleDelegateCommandBase : ICommand
    {
        #region ICommand Members

        #region IsEnabled

        /// <summary>
        /// A backing store for the property <see cref="IsEnabled"/>
        /// </summary>
        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnCanExecuteChanged();
                }
            }
        }

        #endregion


        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;
        protected void OnCanExecuteChanged()
        {
            EventHandler t = CanExecuteChanged;
            if (t != null)
            {
                t(this, EventArgs.Empty);
            }
        }

        public abstract void Execute(object parameter);

        #endregion
    }

    public sealed class ToggleDelegateCommand : ToggleDelegateCommandBase
    {
        private readonly Action _action;
        public ToggleDelegateCommand(Action action)
        {
            _action = action;
        }
        public override void Execute(object parameter)
        {
            if (_action != null)
            {
                _action();
            }
        }
    }

    public sealed class ToggleDelegateCommand<T> : ToggleDelegateCommandBase
    {
        private readonly Action<T> _action;
        public ToggleDelegateCommand(Action<T> action)
        {
            _action = action;
        }
        public override void Execute(object parameter)
        {
            if (_action != null)
            {
                _action((T)parameter);
            }
        }
    }
}
