using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Threading;
using CommitGraph.Interfaces;

namespace CommitGraph.Services
{
    [Export(typeof(IDispatcherService))]
    internal sealed class DispatcherService : IDispatcherService
    {
        private readonly Dispatcher _dispatcher = System.Windows.Application.Current.Dispatcher;

        public void Invoke(Action invokeAction)
        {
            _dispatcher.Invoke(invokeAction);
        }

        public TResult Invoke<TResult>(Func<TResult> invokeFunc)
        {
            return _dispatcher.Invoke(invokeFunc);
        }

        public Task InvokeAsync(Func<Task> invokeFunc)
        {
            return _dispatcher.Invoke(invokeFunc);
        }

        public Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> invokeFunc)
        {
            return _dispatcher.Invoke(invokeFunc);
        }
    }
}