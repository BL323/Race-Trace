using Client.EventSelections;
using Client.InformationBar;
using Client.ReferenceTime;
using Client.Trace;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.Shell
{
    /// <summary>
    /// Provides the display for the shell view.
    /// </summary>
    public class MainWindowViewModel : BindableBase
    {
        /// <summary>
        /// Gets the application title.
        /// </summary>
        public string Title { get; } = "Race Trace";

        /// <summary>
        /// Initialises a new instance of the <see cref="MainWindowViewModel"/> object.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        public MainWindowViewModel(IRegionManager regionManager)
        {
            RegisterRegions(regionManager);
        }

        private void RegisterRegions(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.EventSelectionRegion, typeof(EventSelectionView));
            regionManager.RegisterViewWithRegion(RegionNames.TraceRegion, typeof(TraceView));
            regionManager.RegisterViewWithRegion(RegionNames.ReferenceTimeRegion, typeof(ReferenceTimeView));
            regionManager.RegisterViewWithRegion(RegionNames.InformationBarRegion, typeof(InformationBarView));
        }
    }
}
