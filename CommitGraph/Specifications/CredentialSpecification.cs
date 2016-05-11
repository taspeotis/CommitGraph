using System;
using System.Linq.Expressions;
using CommitGraph.Entities;
using SpecificationPatternDotNet;

namespace CommitGraph.Specifications
{
    public sealed class CredentialSpecification : Specification<Credential>
    {
        private readonly string _host;
        private readonly string _userName;

        public CredentialSpecification(string host, string userName = null)
        {
            if (host == null) throw new ArgumentNullException(nameof(host));
            if (string.IsNullOrWhiteSpace(host)) throw new ArgumentOutOfRangeException(nameof(host));

            _host = host;
            _userName = userName;
        }

        protected override Expression<Func<Credential, bool>> Predicate
        {
            get
            {
                if (_userName != null)
                    return c => c.Host == _host && c.UserName == _userName;

                return c => c.Host == _host;
            }
        }
    }
}