﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Image_Utility.Commands
{
    public abstract class AsyncCommandBase : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action<Exception> _onException;

        private bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected AsyncCommandBase(Action<Exception> onException)
        {
            _onException = onException;
        }
        public bool CanExecute(object? parameter)
        {
            return !IsExecuting;
        }

        public async void Execute(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter!);
            }
            catch(Exception ex)
            {
                _onException?.Invoke(ex);
            }
            
            IsExecuting = false;
        }

        protected abstract Task ExecuteAsync(object parameter);
    }
}
