using System.ComponentModel.Composition;
using System.Threading;
using System.Windows;
using CommitGraph.Interfaces;
using CommitGraph.Models.Repository;

namespace CommitGraph
{
    [Export]
    public partial class Shell
    {
        private readonly IRepositoryService _repositoryService;

        [ImportingConstructor]
        public Shell(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;

            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            await _repositoryService.CloneAsync(new CloneModel
            {
                RemoteUrl = "https://t%40speot.is@github.com/taspeotis/CommitGraph.git",
                WorkingDirectory = @"C:\Temp\."
            }, CancellationToken.None);
        }
    }
}