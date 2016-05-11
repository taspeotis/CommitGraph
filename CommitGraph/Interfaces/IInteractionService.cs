﻿using System.Threading;
using System.Threading.Tasks;
using CommitGraph.Entities;

namespace CommitGraph.Interfaces
{
    public interface IInteractionService
    {
        Task<Credential> GetCredentialAsync(string host, string userName, CancellationToken cancellationToken);
    }
}