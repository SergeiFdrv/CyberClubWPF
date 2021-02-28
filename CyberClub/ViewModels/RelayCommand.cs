using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CyberClub.ViewModels
{
    /// <summary>
    /// Code from https://metanit.com/sharp/wpf/22.3.php
    /// </summary>
    public class RelayCommand : ICommand
    {
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _Execute = execute;
            _CanExecute = canExecute;
        }

        private Action<object> _Execute;
        private Func<object, bool> _CanExecute;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _CanExecute == null || _CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _Execute(parameter);
        }
    }
}
