using System;
using System.ComponentModel.Composition;
using System.Windows;
using CommitGraph.Interfaces;
using Humanizer;

namespace CommitGraph.Services
{
    [Export(typeof(IExceptionService))]
    internal sealed class ExceptionService : IExceptionService
    {
        public void Log(Exception exception)
        {
            // if (Settings.Default.DisableTelemetry) return;
            // Waiting for "Custom Events" from HockeyApp
            //HockeyClient.Current.TrackEvent
            var owner = System.Windows.Application.Current.MainWindow;
            var caption = exception.GetType().Name.Humanize(LetterCasing.Title);

            MessageBox.Show(owner, exception.Message, caption);
        }
    }
}