using System.Windows.Input;
using CommitGraph.ViewModels;

namespace CommitGraph.Modules.Repository.ViewModels
{
    internal sealed class CloneRepositoryViewModel : ViewModelBase
    {
        public string RemoteUrl { get; set; }

        public string WorkingDirectory { get; set; }

        public ICommand BrowseCommand { get; set; }

        public ICommand CloneCommand { get; set; }
    }
}