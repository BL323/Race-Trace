using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Client.Trace
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class TraceView : UserControl
    {
        public TraceView(TraceUpdateObserver traceUpdateObserver)
        {
            InitializeComponent();

            traceUpdateObserver.TraceUpdateObservable.Subscribe(onNext: TraceUpdated);
        }

        private void TraceUpdated(bool shouldRefresh)
        {
            if (!shouldRefresh)
                return;

            Dispatcher.Invoke(DispatcherPriority.Render, new Action(() =>
            {
                TraceChart.ZoomExtents();
            }));
        }
    }
}
