using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Media;
using Client.Utilities;
using Core.Application.Observers;
using Core.Application.RaceTraceServices;
using Core.Domain;
using Prism.Mvvm;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Data.Model;

namespace Client.Trace
{
    /// <summary>
    /// Provides race trace data display.
    /// </summary>
    public sealed class TraceViewModel : BindableBase, IDisposable
    {
        private readonly SynchronizationContext _uiContext;
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        private readonly TraceUpdateObserver _traceUpdateObserver;
        private ObservableCollection<IRenderableSeriesViewModel> _series = new ObservableCollection<IRenderableSeriesViewModel>();

        private IRange _xAxisRange = new DoubleRange();

        /// <summary>
        /// Gets and sets the trace series to render.
        /// </summary>
        public ObservableCollection<IRenderableSeriesViewModel> Series
        {
            get => _series;
            set => SetProperty(ref _series, value);
        }

        /// <summary>
        /// Gets the x axis range.
        /// </summary>
        public IRange XAxisRange
        {
            get => _xAxisRange;
            set => SetProperty(ref _xAxisRange, value);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="TraceViewModel"/> class.
        /// </summary>
        /// <param name="raceTraceService">The race trace service.</param>
        /// <param name="traceUpdateObserver">The trace update observer.</param>
        /// <param name="isLoadingObserver">The is data loading observer.</param>
        public TraceViewModel(IRaceTraceService raceTraceService, TraceUpdateObserver traceUpdateObserver, DataLoadingObserver isLoadingObserver)
        {
            _traceUpdateObserver = traceUpdateObserver;
            Subscribe(raceTraceService.RaceTraceObservable, isLoadingObserver.IsLoadingObservable);
            _uiContext = SynchronizationContext.Current;
        }

        private void Subscribe(IObservable<TraceData> raceTraceObservable, IObservable<bool> isLoadingObservable)
        {
            _disposables.AddRange(new[]
            {
                raceTraceObservable.Subscribe(onNext: RaceTraceUpdated),
                isLoadingObservable.Subscribe(onNext: DataIsLoading)
            });
        }

        private void DataIsLoading(bool obj)
        {
            _uiContext.Send(context =>
            {
                _series.Clear();
                XAxisRange = null;
            }, null);
            RaisePropertyChanged(nameof(Series));
        }

        private void RaceTraceUpdated(TraceData raceTrace)
        {
            BuildSeries(raceTrace);
        }

        private void BuildSeries(TraceData raceTrace)
        {
            var drivers = raceTrace.DriverCodes;
            var driverCollection = drivers.Select(driver => CreateDataSeries(driver, raceTrace.GetDataForDriver(driver)));
            _uiContext.Send(context =>
            {
                Series.Clear();
                XAxisRange = null;
                Series = new ObservableCollection<IRenderableSeriesViewModel>(driverCollection);
            }, null);
            NotifyTraceUpdated();
        }

        private void NotifyTraceUpdated()
        {
            _traceUpdateObserver.TracesUpdated(true);
        }

        private IRenderableSeriesViewModel CreateDataSeries(DriverCode driver, DriverTraceData raceData)
        {
            var deltas = raceData.GetAllLaps();
            var teamColour = TeamColourProvider.ColourForTeam(raceData.Team);
            var data = deltas.Select((x, i) => Tuple.Create<double, double>(i, x.Delta.TotalSeconds)).ToList();
            return CreateSeries(driver.Code, teamColour, data);
        }

        private static IRenderableSeriesViewModel CreateSeries(string name, Color stroke, List<Tuple<double, double>> dataList)
        {
            var data = new XyDataSeries<double, double>
            {
                SeriesName = name
            };

            foreach (var (x, y) in dataList)
                data.Append(x, y);

            return new LineRenderableSeriesViewModel
            {
                DataSeries = data,
                Stroke = stroke
            };
        }

        /// <summary>
        /// Dispose and clean up.
        /// </summary>
        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();

            _disposables.Clear();
        }
    }
}
