using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CommitGraph.Entities;
using CommitGraph.Interfaces;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CommitGraph.Services
{
    [Export(typeof(IInteractionService))]
    internal sealed class InteractionService : IInteractionService
    {
        public async Task<Credential> GetCredentialAsync(
            string host, string userName, CancellationToken cancellationToken)
        {
            var metroWindow = (MetroWindow) System.Windows.Application.Current.MainWindow;

            var loginDialogSettings = new LoginDialogSettings
            {
                UsernameWatermark = "User Name",
                InitialUsername = userName,
                PasswordWatermark = "Password",
                EnablePasswordPreview = true,
                AffirmativeButtonText = "Sign In",
                NegativeButtonVisibility = Visibility.Visible
            };

            var result = await metroWindow.ShowLoginAsync(
                "Credentials", $"Credentials require for {host}", loginDialogSettings);

            if (result != null)
            {
                return new Credential(host, result.Username, result.Password);
            }

            return null;
        }
    }
}