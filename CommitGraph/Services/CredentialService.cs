using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommitGraph.Entities;
using CommitGraph.Infrastructure;
using CommitGraph.Interfaces;
using CommitGraph.Specifications;

namespace CommitGraph.Services
{
    [Export(typeof(ICredentialService))]
    internal sealed class CredentialService : ICredentialService
    {
        private static readonly CredentialServiceSettings Settings = new CredentialServiceSettings();

        private readonly IInteractionService _interactionService;

        [ImportingConstructor]
        public CredentialService(IInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        private static Credential[] Credentials
        {
            get { return Settings.Credentials; }
            set { Settings.Credentials = value; }
        }

        public async Task<Credential> GetCredentialAsync(
            string host, string userName, CancellationToken cancellationToken)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));
            if (string.IsNullOrWhiteSpace(host)) throw new ArgumentOutOfRangeException(nameof(host));

            var credentialSpecification = new CredentialSpecification(host, userName);
            var credentials = Credentials ?? Enumerable.Empty<Credential>();
            var credential = credentialSpecification.SatisfiedBy(credentials).FirstOrDefault();

            if (credential == null)
            {
                credential = await _interactionService.GetCredentialAsync(host, userName, cancellationToken);

                if (credential != null)
                {
                    // TODO: merge it
                    Credentials = new[] {credential};
                }
            }

            return credential;
        }

        private class CredentialServiceSettings : Settings
        {
            [SettingsSerializeAs(SettingsSerializeAs.Binary), UserScopedSetting]
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public Credential[] Credentials
            {
                get { return Get<Credential[]>(nameof(Credentials)); }
                set { SetImmediate(nameof(Credentials), value); }
            }
        }
    }
}