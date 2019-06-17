namespace Core.Application.EventServices
{
    /// <summary>
    /// Provides event selection.
    /// </summary>
    public interface IEventSelector
    {
        /// <summary>
        /// Provides selection of events to plot.
        /// </summary>
        /// <param name="eventSelection">The event selection.</param>
        void SelectEvent(EventSelection eventSelection);
    }
}
