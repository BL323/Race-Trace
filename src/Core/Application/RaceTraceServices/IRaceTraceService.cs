using System;
using Core.Domain;

namespace Core.Application.RaceTraceServices
{
    /// <summary>
    /// Provides trace times.
    /// </summary>
    public interface IRaceTraceService
    {
        /// <summary>
        /// Provides update race traces.
        /// </summary>
        IObservable<TraceData> RaceTraceObservable { get; }
    }
}
