using System.Threading;
using System.Threading.Tasks;
using CommitGraph.Models.Repository;

namespace CommitGraph.Interfaces
{
    public interface IRepositoryService
    {
        Task CloneAsync(CloneModel cloneModel, CancellationToken cancellationToken);
    }
}