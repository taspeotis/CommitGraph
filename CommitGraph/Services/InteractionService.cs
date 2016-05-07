using System.ComponentModel.Composition;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CommitGraph.Interfaces;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CommitGraph.Services
{
    [Export(typeof(IInteractionService))]
    internal sealed class InteractionService : IInteractionService
    {
        public async Task<NetworkCredential> GetNetworkCredentialAsync(string userName,
            CancellationToken cancellationToken)
        {
            var result =
                await
                    ((MetroWindow) Application.Current.MainWindow).ShowLoginAsync("Credentials", "Enter your creds for ",
                        new LoginDialogSettings {InitialUsername = userName});

            if (result != null)
            {
                return new NetworkCredential(result.Username, result.Password);
            }

            return null;
        }
    }
}