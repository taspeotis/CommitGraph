using System;
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
        private readonly ICredentialService _credentialService;
        private readonly IDispatcherService _dispatcherService;

        [ImportingConstructor]
        public RepositoryService(ICredentialService credentialService, IDispatcherService dispatcherService)
        {
            _credentialService = credentialService;
            _dispatcherService = dispatcherService;
        }

        public Task CloneAsync(CloneModel cloneModel, CancellationToken cancellationToken)
        {
            // await response
            // write the path into the list of repositories
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

        public void Discover(string workingDirectory)
        {
            // check return code
            Repository.Discover(workingDirectory);
        }

        private Credentials GetCredentials(
            string remoteUrl, string userName, SupportedCredentialTypes supportedCredentialTypes)
        {
            // ICredentialService(remoteUrl) // it gets the host, and checks for existing creds
            var uri = new Uri(remoteUrl); // todo? tryPass?

            userName = WebUtility.UrlDecode(userName);
            var password = _dispatcherService.Invoke(
                () => _credentialService.GetCredentialAsync(uri.Host, userName, CancellationToken.None));

            if (password == null)
                return null;

            return new UsernamePasswordCredentials
            {
                Username = password.Result.UserName,
                Password = password.Result.Password
            };
        }
    }
}