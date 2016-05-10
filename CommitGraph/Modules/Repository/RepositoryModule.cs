using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using CommitGraph.Infrastructure;
using CommitGraph.Modules.Repository.ViewModels;
using CommitGraph.Modules.Repository.Views;
using CommitGraph.ViewModels;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;

namespace CommitGraph.Modules.Repository
{
    [ModuleExport(typeof(RepositoryModule))]
    internal sealed class RepositoryModule : IModule
    {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public RepositoryModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterView<CloneRepositoryView, CloneRepositoryViewModel>();
            // Discover
            // Init
        }

        private void RegisterView<TView, TViewModel>()
            where TView : FrameworkElement
            where TViewModel : FlyoutViewModelBase
        {
            var viewModel = ServiceLocator.Current.GetInstance<TViewModel>();

            _regionManager.RegisterViewWithRegion(Constants.FlyoutControlsRegionName, typeof(TView));

            _regionManager.RegisterViewWithRegion(Constants.RightWindowCommandsRegionName,
                () => new Button {Command = viewModel.ShowCommand, Content = viewModel.Header});
        }
    }
}