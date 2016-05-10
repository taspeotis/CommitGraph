using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CommitGraph.Infrastructure;
using CommitGraph.Interfaces;
using CommitGraph.Models.Repository;
using CommitGraph.ViewModels;
using Prism.Commands;

namespace CommitGraph.Modules.Repository.ViewModels
{
    [Export]
    public sealed class CloneRepositoryViewModel : FlyoutViewModelBase
    {
        private readonly IRepositoryService _repositoryService;

        [ImportingConstructor]
        public CloneRepositoryViewModel(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;

            //BrowseCommand = 
            //CloneCommand
            CloneCommand = new AsyncDelegateCommand(CloneAsync);
        }

        private async Task CloneAsync(CancellationToken cancellationToken)
        {
            await _repositoryService.CloneAsync(new CloneModel
            {
                RemoteUrl = RemoteUrl,
                WorkingDirectory = WorkingDirectory
            }, cancellationToken);

            CloseCommand.Execute(null);
        }

        public string RemoteUrl { get; set; }

        public string WorkingDirectory { get; set; }

        public ICommand BrowseCommand { get; set; }

        public ICommand CloneCommand { get; set; }
    }
}