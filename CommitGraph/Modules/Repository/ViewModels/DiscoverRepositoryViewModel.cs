using System.ComponentModel.Composition;
using System.Windows.Input;
using CommitGraph.Interfaces;
using CommitGraph.ViewModels;

namespace CommitGraph.Modules.Repository.ViewModels
{
    [Export]
    public sealed class DiscoverRepositoryViewModel : FlyoutViewModelBase
    {
        private readonly IRepositoryService _repositoryService;

        [ImportingConstructor]
        public DiscoverRepositoryViewModel(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;

            //DiscoverCommand = new DelegateCommand(Discover);
        }

        public string WorkingDirectory { get; set; }

        public ICommand BrowseCommand { get; set; }
    }
}