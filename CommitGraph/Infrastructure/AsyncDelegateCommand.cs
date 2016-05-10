using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommitGraph.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace CommitGraph.Infrastructure
{
    public sealed class AsyncDelegateCommand : ICommand
    {
        private readonly Func<CancellationToken, Task> _taskFunc;
        private bool _canExecute = true;

        public AsyncDelegateCommand(Func<CancellationToken, Task> taskFunc)
        {
            _taskFunc = taskFunc;
        }

        private bool CanExecute
        {
            get { return _canExecute; }

            set
            {
                if (_canExecute != value)
                {
                    _canExecute = value;

                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            if (CanExecute)
            {
                var exceptionService = ServiceLocator.Current.GetInstance<IExceptionService>();

                try
                {
                    CanExecute = false;

                    using (var cancellationTokenSource = new CancellationTokenSource())
                        await _taskFunc(cancellationTokenSource.Token);
                }
                catch (Exception exception)
                {
                    exceptionService.Log(exception);
                }
                finally
                {
                    CanExecute = true;
                }
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}