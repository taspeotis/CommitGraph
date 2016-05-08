using System;

namespace CommitGraph.Interfaces
{
    public interface IExceptionService
    {
        void Log(Exception exception);
    }
}