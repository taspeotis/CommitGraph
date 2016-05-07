using System;
using System.Threading.Tasks;

namespace CommitGraph.Interfaces
{
    public interface IDispatcherService
    {
        void Invoke(Action invokeAction);

        TResult Invoke<TResult>(Func<TResult> invokeFunc);

        Task InvokeAsync(Func<Task> invokeFunc);

        Task<TResult> InvokeAsync<TResult>(Func<Task<TResult>> invokeFunc);
    }
}