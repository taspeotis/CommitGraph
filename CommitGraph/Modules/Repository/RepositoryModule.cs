using System.ComponentModel.Composition;
using System.Windows.Controls;
using CommitGraph.Infrastructure;
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
            _regionManager.RegisterViewWithRegion(Constants.RegionNames.RightWindowCommands, () => new Button {Content = "Hello"});
        }
    }
}