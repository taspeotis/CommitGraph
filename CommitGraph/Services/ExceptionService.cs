using System;
using CommitGraph.Interfaces;

namespace CommitGraph.Services
{
    internal sealed class ExceptionService : IExceptionService
    {
        public void Log(Exception exception)
        {
            // if (Settings.Default.DisableTelemetry) return;
            // Waiting for "Custom Events" from HockeyApp
            //HockeyClient.Current.TrackEvent
        }
    }
}