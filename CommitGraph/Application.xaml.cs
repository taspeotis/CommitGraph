using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef;

namespace CommitGraph
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public sealed partial class Application
    {
        public Application()
        {
            Startup += delegate { new ApplicationBootstrapper().Run(); };
        }

        private sealed class ApplicationBootstrapper : MefBootstrapper
        {
            protected override void ConfigureAggregateCatalog()
            {
                base.ConfigureAggregateCatalog();

                var applicationType = typeof(Application);
                var applicationAssembly = applicationType.Assembly;
                var applicationCatalog = new AssemblyCatalog(applicationAssembly);

                AggregateCatalog.Catalogs.Add(applicationCatalog);
            }

            protected override DependencyObject CreateShell()
            {
                return ServiceLocator.Current.GetInstance<Shell>();
            }

            protected override void InitializeShell()
            {
                base.InitializeShell();

                ((UIElement) Shell).Visibility = Visibility.Visible;
            }
        }
    }
}