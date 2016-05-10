using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using CommitGraph.Infrastructure;
using CommitGraph.Properties;
using HockeyApp;
using MahApps.Metro.Controls;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using Prism.Regions;

namespace CommitGraph
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public sealed partial class Application
    {
        public Application()
        {
            Startup += async delegate
            {
                if (Settings.Default.DisableTelemetry)
                    return;

                var hockeyClient = HockeyClient.Current;

                hockeyClient.Configure(Constants.HockeyAppId);

                try
                {
                    await hockeyClient.SendCrashesAsync();

                    // It's not clear that "shutdownActions" is ever called...
                    // https://github.com/bitstadium/HockeySDK-Windows/issues/39
                    await hockeyClient.CheckForUpdatesAsync(true, () => true);
                }
                catch
                {
                    // https://github.com/bitstadium/HockeySDK-Windows/issues/9
                }
            };

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

            protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
            {
                var regionAdapterMappings = base.ConfigureRegionAdapterMappings();

                regionAdapterMappings.RegisterMapping(typeof(FlyoutsControl),
                    ServiceLocator.Current.GetInstance<FlyoutsControlRegionAdapter>());

                return regionAdapterMappings;
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

            [Export]
            private sealed class FlyoutsControlRegionAdapter : RegionAdapterBase<FlyoutsControl>
            {
                [ImportingConstructor]
                public FlyoutsControlRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
                    : base(regionBehaviorFactory)
                {
                }

                protected override void Adapt(IRegion region, FlyoutsControl flyoutsControl)
                {
                    region.ActiveViews.CollectionChanged += (_, eventArgs) =>
                    {
                        if (eventArgs.Action != NotifyCollectionChangedAction.Add) return;

                        foreach (FrameworkElement frameworkElement in eventArgs.NewItems)
                        {
                            flyoutsControl.Items.Add(new Flyout
                            {
                                Content = frameworkElement,
                                DataContext = frameworkElement.DataContext,
                                Position = Position.Right
                            });
                        }
                    };
                }

                protected override IRegion CreateRegion()
                {
                    return new AllActiveRegion();
                }
            }
        }
    }
}