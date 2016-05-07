using System;
using System.Threading;
using System.Threading.Tasks;

namespace CommitGraph.Extensions
{
    internal static class TaskFactoryExtensions
    {
        public static Task StartNew<TModel>(this TaskFactory taskFactory,
            Action<TModel, CancellationToken> taskAction, TModel taskModel, CancellationToken cancellationToken)
        {
            return taskFactory.StartNew(() => taskAction(taskModel, cancellationToken),
                cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }
    }
}