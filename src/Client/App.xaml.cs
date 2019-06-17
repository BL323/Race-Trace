using Client.Shell;
using Client.Trace;
using Core.Application.DriverServices;
using Core.Application.EventServices;
using Core.Application.Observers;
using Core.Application.RaceTraceServices;
using Core.Application.ReferenceTimeCalculations;
using ErgastApi.Client;
using Infrastructure.Contracts.Driver;
using Infrastructure.Contracts.Lap;
using Infrastructure.Contracts.Race;
using Infrastructure.DefaultCredentials;
using Infrastructure.Driver;
using Infrastructure.Lap;
using Infrastructure.Race;
using Prism.Ioc;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(typeof(IErgastClient), new ErgastClient
            {
                HttpClient = new DefaultCredentialsHttpClient()
            });

            containerRegistry.RegisterSingleton(typeof(TraceUpdateObserver));
            containerRegistry.RegisterSingleton(typeof(DataLoadingObserver));
            containerRegistry.RegisterSingleton(typeof(ReferenceTimeObserver));
            containerRegistry.RegisterSingleton(typeof(IDriverObservation), typeof(DriverObservation));

            containerRegistry.Register(typeof(IRaceRepository), typeof(RaceRepository));
            containerRegistry.Register(typeof(IRaceService), typeof(RaceService));

            containerRegistry.Register(typeof(IDriverRepository), typeof(DriverRepository));
            containerRegistry.Register(typeof(IDriverService), typeof(DriverService));

            containerRegistry.Register(typeof(ILapRepository), typeof(LapRepository));

            var raceTrace = Container.Resolve(typeof(RaceTraceService));
            containerRegistry.RegisterInstance(typeof(IEventSelector), raceTrace);
            containerRegistry.RegisterInstance(typeof(IReferenceTimeStrategySelector), raceTrace);
            containerRegistry.RegisterInstance(typeof(IRaceTraceService), raceTrace);
        }
    }
}
