using System.Threading;
using System.Threading.Tasks;
using CommitGraph.Entities;
using CommitGraph.Models;

namespace CommitGraph.Interfaces
{
    public interface ICredentialService
    {
        Task<Credential> GetCredentialAsync(string host, string userName, CancellationToken cancellationToken);
    }
}