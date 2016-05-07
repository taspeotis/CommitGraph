using System.ComponentModel.Composition;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CommitGraph.Extensions;
using CommitGraph.Interfaces;
using CommitGraph.Models.Repository;
using LibGit2Sharp;

namespace CommitGraph.Services
{
    [Export(typeof(IRepositoryService))]
    internal sealed class RepositoryService : IRepositoryService
    {
        private readonly IDispatcherService _dispatcherService;
        private readonly IInteractionService _interactionService;

        [ImportingConstructor]
        public RepositoryService(IDispatcherService dispatcherService, IInteractionService interactionService)
        {
            _dispatcherService = dispatcherService;
            _interactionService = interactionService;
        }

        public Task CloneAsync(CloneModel cloneModel, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(Clone, cloneModel, cancellationToken);
        }

        private void Clone(CloneModel cloneModel, CancellationToken cancellationToken)
        {
            var cloneOptions = new CloneOptions
            {
                CredentialsProvider = GetCredentials,
                OnProgress = _ => !cancellationToken.IsCancellationRequested,
                OnTransferProgress = _ => !cancellationToken.IsCancellationRequested,
                OnUpdateTips = delegate { return !cancellationToken.IsCancellationRequested; }
            };

            Repository.Clone(cloneModel.RemoteUrl, cloneModel.WorkingDirectory, cloneOptions);
        }

        private Credentials GetCredentials(
            string remoteUrl, string userName, SupportedCredentialTypes supportedCredentialTypes)
        {
            // ICredentialService(remoteUrl) // it gets the host, and checks for existing creds

            userName = WebUtility.UrlDecode(userName);
            var password = _dispatcherService.Invoke(
                () => _interactionService.GetNetworkCredentialAsync(userName, CancellationToken.None));

            return new UsernamePasswordCredentials
            {
                Password = password.Result.Password,
                Username = "t@speot.is"
            };
        }
    }
}