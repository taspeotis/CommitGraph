using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace CommitGraph.Interfaces
{
    public interface IInteractionService
    {
        Task<NetworkCredential> GetNetworkCredentialAsync(string userName, CancellationToken cancellationToken);
    }
}